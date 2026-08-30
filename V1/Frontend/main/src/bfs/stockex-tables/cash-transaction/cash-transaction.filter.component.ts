
import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { IQueryResponse, ILookup } from '@bfs/_shared/interfaces';
import { ICashTransactionFilter } from './cash-transaction.shared';
import { debounceTime, distinctUntilChanged, filter, switchMap, finalize, mergeMap } from 'rxjs/operators';
//Template_Component_AutoComplete

@Component({
    selector: 'app-cash-transaction-filter',
    imports: [FormsModule, CommonModule],
    templateUrl: './cash-transaction.filter.component.html'
    //styles: ``
})
export class CashTransactionFilterComponent implements OnInit {

    public result = {} as ICashTransactionFilter;

    // Define look ups
    public SspTransactionOptions:  any[] = [];
public SsPortfolioOptions:  any[] = [];
public TransactionTypeOptions:  any[] = [];
public ExpensesTypeOptions:  any[] = [];
public CurrencyOptions:  any[] = [];

    // Define range filters
    public SourceDateFrom: Date | null | undefined;
    public SourceDateTo: Date | null | undefined;
public TransactionDateFrom: Date | null | undefined;
    public TransactionDateTo: Date | null | undefined;
public ValueFrom: number | undefined;
    public ValueTo: number | undefined;

    public isLoading: any = { list: false, view: false, save: false, lookups: false, autoComplete: false };
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
        await this.setAutoComplete();
        // Initialize range filters if not set
        this.SourceDateFrom = this.result.SourceDate?.from;
        this.SourceDateTo   = this.result.SourceDate?.to;
this.TransactionDateFrom = this.result.TransactionDate?.from;
        this.TransactionDateTo   = this.result.TransactionDate?.to;
this.ValueFrom = this.result.Value?.from;
        this.ValueTo   = this.result.Value?.to;

    }
    //---------------------------------------------------------
    async getLookups(): Promise<void> {
        let target = '';
        target = "/SspTransaction/list";
        (await this.parent.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.SspTransactionOptions = response.items;
                this.isLoading.list = false;
            },
                error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching StocksShare Transaction data.';
                this.isLoading.list = false;
            }
        });
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
target = "/ExpensesType/list";
        (await this.parent.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.ExpensesTypeOptions = response.items;
                this.isLoading.list = false;
            },
                error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching Expenses Type data.';
                this.isLoading.list = false;
            }
        });
target = "/Currency/list";
        (await this.parent.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.CurrencyOptions = response.items;
                this.isLoading.list = false;
            },
                error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching Currency data.';
                this.isLoading.list = false;
            }
        });

    }
    //---------------------------------------------------------
    async setAutoComplete() {

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
this.result.Value = { from: this.ValueFrom, to: this.ValueTo };

        this.parent.applyFilter(this.result);
    }
}

