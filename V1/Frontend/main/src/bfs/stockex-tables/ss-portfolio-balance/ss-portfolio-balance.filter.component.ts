
import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { IQueryResponse, ILookup } from '@bfs/_shared/interfaces';
import { ISsPortfolioBalanceFilter } from './ss-portfolio-balance.shared';

@Component({
    selector: 'app-ss-portfolio-balance-filter',
    imports: [FormsModule, CommonModule],
    templateUrl: './ss-portfolio-balance.filter.component.html'
    //styles: ``
})
export class SsPortfolioBalanceFilterComponent implements OnInit {

    public result = {} as ISsPortfolioBalanceFilter;

    // Define look ups
    public SsPortfolioOptions:  any[] = [];

    // Define range filters
    public BalanceFrom: number | undefined;
    public BalanceTo: number | undefined;

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
        this.BalanceFrom = this.result.Balance?.from;
        this.BalanceTo   = this.result.Balance?.to;

    }
    //---------------------------------------------------------
    async getLookups(): Promise<void> {
        let target = '';
        target = "/SsPortfolio/list";
        (await this.parent.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.SsPortfolioOptions = response.items;
                this.isLoading.list = false;
            },
                error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching  Portfolio data.';
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
        this.result.Balance = { from: this.BalanceFrom, to: this.BalanceTo };

        this.parent.applyFilter(this.result);
    }
}

