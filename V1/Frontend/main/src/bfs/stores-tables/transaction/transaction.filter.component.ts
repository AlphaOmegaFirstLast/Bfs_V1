
import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { IQueryResponse, ILookup } from '@bfs/_shared/interfaces';
import { ITransactionFilter } from './transaction.shared';

@Component({
    selector: 'app-transaction-filter',
    imports: [FormsModule, CommonModule],
    templateUrl: './transaction.filter.component.html'
    //styles: ``
})
export class TransactionFilterComponent implements OnInit {

    public result = {} as ITransactionFilter;

    // Define look ups
    public StoreOptions:  any[] = [];
public OperationOptions:  any[] = [];
public ProductOptions:  any[] = [];

    // Define range filters
    public QuantityFrom: number | undefined;
    public QuantityTo: number | undefined;

    isLoading: boolean = false;
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
        target = "/Store/list";
        (await this.parent.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.StoreOptions = response.items;
                this.isLoading = false;
            },
                error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching Store data.';
                this.isLoading = false;
            }
        });
target = "/Operation/list";
        (await this.parent.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.OperationOptions = response.items;
                this.isLoading = false;
            },
                error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching Operation data.';
                this.isLoading = false;
            }
        });
target = "/Product/list";
        (await this.parent.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.ProductOptions = response.items;
                this.isLoading = false;
            },
                error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching Product data.';
                this.isLoading = false;
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

