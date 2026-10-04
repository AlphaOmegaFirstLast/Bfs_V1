
import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { FlatpickrDirective, provideFlatpickrDefaults } from 'angularx-flatpickr';
import { IQueryResponse, ILookup } from '@bfs/_shared/interfaces';

import { IOverdraftPortfolioFilter } from './overdraft-portfolio.shared';
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

@Component({
    selector: 'app-overdraft-portfolio-filter',
    imports: [FormsModule, CommonModule, FlatpickrDirective],
    templateUrl: './overdraft-portfolio.filter.component.html',
    providers: [provideFlatpickrDefaults()],
    //styles: ``
})
export class OverdraftPortfolioFilterComponent implements OnInit {

    public result = {} as IOverdraftPortfolioFilter;

    // Define look ups
    public SsPortfolioOptions:  any[] = [];
public BrokerAgreementOptions:  any[] = [];

    // Define autocomplete fields

    // Define range filters
    public OverdraftPercentFrom: number | undefined;
    public OverdraftPercentTo: number | undefined;
public OverdraftMaxFrom: number | undefined;
    public OverdraftMaxTo: number | undefined;

    public isLoading: any = { list: false, view: false, save: false, lookups: false, autoComplete: false };
    public submit: boolean = false;
    public errorMessage: string = '';
    public infoMessage: string = '';
    public currentOperation: string = '';
    public parent: any;
    //---------------------------------------------------------
    constructor(public activeModal: NgbActiveModal) {

    }

    async ngOnInit(): Promise<void> {
        this.result = this.parent.queryRequest.filter || {};
        await this.getLookups();
        await this.setAutoComplete();
        // Initialize range filters if not set
        this.OverdraftPercentFrom = this.result.OverdraftPercent?.from;
        this.OverdraftPercentTo   = this.result.OverdraftPercent?.to;
this.OverdraftMaxFrom = this.result.OverdraftMax?.from;
        this.OverdraftMaxTo   = this.result.OverdraftMax?.to;

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
target = "/BrokerAgreement/list";
        (await this.parent.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.BrokerAgreementOptions = response.items;
                this.isLoading.list = false;
            },
                error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching Broker Agreement data.';
                this.isLoading.list = false;
            }
        });

    }
    //---------------------------------------------------------
    async setAutoComplete() {

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
        this.result.OverdraftPercent = { from: this.OverdraftPercentFrom, to: this.OverdraftPercentTo };
this.result.OverdraftMax = { from: this.OverdraftMaxFrom, to: this.OverdraftMaxTo };

        this.parent.applyFilter(this.result);
    }
//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

}