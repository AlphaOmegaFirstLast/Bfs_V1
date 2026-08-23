
import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { IQueryResponse, ILookup } from '@bfs/_shared/interfaces';
import { IPortfolioCashTransactionAggregateCompareFilter } from './portfolio-cash-transaction-aggregate-compare.shared';

@Component({
    selector: 'app-portfolio-cash-transaction-aggregate-compare-report-filter',
    imports: [FormsModule, CommonModule],
    templateUrl: './portfolio-cash-transaction-aggregate-compare.filter.component.html'
    //styles: ``
})
export class PortfolioCashTransactionAggregateCompareFilterComponent implements OnInit {

    public result = {} as IPortfolioCashTransactionAggregateCompareFilter;

    // Define look ups

    // Define range filters

    public sumValueFrom: number | undefined;
    public sumValueTo: number | undefined;

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

        this.sumValueFrom = this.result.sumValue?.from;
        this.sumValueTo   = this.result.sumValue?.to;

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

        this.result.sumValue = { from: this.sumValueFrom, to: this.sumValueTo };

        this.parent.applyFilter(this.result);
    }
}