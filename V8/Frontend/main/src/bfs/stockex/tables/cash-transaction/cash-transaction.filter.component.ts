
import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { LucideAngularModule, LucideSearch } from 'lucide-angular';
import { FlatpickrDirective, provideFlatpickrDefaults } from 'angularx-flatpickr';

import { IQueryResponse, ILookup } from '@bfs/_shared/interfaces';
import { IAutoComplete, AutoCompleteHelper } from '@bfs/_shared/helpers/auto-complete.class';

import { ICashTransactionFilter } from './cash-transaction.shared';
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

@Component({
    selector: 'app-cash-transaction-filter',
    imports: [LucideAngularModule, FormsModule, CommonModule
, FlatpickrDirective

    ],
    templateUrl: './cash-transaction.filter.component.html',
    providers: [
  provideFlatpickrDefaults()

    ],
  //styles: ``
})
export class CashTransactionFilterComponent implements OnInit {

    public result = {} as ICashTransactionFilter;
    protected readonly LucideSearch = LucideSearch;

    // Define look ups
    public SspTransactionOptions:  any[] = [];
public TransactionTypeOptions:  any[] = [];
public ExpensesTypeOptions:  any[] = [];
public CurrencyOptions:  any[] = [];

    // Define autocomplete fields
    ssPortfolioAuto: AutoCompleteHelper;

    // Define range filters
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
//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

    //---------------------------------------------------------
    constructor(public activeModal: NgbActiveModal) {
    this.ssPortfolioAuto = new AutoCompleteHelper({ queryUrl: "/SsPortfolio/list", fieldName: 'SsPortfolio', control: null, id: '', name: '', showDropDown: false, options: [], isLoading: false, isInitial: true } as IAutoComplete);

//Template_Start_Code_DontOverwrite_3
//Template_End_Code_DontOverwrite_3

    }

    async ngOnInit(): Promise<void> {
        this.result = this.parent.queryRequest.filter || {};
        await this.getLookups();
        await this.setAutoComplete();
        // Initialize range filters if not set
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
    this.ssPortfolioAuto.setupFilter(this.parent.apiService, this.result);

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
        this.result.TransactionDate = { from: this.TransactionDateFrom, to: this.TransactionDateTo };
this.result.Value = { from: this.ValueFrom, to: this.ValueTo };

        this.parent.applyFilter(this.result);
    }
//Template_Start_Code_DontOverwrite_4
//Template_End_Code_DontOverwrite_4
}