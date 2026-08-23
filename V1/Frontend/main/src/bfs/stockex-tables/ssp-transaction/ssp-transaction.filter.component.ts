
import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { IQueryResponse, ILookup } from '@bfs/_shared/interfaces';
import { ISspTransactionFilter } from './ssp-transaction.shared';

@Component({
    selector: 'app-ssp-transaction-filter',
    imports: [FormsModule, CommonModule],
    templateUrl: './ssp-transaction.filter.component.html'
    //styles: ``
})
export class SspTransactionFilterComponent implements OnInit {

    public result = {} as ISspTransactionFilter;

    // Define look ups
    public SsPortfolioOptions:  any[] = [];
public TransactionTypeOptions:  any[] = [];
public StockShareOptions:  any[] = [];
public ToPortfolioOptions:  any[] = [];

    // Define range filters
    public SourceDateFrom: Date | null | undefined;
    public SourceDateTo: Date | null | undefined;
public TransactionDateFrom: Date | null | undefined;
    public TransactionDateTo: Date | null | undefined;
public QuantityFrom: number | undefined;
    public QuantityTo: number | undefined;
public PriceFrom: number | undefined;
    public PriceTo: number | undefined;
public ToQuantityFrom: number | undefined;
    public ToQuantityTo: number | undefined;

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
        this.SourceDateFrom = this.result.SourceDate?.from;
        this.SourceDateTo   = this.result.SourceDate?.to;
this.TransactionDateFrom = this.result.TransactionDate?.from;
        this.TransactionDateTo   = this.result.TransactionDate?.to;
this.QuantityFrom = this.result.Quantity?.from;
        this.QuantityTo   = this.result.Quantity?.to;
this.PriceFrom = this.result.Price?.from;
        this.PriceTo   = this.result.Price?.to;
this.ToQuantityFrom = this.result.ToQuantity?.from;
        this.ToQuantityTo   = this.result.ToQuantity?.to;

    }
    //---------------------------------------------------------
    async getLookups(): Promise<void> {
        let target = '';
        target = "/SsPortfolio/list";
        (await this.parent.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.SsPortfolioOptions = response.items;
                this.isLoading.list = false;
            },
                error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching StockShare Portfolio data.';
                this.isLoading.list = false;
            }
        });
target = "/TransactionType/list";
        (await this.parent.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.TransactionTypeOptions = response.items;
                this.isLoading.list = false;
            },
                error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching Transaction Type data.';
                this.isLoading.list = false;
            }
        });
target = "/StockShare/list";
        (await this.parent.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.StockShareOptions = response.items;
                this.isLoading.list = false;
            },
                error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching Stock Share data.';
                this.isLoading.list = false;
            }
        });
target = "/ToPortfolio/list";
        (await this.parent.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.ToPortfolioOptions = response.items;
                this.isLoading.list = false;
            },
                error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching To Portfolio data.';
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
        this.result.SourceDate = { from: this.SourceDateFrom, to: this.SourceDateTo };
this.result.TransactionDate = { from: this.TransactionDateFrom, to: this.TransactionDateTo };
this.result.Quantity = { from: this.QuantityFrom, to: this.QuantityTo };
this.result.Price = { from: this.PriceFrom, to: this.PriceTo };
this.result.ToQuantity = { from: this.ToQuantityFrom, to: this.ToQuantityTo };

        this.parent.applyFilter(this.result);
    }
}

