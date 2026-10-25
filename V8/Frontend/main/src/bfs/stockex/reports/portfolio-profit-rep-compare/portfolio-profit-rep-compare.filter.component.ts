
import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { IQueryResponse, ILookup } from '@bfs/_shared/interfaces';
import { IPortfolioProfitRepCompareFilter } from './portfolio-profit-rep-compare.shared';

@Component({
    selector: 'app-portfolio-profit-rep-compare-report-filter',
    imports: [FormsModule, CommonModule],
    templateUrl: './portfolio-profit-rep-compare.filter.component.html'
    //styles: ``
})
export class PortfolioProfitRepCompareFilterComponent implements OnInit {

    public result = {} as IPortfolioProfitRepCompareFilter;

    // Define look ups
    public SsPortfolioOptions:  any[] = [];
public StockShareOptions:  any[] = [];
public CurrencyOptions:  any[] = [];

    // Define range filters
    public CurrentPrice_ProfitFrom: number | undefined;
    public CurrentPrice_ProfitTo: number | undefined;

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
        this.CurrentPrice_ProfitFrom = this.result.CurrentPrice_Profit?.from;
        this.CurrentPrice_ProfitTo   = this.result.CurrentPrice_Profit?.to;

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
target = "/StockShare/list";
        (await this.parent.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.StockShareOptions = response.items;
                this.isLoading = false;
            },
                error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching StockShare  data.';
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
        this.result.CurrentPrice_Profit = { from: this.CurrentPrice_ProfitFrom, to: this.CurrentPrice_ProfitTo };

        this.parent.applyFilter(this.result);
    }
}