
import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { IQueryResponse, ILookup } from '@bfs/_shared/interfaces';
import { INavReportCompareFilter } from './nav-report-compare.shared';

@Component({
    selector: 'app-nav-report-compare-report-filter',
    imports: [FormsModule, CommonModule],
    templateUrl: './nav-report-compare.filter.component.html'
    //styles: ``
})
export class NavReportCompareFilterComponent implements OnInit {

    public result = {} as INavReportCompareFilter;

    // Define look ups

    // Define range filters

    public stockValueFrom: number | undefined;
    public stockValueTo: number | undefined;
    public cashFrom: string | undefined;
    public cashTo: string | undefined;

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

        this.stockValueFrom = this.result.stockValue?.from;
        this.stockValueTo = this.result.stockValue?.to;
        this.cashFrom = this.result.cash?.from;
        this.cashTo = this.result.cash?.to;

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

        this.result.stockValue = { from: this.stockValueFrom, to: this.stockValueTo };
        this.result.cash = { from: this.cashFrom, to: this.cashTo };

        this.parent.applyFilter(this.result);
    }
}

