
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
public InvestorOptions:  any[] = [];
public BrokerOptions:  any[] = [];
public CurrencyOptions:  any[] = [];

    // Define range filters

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
target = "/Investor/list";
        (await this.parent.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.InvestorOptions = response.items;
                this.isLoading = false;
            },
                error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching Investor data.';
                this.isLoading = false;
            }
        });
target = "/Broker/list";
        (await this.parent.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.BrokerOptions = response.items;
                this.isLoading = false;
            },
                error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching Broker data.';
                this.isLoading = false;
            }
        });
target = "/Currency/list";
        (await this.parent.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.CurrencyOptions = response.items;
                this.isLoading = false;
            },
                error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching Currency data.';
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

        this.parent.applyFilter(this.result);
    }
}