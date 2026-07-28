
import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { IQueryResponse, ILookup } from '@bfs/_shared/interfaces';
import { IDocumentDetailsFilter } from './document-details.shared';

@Component({
    selector: 'app-document-details-filter',
    imports: [FormsModule, CommonModule],
    templateUrl: './document-details.filter.component.html'
    //styles: ``
})
export class DocumentDetailsFilterComponent implements OnInit {

    public result = {} as IDocumentDetailsFilter;

    // Define look ups
    public ProductOptions:  any[] = [];
public UnitOptions:  any[] = [];
public DocumentOptions:  any[] = [];

    // Define range filters
    public QuantityFrom: number | undefined;
    public QuantityTo: number | undefined;

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
        this.QuantityFrom = this.result.Quantity?.from;
        this.QuantityTo   = this.result.Quantity?.to;

    }
    //---------------------------------------------------------
    async getLookups(): Promise<void> {
        let target = '';
        target = "/Product/list";
        (await this.parent.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.ProductOptions = response.items;
                this.isLoading.list = false;
            },
                error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching Product data.';
                this.isLoading.list = false;
            }
        });
target = "/Unit/list";
        (await this.parent.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.UnitOptions = response.items;
                this.isLoading.list = false;
            },
                error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching Unit data.';
                this.isLoading.list = false;
            }
        });
target = "/Document/list";
        (await this.parent.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.DocumentOptions = response.items;
                this.isLoading.list = false;
            },
                error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching Document data.';
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
        this.result.Quantity = { from: this.QuantityFrom, to: this.QuantityTo };

        this.parent.applyFilter(this.result);
    }
}

