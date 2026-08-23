
import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { IQueryResponse, ILookup } from '@bfs/_shared/interfaces';
import { IInvestorBrokerFundFilter } from './investor-broker-fund.shared';

@Component({
    selector: 'app-investor-broker-fund-filter',
    imports: [FormsModule, CommonModule],
    templateUrl: './investor-broker-fund.filter.component.html'
    //styles: ``
})
export class InvestorBrokerFundFilterComponent implements OnInit {

    public result = {} as IInvestorBrokerFundFilter;

    // Define look ups
    public BrokerOptions:  any[] = [];
public InvestorOptions:  any[] = [];

    // Define range filters
    public FundFrom: number | undefined;
    public FundTo: number | undefined;
public FundDateFrom: Date | null | undefined;
    public FundDateTo: Date | null | undefined;

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
        this.FundFrom = this.result.Fund?.from;
        this.FundTo   = this.result.Fund?.to;
this.FundDateFrom = this.result.FundDate?.from;
        this.FundDateTo   = this.result.FundDate?.to;

    }
    //---------------------------------------------------------
    async getLookups(): Promise<void> {
        let target = '';
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
        this.result.Fund = { from: this.FundFrom, to: this.FundTo };
this.result.FundDate = { from: this.FundDateFrom, to: this.FundDateTo };

        this.parent.applyFilter(this.result);
    }
}

