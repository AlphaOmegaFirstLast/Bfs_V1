
import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { IQueryResponse, ILookup } from '@bfs/_shared/interfaces';
import { IStrTransactionFilter } from './str-transaction.shared';

@Component({
    selector: 'app-str-transaction-filter',
    imports: [FormsModule, CommonModule],
    templateUrl: './str-transaction.filter.component.html'
    //styles: ``
})
export class StrTransactionFilterComponent implements OnInit {

    public result = {} as IStrTransactionFilter;

    // Define look ups
    public StrStoreOptions:  any[] = [];
public StrOperationOptions:  any[] = [];
public StrProductOptions:  any[] = [];

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
        target = "/StrStore/list";
        (await this.parent.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.StrStoreOptions = response.items;
                this.isLoading = false;
            },
                error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching Store data.';
                this.isLoading = false;
            }
        });
target = "/StrOperation/list";
        (await this.parent.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.StrOperationOptions = response.items;
                this.isLoading = false;
            },
                error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching Operation data.';
                this.isLoading = false;
            }
        });
target = "/StrProduct/list";
        (await this.parent.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.StrProductOptions = response.items;
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