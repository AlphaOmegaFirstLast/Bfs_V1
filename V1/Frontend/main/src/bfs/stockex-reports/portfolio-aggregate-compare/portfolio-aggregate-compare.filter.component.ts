
import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { IQueryResponse, ILookup } from '@bfs/_shared/interfaces';
import { IPortfolioAggregateCompareFilter } from './portfolio-aggregate-compare.shared';

@Component({
    selector: 'app-portfolio-aggregate-compare-report-filter',
    imports: [FormsModule, CommonModule],
    templateUrl: './portfolio-aggregate-compare.filter.component.html'
    //styles: ``
})
export class PortfolioAggregateCompareFilterComponent implements OnInit {

    public result = {} as IPortfolioAggregateCompareFilter;

    // Define look ups

    // Define range filters

    public sumQuantityFrom: number | undefined;
    public sumQuantityTo: number | undefined;
public sumPriceFrom: number | undefined;
    public sumPriceTo: number | undefined;

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

        this.sumQuantityFrom = this.result.sumQuantity?.from;
        this.sumQuantityTo   = this.result.sumQuantity?.to;
this.sumPriceFrom = this.result.sumPrice?.from;
        this.sumPriceTo   = this.result.sumPrice?.to;

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

        this.result.sumQuantity = { from: this.sumQuantityFrom, to: this.sumQuantityTo };
this.result.sumPrice = { from: this.sumPriceFrom, to: this.sumPriceTo };

        this.parent.applyFilter(this.result);
    }
}

