
import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { IQueryResponse, ILookup } from '@bfs/_shared/interfaces';
import { ISsPortfolioFilter } from './ss-portfolio.shared';

@Component({
    selector: 'app-ss-portfolio-filter',
    imports: [FormsModule, CommonModule],
    templateUrl: './ss-portfolio.filter.component.html'
    //styles: ``
})
export class SsPortfolioFilterComponent implements OnInit {

    public result = {} as ISsPortfolioFilter;

    // Define look ups

    showBroker = false; // Toggle for the overlay
    brokerOptions: any[] = [];
    brokerControl: any;
showInvestor = false; // Toggle for the overlay
    investorOptions: any[] = [];
    investorControl: any;

    // Define range filters

    public isLoading: any = { list: false, view: false, save: false, lookups: false, autoComplete: false };
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
        await this.setAutoComplete();
        // Initialize range filters if not set

    }
    //---------------------------------------------------------
    async getLookups(): Promise<void> {
        let target = '';

    }
    //---------------------------------------------------------
    async setAutoComplete() {
    await this.brokerAutoComplete();
await this.investorAutoComplete();

    }
    //---------------------------------------------------------
    async brokerAutoComplete(searchTerm: string = this.result?.['brokerName'] ?? ''): Promise<void> {
        const term = (searchTerm ?? '').trim();
        this.showBroker = term.length >= 2;

        if (this.showBroker) {
            this.isLoading.autoComplete = true;
            try {
                if (term.length < 2) {
                    this.brokerOptions = [];
                    return;
                }

                const request = { pageSize: 20, filter: { name: term } };
                const response: any = await this.parent.apiService.postAutoComplete("/Broker/list", request);
                this.brokerOptions = response?.items ?? [];
            } catch (err: any) {
                this.errorMessage = err?.message ||  "Error fetching data";
                this.brokerOptions = [];
            } finally {
                this.isLoading.autoComplete = false;
            }
        }
    }

   //---------------------------------------------------------
async investorAutoComplete(searchTerm: string = this.result?.['investorName'] ?? ''): Promise<void> {
        const term = (searchTerm ?? '').trim();
        this.showInvestor = term.length >= 2;

        if (this.showInvestor) {
            this.isLoading.autoComplete = true;
            try {
                if (term.length < 2) {
                    this.investorOptions = [];
                    return;
                }

                const request = { pageSize: 20, filter: { name: term } };
                const response: any = await this.parent.apiService.postAutoComplete("/Investor/list", request);
                this.investorOptions = response?.items ?? [];
            } catch (err: any) {
                this.errorMessage = err?.message ||  "Error fetching data";
                this.investorOptions = [];
            } finally {
                this.isLoading.autoComplete = false;
            }
        }
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

