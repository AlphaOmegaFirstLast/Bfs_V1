
import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { IQueryResponse, ILookup } from '@bfs/_shared/interfaces';
import { IBfsFieldFilter } from './bfs-field.shared';
import { IAutoComplete, AutoCompleteHelper } from '@bfs/_shared/helpers/auto-complete.class';


@Component({
    selector: 'app-bfs-field-filter',
    imports: [FormsModule, CommonModule],
    templateUrl: './bfs-field.filter.component.html'
    //styles: ``
})
export class BfsFieldFilterComponent implements OnInit {

    public result = {} as IBfsFieldFilter;

    // Define look ups
    public FilterTypeOptions: any[] = [];
    public BackendDataTypeOptions: any[] = [];

    bfsComponentAuto: AutoCompleteHelper;

    // Define range filters

    public isLoading: any = { list: false, view: false, save: false, lookups: false, autoComplete: false };
    public submit: boolean = false;
    public errorMessage: string = '';
    public infoMessage: string = '';
    public currentOperation: string = '';
    public parent: any;

    //---------------------------------------------------------
    constructor(public activeModal: NgbActiveModal) {
        this.bfsComponentAuto = new AutoCompleteHelper({ queryUrl: "/BfsComponent/list", fieldName: 'BfsComponent', control: null, id: '', name: '', showDropDown: false, options: [], isLoading: false, isInitial: true } as IAutoComplete);
    }

    async ngOnInit(): Promise<void> {
        this.result = this.parent.queryRequest.filter || {};
        await this.getLookups();
        await this.setAutoComplete();
        // Initialize range filters if not set

    }
    //---------------------------------------------------------
    async getLookups(): Promise<void> {
        let target = '';
        target = "/FilterType/list";
        (await this.parent.apiService.post(target, { pageSize: 50 })).subscribe({
            next: (response: IQueryResponse) => {
                this.FilterTypeOptions = response.items;
                this.isLoading.list = false;
            },
            error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching Filter Type data.';
                this.isLoading.list = false;
            }
        });
        target = "/BackendDataType/list";
        (await this.parent.apiService.post(target, { pageSize: 50 })).subscribe({
            next: (response: IQueryResponse) => {
                this.BackendDataTypeOptions = response.items;
                this.isLoading.list = false;
            },
            error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching Backend Type data.';
                this.isLoading.list = false;
            }
        });

    }
    //---------------------------------------------------------
    async setAutoComplete() {
        this.bfsComponentAuto.setupFilter(this.parent.apiService, this.result);
    }
    //---------------------------------------------------------
    reset() {
        this.activeModal.close('Reset');
        this.parent.applyFilter(null);
    }
    //---------------------------------------------------------
    apply() {
        this.activeModal.close('Apply');
        // Apply range filters

        this.parent.applyFilter(this.result);
    }
}

