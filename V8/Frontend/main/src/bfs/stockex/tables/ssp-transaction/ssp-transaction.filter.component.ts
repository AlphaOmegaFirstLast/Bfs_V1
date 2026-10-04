
import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { FlatpickrDirective, provideFlatpickrDefaults } from 'angularx-flatpickr';
import { IQueryResponse, ILookup } from '@bfs/_shared/interfaces';
import { IAutoComplete, AutoCompleteHelper } from '@bfs/_shared/helpers/auto-complete.class';

import { ISspTransactionFilter } from './ssp-transaction.shared';
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

@Component({
    selector: 'app-ssp-transaction-filter',
    imports: [FormsModule, CommonModule, FlatpickrDirective],
    templateUrl: './ssp-transaction.filter.component.html',
    providers: [provideFlatpickrDefaults()],
    //styles: ``
})
export class SspTransactionFilterComponent implements OnInit {

    public result = {} as ISspTransactionFilter;

    // Define look ups
    public TransactionTypeOptions:  any[] = [];
public StockShareOptions:  any[] = [];
public CurrencyOptions:  any[] = [];

    // Define autocomplete fields
    ssPortfolioAuto: AutoCompleteHelper;
toPortfolioAuto: AutoCompleteHelper;

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

    public isLoading: any = { list: false, view: false, save: false, lookups: false, autoComplete: false };
    public submit: boolean = false;
    public errorMessage: string = '';
    public infoMessage: string = '';
    public currentOperation: string = '';
    public parent: any;
    //---------------------------------------------------------
    constructor(public activeModal: NgbActiveModal) {
    this.ssPortfolioAuto = new AutoCompleteHelper({ queryUrl: "/SsPortfolio/list", fieldName: 'SsPortfolio', control: null, id: '', name: '', showDropDown: false, options: [], isLoading: false, isInitial: true } as IAutoComplete);
this.toPortfolioAuto = new AutoCompleteHelper({ queryUrl: "/ToPortfolio/list", fieldName: 'ToPortfolio', control: null, id: '', name: '', showDropDown: false, options: [], isLoading: false, isInitial: true } as IAutoComplete);

    }

    async ngOnInit(): Promise<void> {
        this.result = this.parent.queryRequest.filter || {};
        await this.getLookups();
        await this.setAutoComplete();
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
this.toPortfolioAuto.setupFilter(this.parent.apiService, this.result);

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
//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

}