//---------------- angular ----------------------------------
import { Component, inject, OnInit, Input, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, ActivatedRoute } from '@angular/router';
//import { Router, RouterLink, ActivatedRoute } from '@angular/router';
//---------------- Ng Bootstrap ------------------------------
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';
import { NgbAlertModule } from '@ng-bootstrap/ng-bootstrap';
import { NgbProgressbarModule } from '@ng-bootstrap/ng-bootstrap';
import { NgIcon } from '@ng-icons/core'

import { IIdentifiable, ILookup, IEntityRequest, IUIMessage, ViewLink, ActionLink } from '@bfs/_shared/interfaces';
import { TokenService } from '@bfs/_shared/services/token.service';


@Component({
    selector: 'app-base-matrix',
    template: ''
})
export class BaseMatrixComponent<IMatrix, IFilter> {
    @Input() presetFilter: IFilter | undefined;
    @Input() entityId: string | number | undefined;

    filter!: IFilter;
    public list: IMatrix[] = [];

    public getApiUrl = '';
    public saveApiUrl = '';

    public getHorizontalApiUrl = '';
    public getVerticalApiUrl = '';

    public apiService!: any;
    public apiHorizontalService!: any;
    public apiVerticalService!: any;
    public tokenService: TokenService = inject(TokenService);

    public queryRequest = {} as IEntityRequest<IFilter>;
    public matrixRequest = { pageIndex: 1, pageSize: 100, filter: {} as any };

    public filterComponent: any;
    public saveLink: ActionLink = { recordId: '', action: this.saveMatrix.bind(this), displayText: "Save" }; // top left button of 'Add' functionality. it gets overriden in derived classes
    //---------------------------------------------------------

    public title: string = '';  // to be set from outside
    public parentId: string = '';
    public horizontalId: string = '';
    public verticalId: string = '';
    public horizontalList: ILookup[] = [];   // Records. up to 100 record
    public verticalList: ILookup[] = [];      // Columns. supposed to be finite. ideally not more than 10

    public filterArray: string[] = [];
    public isLoading = { main: false, matrixVertical: false, matrixHorizontal: false, save: false };
    public messages: IUIMessage[] = [];
    //-------------------------------------------------------- Set visibility of buttons and sections ----------
    public isSection = { chart: false, table: true, description: false };
    public isButton: any = { saveMatrix: true, addNew: false, chart: false, description: true, columns: false, filter: false, sort: false, group: false };

