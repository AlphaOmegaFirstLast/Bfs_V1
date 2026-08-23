
import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { IQueryResponse, ILookup } from '@bfs/_shared/interfaces';
import { IBrokerAgreementFilter } from './broker-agreement.shared';

@Component({
    selector: 'app-broker-agreement-filter',
    imports: [FormsModule, CommonModule],
    templateUrl: './broker-agreement.filter.component.html'
    //styles: ``
})
export class BrokerAgreementFilterComponent implements OnInit {

    public result = {} as IBrokerAgreementFilter;

    // Define look ups
    public InvestorOptions:  any[] = [];
public BrokerOptions:  any[] = [];
public SsPortfolioOptions:  any[] = [];

    // Define range filters
    public AgreementDateFrom: Date | null | undefined;
    public AgreementDateTo: Date | null | undefined;
public OverdraftPrcntFrom: number | undefined;
    public OverdraftPrcntTo: number | undefined;
public OverdraftMxFrom: number | undefined;
    public OverdraftMxTo: number | undefined;

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
        this.AgreementDateFrom = this.result.AgreementDate?.from;
        this.AgreementDateTo   = this.result.AgreementDate?.to;
this.OverdraftPrcntFrom = this.result.OverdraftPrcnt?.from;
        this.OverdraftPrcntTo   = this.result.OverdraftPrcnt?.to;
this.OverdraftMxFrom = this.result.OverdraftMx?.from;
        this.OverdraftMxTo   = this.result.OverdraftMx?.to;

    }
    //---------------------------------------------------------
    async getLookups(): Promise<void> {
        let target = '';
        target = "/Investor/list";
        (await this.parent.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.InvestorOptions = response.items;
                this.isLoading.list = false;
            },
                error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching Investor data.';
                this.isLoading.list = false;
            }
        });
target = "/Broker/list";
        (await this.parent.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.BrokerOptions = response.items;
                this.isLoading.list = false;
            },
                error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching Broker data.';
                this.isLoading.list = false;
            }
        });
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
        this.result.AgreementDate = { from: this.AgreementDateFrom, to: this.AgreementDateTo };
this.result.OverdraftPrcnt = { from: this.OverdraftPrcntFrom, to: this.OverdraftPrcntTo };
this.result.OverdraftMx = { from: this.OverdraftMxFrom, to: this.OverdraftMxTo };

        this.parent.applyFilter(this.result);
    }
}

