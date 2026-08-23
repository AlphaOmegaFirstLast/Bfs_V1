
import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { IQueryResponse, ILookup } from '@bfs/_shared/interfaces';
import { ITransactionTypeFilter } from './transaction-type.shared';

@Component({
    selector: 'app-transaction-type-filter',
    imports: [FormsModule, CommonModule],
    templateUrl: './transaction-type.filter.component.html'
    //styles: ``
})
export class TransactionTypeFilterComponent implements OnInit {

    public result = {} as ITransactionTypeFilter;

    // Define look ups
    public EffectTypeOptions:  any[] = [];
public StockEntityTypeOptions:  any[] = [];
public CalculationMethodOptions:  any[] = [];
public SourceTypeOptions:  any[] = [];
public StockFieldTypeOptions:  any[] = [];
public NextTransactionTypeOptions:  any[] = [];

    // Define range filters

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

    }
    //---------------------------------------------------------
    async getLookups(): Promise<void> {
        let target = '';
        target = "/EffectType/list";
        (await this.parent.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.EffectTypeOptions = response.items;
                this.isLoading.list = false;
            },
                error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching Effect Type data.';
                this.isLoading.list = false;
            }
        });
target = "/StockEntityType/list";
        (await this.parent.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.StockEntityTypeOptions = response.items;
                this.isLoading.list = false;
            },
                error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching Applicable To Entity data.';
                this.isLoading.list = false;
            }
        });
target = "/CalculationMethod/list";
        (await this.parent.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.CalculationMethodOptions = response.items;
                this.isLoading.list = false;
            },
                error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching Calculation Method data.';
                this.isLoading.list = false;
            }
        });
target = "/SourceType/list";
        (await this.parent.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.SourceTypeOptions = response.items;
                this.isLoading.list = false;
            },
                error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching Source Type data.';
                this.isLoading.list = false;
            }
        });
target = "/StockFieldType/list";
        (await this.parent.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.StockFieldTypeOptions = response.items;
                this.isLoading.list = false;
            },
                error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching Applicable To Field data.';
                this.isLoading.list = false;
            }
        });
target = "/NextTransactionType/list";
        (await this.parent.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.NextTransactionTypeOptions = response.items;
                this.isLoading.list = false;
            },
                error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching Next Transaction Type data.';
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

        this.parent.applyFilter(this.result);
    }
}

