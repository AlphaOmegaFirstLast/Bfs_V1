
import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { IQueryResponse, ILookup } from '@bfs/_shared/interfaces';
import { IOverdraftRepCompareFilter } from './overdraft-rep-compare.shared';

@Component({
    selector: 'app-overdraft-rep-compare-report-filter',
    imports: [FormsModule, CommonModule],
    templateUrl: './overdraft-rep-compare.filter.component.html'
    //styles: ``
})
export class OverdraftRepCompareFilterComponent implements OnInit {

    public result = {} as IOverdraftRepCompareFilter;

    // Define look ups
    public SsPortfolioOptions:  any[] = [];
public BrokerAgreementOptions:  any[] = [];

    // Define range filters
    public OverdraftPortfolio_OverdraftPercentFrom: number | undefined;
    public OverdraftPortfolio_OverdraftPercentTo: number | undefined;
public OverdraftPortfolio_OverdraftMaxFrom: number | undefined;
    public OverdraftPortfolio_OverdraftMaxTo: number | undefined;

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
        this.OverdraftPortfolio_OverdraftPercentFrom = this.result.OverdraftPortfolio_OverdraftPercent?.from;
        this.OverdraftPortfolio_OverdraftPercentTo   = this.result.OverdraftPortfolio_OverdraftPercent?.to;
this.OverdraftPortfolio_OverdraftMaxFrom = this.result.OverdraftPortfolio_OverdraftMax?.from;
        this.OverdraftPortfolio_OverdraftMaxTo   = this.result.OverdraftPortfolio_OverdraftMax?.to;

    }
    //---------------------------------------------------------
    async getLookups(): Promise<void> {
        let target = '';
        target = "/SsPortfolio/list";
        (await this.parent.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.SsPortfolioOptions = response.items;
                this.isLoading = false;
            },
                error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching StockShare Portfolio data.';
                this.isLoading = false;
            }
        });
target = "/BrokerAgreement/list";
        (await this.parent.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.BrokerAgreementOptions = response.items;
                this.isLoading = false;
            },
                error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching Broker Agreement data.';
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
        this.result.OverdraftPortfolio_OverdraftPercent = { from: this.OverdraftPortfolio_OverdraftPercentFrom, to: this.OverdraftPortfolio_OverdraftPercentTo };
this.result.OverdraftPortfolio_OverdraftMax = { from: this.OverdraftPortfolio_OverdraftMaxFrom, to: this.OverdraftPortfolio_OverdraftMaxTo };

        this.parent.applyFilter(this.result);
    }
}