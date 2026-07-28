
import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { IQueryResponse, ILookup } from '@bfs/_shared/interfaces';
import { IDocumentFilter } from './document.shared';

@Component({
    selector: 'app-document-filter',
    imports: [FormsModule, CommonModule],
    templateUrl: './document.filter.component.html'
    //styles: ``
})
export class DocumentFilterComponent implements OnInit {

    public result = {} as IDocumentFilter;

    // Define look ups
    public StoreOptions:  any[] = [];
public OperationOptions:  any[] = [];

    // Define range filters
    public ResponseDateFrom: Date | null | undefined;
    public ResponseDateTo: Date | null | undefined;

    isLoading: { list: boolean } = { list: false };
    public submit: boolean = false;
    public errorMessage: string = '';
    public infoMessage: string = '';
    public currentOperation: string = '';
    public parent: any;
    //---------------------------------------------------------
    constructor(public activeModal: NgbActiveModal) { }

    async ngOnInit(): Promise<void> {
        this.result = this.parent.queryRequest.filter || {};
        await this.getLookups();
        // Initialize range filters if not set
        this.ResponseDateFrom = this.result.ResponseDate?.from;
        this.ResponseDateTo   = this.result.ResponseDate?.to;

    }
    //---------------------------------------------------------
    async getLookups(): Promise<void> {
        let target = '';
        target = "/Store/list";
        (await this.parent.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.StoreOptions = response.items;
                this.isLoading.list = false;
            },
                error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching Store data.';
                this.isLoading.list = false;
            }
        });
target = "/Operation/list";
        (await this.parent.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.OperationOptions = response.items;
                this.isLoading.list = false;
            },
                error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching Operation data.';
                this.isLoading.list = false;
            }
        });

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
        this.result.ResponseDate = { from: this.ResponseDateFrom, to: this.ResponseDateTo };

        this.parent.applyFilter(this.result);
    }
}

