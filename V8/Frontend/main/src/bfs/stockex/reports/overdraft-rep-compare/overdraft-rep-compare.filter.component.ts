
import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { IQueryResponse, ILookup } from '@bfs/_shared/interfaces';
import { IOverdraftRepCompareFilter } from './overdraft-rep-compare.shared';
import { FlatpickrDirective, provideFlatpickrDefaults } from 'angularx-flatpickr';

@Component({
    selector: 'app-overdraft-rep-compare-report-filter',
    imports: [FlatpickrDirective, FormsModule, CommonModule],
    templateUrl: './overdraft-rep-compare.filter.component.html'
    //styles: ``
})
export class OverdraftRepCompareFilterComponent implements OnInit {

    public result = {} as IOverdraftRepCompareFilter;

    // Define look ups
    public BrokerAgreementOptions:  any[] = [];
public SsPortfolioOptions:  any[] = [];
public InvestorOptions:  any[] = [];
public BrokerOptions:  any[] = [];
public CurrencyOptions:  any[] = [];

    // Define range filters
    public BrokerAgreement_AgreementDateFrom: Date | null | undefined;
    public BrokerAgreement_AgreementDateTo: Date | null | undefined;
public OverdraftPortfolio_OverdraftFrom: number | undefined;
    public OverdraftPortfolio_OverdraftTo: number | undefined;
public OverdraftPortfolio_OverdraftMaxFrom: number | undefined;
    public OverdraftPortfolio_OverdraftMaxTo: number | undefined;
public OverdraftPortfolio_OverdraftPercentFrom: number | undefined;
    public OverdraftPortfolio_OverdraftPercentTo: number | undefined;

    public sumTotalCashDepositFrom: number | undefined;
    public sumTotalCashDepositTo: number | undefined;
public sumAvailableCreditFrom: number | undefined;
    public sumAvailableCreditTo: number | undefined;

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
        this.BrokerAgreement_AgreementDateFrom = this.result.BrokerAgreement_AgreementDate?.from;
        this.BrokerAgreement_AgreementDateTo   = this.result.BrokerAgreement_AgreementDate?.to;
this.OverdraftPortfolio_OverdraftFrom = this.result.OverdraftPortfolio_Overdraft?.from;
        this.OverdraftPortfolio_OverdraftTo   = this.result.OverdraftPortfolio_Overdraft?.to;
this.OverdraftPortfolio_OverdraftMaxFrom = this.result.OverdraftPortfolio_OverdraftMax?.from;
        this.OverdraftPortfolio_OverdraftMaxTo   = this.result.OverdraftPortfolio_OverdraftMax?.to;
this.OverdraftPortfolio_OverdraftPercentFrom = this.result.OverdraftPortfolio_OverdraftPercent?.from;
        this.OverdraftPortfolio_OverdraftPercentTo   = this.result.OverdraftPortfolio_OverdraftPercent?.to;

        this.sumTotalCashDepositFrom = this.result.sumTotalCashDeposit?.from;
        this.sumTotalCashDepositTo   = this.result.sumTotalCashDeposit?.to;
this.sumAvailableCreditFrom = this.result.sumAvailableCredit?.from;
        this.sumAvailableCreditTo   = this.result.sumAvailableCredit?.to;

    }
    //---------------------------------------------------------
    async getLookups(): Promise<void> {
        let target = '';
        target = "/BrokerAgreement/list";
        (await this.parent.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.BrokerAgreementOptions = response.items;
                this.isLoading = false;
            },
                error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching Broker Agreement ID data.';
                this.isLoading = false;
            }
        });
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
        this.result.BrokerAgreement_AgreementDate = { from: this.BrokerAgreement_AgreementDateFrom, to: this.BrokerAgreement_AgreementDateTo };
this.result.OverdraftPortfolio_Overdraft = { from: this.OverdraftPortfolio_OverdraftFrom, to: this.OverdraftPortfolio_OverdraftTo };
this.result.OverdraftPortfolio_OverdraftMax = { from: this.OverdraftPortfolio_OverdraftMaxFrom, to: this.OverdraftPortfolio_OverdraftMaxTo };
this.result.OverdraftPortfolio_OverdraftPercent = { from: this.OverdraftPortfolio_OverdraftPercentFrom, to: this.OverdraftPortfolio_OverdraftPercentTo };

        this.result.sumTotalCashDeposit = { from: this.sumTotalCashDepositFrom, to: this.sumTotalCashDepositTo };
this.result.sumAvailableCredit = { from: this.sumAvailableCreditFrom, to: this.sumAvailableCreditTo };

        this.parent.applyFilter(this.result);
    }
}