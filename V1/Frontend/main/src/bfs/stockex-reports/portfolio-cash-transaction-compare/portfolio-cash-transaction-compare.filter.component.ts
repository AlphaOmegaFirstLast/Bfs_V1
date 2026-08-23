
import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { IQueryResponse, ILookup } from '@bfs/_shared/interfaces';
import { IPortfolioCashTransactionCompareFilter } from './portfolio-cash-transaction-compare.shared';

@Component({
    selector: 'app-portfolio-cash-transaction-compare-report-filter',
    imports: [FormsModule, CommonModule],
    templateUrl: './portfolio-cash-transaction-compare.filter.component.html'
    //styles: ``
})
export class PortfolioCashTransactionCompareFilterComponent implements OnInit {

    public result = {} as IPortfolioCashTransactionCompareFilter;

    // Define look ups

    // Define range filters
    public CashTransaction_ValueFrom: number | undefined;
    public CashTransaction_ValueTo: number | undefined;
public CashTransaction_TransactionDateFrom: Date | null | undefined;
    public CashTransaction_TransactionDateTo: Date | null | undefined;

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
        this.CashTransaction_ValueFrom = this.result.CashTransaction_Value?.from;
        this.CashTransaction_ValueTo   = this.result.CashTransaction_Value?.to;
this.CashTransaction_TransactionDateFrom = this.result.CashTransaction_TransactionDate?.from;
        this.CashTransaction_TransactionDateTo   = this.result.CashTransaction_TransactionDate?.to;

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
        this.result.CashTransaction_Value = { from: this.CashTransaction_ValueFrom, to: this.CashTransaction_ValueTo };
this.result.CashTransaction_TransactionDate = { from: this.CashTransaction_TransactionDateFrom, to: this.CashTransaction_TransactionDateTo };

        this.parent.applyFilter(this.result);
    }
}