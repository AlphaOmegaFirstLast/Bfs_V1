
import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { IQueryResponse, ILookup } from '@bfs/_shared/interfaces';
import { IOverdraftPortfolioFilter } from './overdraft-portfolio.shared';

@Component({
    selector: 'app-overdraft-portfolio-filter',
    imports: [FormsModule, CommonModule],
    templateUrl: './overdraft-portfolio.filter.component.html'
    //styles: ``
})
export class OverdraftPortfolioFilterComponent implements OnInit {

    public result = {} as IOverdraftPortfolioFilter;

    // Define look ups
    public SsPortfolioOptions:  any[] = [];

    // Define range filters
    public OverdraftValueFrom: number | undefined;
    public OverdraftValueTo: number | undefined;

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
        this.OverdraftValueFrom = this.result.OverdraftValue?.from;
        this.OverdraftValueTo   = this.result.OverdraftValue?.to;

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
                this.errorMessage = err.message || 'An error occurred while fetching StockShare Portfolio data.';
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
        this.result.OverdraftValue = { from: this.OverdraftValueFrom, to: this.OverdraftValueTo };

        this.parent.applyFilter(this.result);
    }
}

