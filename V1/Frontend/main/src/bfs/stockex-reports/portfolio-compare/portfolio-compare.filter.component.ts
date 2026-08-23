
import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { IQueryResponse, ILookup } from '@bfs/_shared/interfaces';
import { IPortfolioCompareFilter } from './portfolio-compare.shared';

@Component({
    selector: 'app-portfolio-compare-report-filter',
    imports: [FormsModule, CommonModule],
    templateUrl: './portfolio-compare.filter.component.html'
    //styles: ``
})
export class PortfolioCompareFilterComponent implements OnInit {

    public result = {} as IPortfolioCompareFilter;

    // Define look ups

    // Define range filters
    public SspTransaction_QuantityFrom: number | undefined;
    public SspTransaction_QuantityTo: number | undefined;
public SspTransaction_PriceFrom: number | undefined;
    public SspTransaction_PriceTo: number | undefined;
public SspTransaction_TransactionDateFrom: Date | null | undefined;
    public SspTransaction_TransactionDateTo: Date | null | undefined;

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
        this.SspTransaction_QuantityFrom = this.result.SspTransaction_Quantity?.from;
        this.SspTransaction_QuantityTo   = this.result.SspTransaction_Quantity?.to;
this.SspTransaction_PriceFrom = this.result.SspTransaction_Price?.from;
        this.SspTransaction_PriceTo   = this.result.SspTransaction_Price?.to;
this.SspTransaction_TransactionDateFrom = this.result.SspTransaction_TransactionDate?.from;
        this.SspTransaction_TransactionDateTo   = this.result.SspTransaction_TransactionDate?.to;

    }
    //---------------------------------------------------------
    async getLookups(): Promise<void> {
        let target = '';

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
        this.result.SspTransaction_Quantity = { from: this.SspTransaction_QuantityFrom, to: this.SspTransaction_QuantityTo };
this.result.SspTransaction_Price = { from: this.SspTransaction_PriceFrom, to: this.SspTransaction_PriceTo };
this.result.SspTransaction_TransactionDate = { from: this.SspTransaction_TransactionDateFrom, to: this.SspTransaction_TransactionDateTo };

        this.parent.applyFilter(this.result);
    }
}