    public me = this;
    constructor(public modalService: NgbModal, public router: Router, public activatedRoute: ActivatedRoute) {
    }
    //---------------------------------------------------------
    async ngOnInit(): Promise<void> {

        if (this.apiHorizontalService == undefined) this.apiHorizontalService = this.apiService;
        if (this.apiVerticalService == undefined) this.apiVerticalService = this.apiService;

        if (this.presetFilter) {
            this.queryRequest.filter = this.presetFilter;
        }
        this.queryRequest.pageSize = 100;

        await this.getChildren();  //get parent children list, that the matrix selections will be saved to.  
        await this.getMatrix();

        this.setAccessible();
    }
    //---------------------------------------------------------
    async getChildren(): Promise<void> {

        if (!this.isLoading.main) {  // to prevent multiple requests
            this.messages = [];
            this.isLoading.main = true;
            var target = this.getApiUrl;
            (await this.apiService.post(target, this.queryRequest)).subscribe({
                next: (res: any) => {
                    this.isLoading.main = false;
                    this.list = res.items;
                },
                error: (err: any) => {
                    this.isLoading.main = false;
                    var msg = err.message || 'An error occurred while processing Broker Activity data.';
                    this.messages.push({ text: msg, msgType: "danger" });
                }
            });
        }
    }
    //---------------------------------------------------------
    async getMatrix(): Promise<void> {

        if (!this.isLoading.matrixHorizontal) {  // to prevent multiple requests
            this.messages = [];
            this.isLoading.matrixHorizontal = true;
            var target = this.getHorizontalApiUrl;
            (await this.apiHorizontalService.post(target, this.matrixRequest)).subscribe({
                next: (res: any) => {
                    this.isLoading.matrixHorizontal = false;
                    this.horizontalList = res.items;

                    // special case of matrix that is of 1 dimention instead of 2.
                    if (this.parentId == this.horizontalId) {
                        this.horizontalList = this.horizontalList.filter(h => h.id.toString() == this.entityId);
                    }
                },
                error: (err: any) => {
                    this.isLoading.matrixHorizontal = false;
                    var msg = err.message || `An error occurred while processing ${this.getHorizontalApiUrl} data.`;
                    this.messages.push({ text: msg, msgType: "danger" });
                }
            });
        }
        if (!this.isLoading.matrixVertical) {  // to prevent multiple requests

            this.isLoading.matrixVertical = true;
            var target = this.getVerticalApiUrl;
            (await this.apiVerticalService.post(target, this.matrixRequest)).subscribe({
                next: (res: any) => {
                    this.isLoading.matrixVertical = false;
                    this.verticalList = res.items;

                    // special case of matrix that is of 1 dimention instead of 2.
                    if (this.parentId == this.verticalId) {
                        this.verticalList = this.verticalList.filter(v => v.id.toString() == this.entityId);
                    }
                },
                error: (err: any) => {
                    this.isLoading.matrixVertical = false;
                    var msg = err.message || `An error occurred while processing ${this.getVerticalApiUrl} data.`;
                    this.messages.push({ text: msg, msgType: "danger" });
                }
            });
        }
    }
    //---------------------------------------------------------
    getDescription() {
        return ["Phrasing the filter object, working on this.queryRequest"];
    }
    //---------------------------------------------------------
    setAccessible(): void {
    }
    //---------------------------------------------------------   
    isVisible(me: BaseMatrixComponent<IMatrix, IFilter>, record: ILookup, column: ILookup): boolean {
        return true;
    }
    //---------------------------------------------------------  
    isChecked(me: any, record: ILookup, column: ILookup): string {
        // check item exists in parent list 
        var matrixItem = me.list.find((item: ILookup) => item
            && item[this.horizontalId as keyof ILookup] == record['id' as keyof ILookup]
            && item[this.verticalId as keyof ILookup] == column['id' as keyof ILookup]
        );

        return matrixItem ? 'checked' : '';
    }
    //---------------------------------------------------------       
    onCheckboxClick(me: any, record: any, column: any): void {
        // check item exists in parent list
        var matrixItem = me.list.find((item: ILookup) => item
            && item[this.horizontalId as keyof ILookup] == record['id' as keyof ILookup]
            && item[this.verticalId as keyof ILookup] == column['id' as keyof ILookup]
        );

        if (matrixItem) {
            // Remove existing item from parent list
            me.list = me.list.filter((item: ILookup) => !(
                item[this.horizontalId as keyof ILookup] == record['id' as keyof ILookup]
                && item[this.verticalId as keyof ILookup] == column['id' as keyof ILookup]
            )
            );
        } else {
            // Add new item to the parent list
            var newItem: any = {};
            newItem[this.parentId] = this.entityId;
            newItem[this.horizontalId] = record.id;
            newItem[this.verticalId] = column.id;
            me.list.push((newItem as ILookup) as ILookup);
        }
    }
    //---------------------------------------------------------   
    async saveMatrix(me: any): Promise<void> {
        if (!me.isLoading.save) {  // to prevent multiple requests
            me.messages = [];
            me.isLoading.save = true;
            var target = me.saveApiUrl + `/${this.entityId}`;

            var childrenList: IMatrix[] = me.list
                .map((item: IMatrix) => ({
                    [this.parentId]: (item[this.parentId as keyof IMatrix] as string | number),
                    [this.horizontalId]: (item[this.horizontalId as keyof IMatrix] as string | number),
                    [this.verticalId]: (item[this.verticalId as keyof IMatrix] as string | number)
                }
                ));

            (await me.apiService.put(target, childrenList)).subscribe({
                next: (res: any) => {
                    me.isLoading.save = false;
                    me.list = res.items;
                },
                error: (err: any) => {
                    me.isLoading.save = false;
                    var msg = err.message || 'An error occurred while saving matrix data.';
                    me.messages.push({ text: msg, msgType: "danger" });
                }
            });
        }
    }
    //---------------------------------------------------------
    viewDescription() {
        this.isSection.description = !this.isSection.description;
    }
    //---------------------------------------------------------
}

