
import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { IQueryResponse, ILookup } from '@bfs/_shared/interfaces';
import { ISsPortfolioFilter } from './ss-portfolio.shared';
import { debounceTime, distinctUntilChanged, filter, switchMap, finalize, mergeMap } from 'rxjs/operators';
//Template_Component_AutoComplete

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
showInvestor = false; // Toggle for the overlay
    investorOptions: any[] = [];

    // Define range filters
    public InterestFrom: number | undefined;
    public InterestTo: number | undefined;

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
        this.InterestFrom = this.result.Interest?.from;
        this.InterestTo   = this.result.Interest?.to;

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

async brokerAutoComplete(searchTerm: string = this.result.BrokerName ?? ''): Promise<void> {
        const term = (searchTerm ?? '').trim();
        if (term.length < 2) {
            this.brokerOptions = [];
            this.showBroker = false;
            return;
        }

        this.showBroker = true;
        this.isLoading.autoComplete = true;
        try {
            const request = { pageSize: 20, filter: { name: term } };
            const response: any = await this.parent.apiService.postAutoComplete('/Broker/list', request);
            this.brokerOptions = response?.items ?? [];
        } catch (err: any) {
            this.errorMessage = err?.message || 'Error fetching data';
            this.brokerOptions = [];
        } finally {
            this.isLoading.autoComplete = false;
        }
    }
    //---------------------------------------------------------
    onBrokerInput(value: string): void {
        const val = value ?? '';
        // Reset selected ID
        this.result.BrokerName = undefined;
        this.result.BrokerId = undefined;
        this.brokerAutoComplete(val);
    }
    //---------------------------------------------------------
    selectBroker(selectedOption: any) {
        this.result.BrokerName = selectedOption?.name ?? undefined;
        this.result.BrokerId = selectedOption?.id ?? undefined;
        this.brokerOptions = [];
        this.showBroker = false;
    }
    //---------------------------------------------------------
    hideBrokerOverlay() {
        setTimeout(() => {
            this.showBroker = false;
        }, 200);
    }
    //---------------------------------------------------------
async investorAutoComplete(searchTerm: string = this.result.InvestorName ?? ''): Promise<void> {
        const term = (searchTerm ?? '').trim();
        if (term.length < 2) {
            this.investorOptions = [];
            this.showInvestor = false;
            return;
        }

        this.showInvestor = true;
        this.isLoading.autoComplete = true;
        try {
            const request = { pageSize: 20, filter: { name: term } };
            const response: any = await this.parent.apiService.postAutoComplete('/Investor/list', request);
            this.investorOptions = response?.items ?? [];
        } catch (err: any) {
            this.errorMessage = err?.message || 'Error fetching data';
            this.investorOptions = [];
        } finally {
            this.isLoading.autoComplete = false;
        }
    }
    //---------------------------------------------------------
    onInvestorInput(value: string): void {
        const val = value ?? '';
        // Reset selected ID
        this.result.InvestorName = undefined;
        this.result.InvestorId = undefined;
        this.investorAutoComplete(val);
    }
    //---------------------------------------------------------
    selectInvestor(selectedOption: any) {
        this.result.InvestorName = selectedOption?.name ?? undefined;
        this.result.InvestorId = selectedOption?.id ?? undefined;
        this.investorOptions = [];
        this.showInvestor = false;
    }
    //---------------------------------------------------------
    hideInvestorOverlay() {
        setTimeout(() => {
            this.showInvestor = false;
        }, 200);
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
        this.result.Interest = { from: this.InterestFrom, to: this.InterestTo };

        this.parent.applyFilter(this.result);
    }
}

