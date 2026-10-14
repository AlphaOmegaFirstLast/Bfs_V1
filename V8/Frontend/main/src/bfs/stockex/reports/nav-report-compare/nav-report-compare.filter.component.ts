
import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { IQueryResponse, ILookup } from '@bfs/_shared/interfaces';
import { INavReportCompareFilter } from './nav-report-compare.shared';
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1
@Component({
    selector: 'app-nav-report-compare-report-filter',
    imports: [FormsModule, CommonModule],
    templateUrl: './nav-report-compare.filter.component.html'
    //styles: ``
})
export class NavReportCompareFilterComponent implements OnInit {

    public result = {} as INavReportCompareFilter;

    // Define look ups
    public InvestorOptions: any[] = [];
    public BrokerOptions: any[] = [];
    public PortfolioOptions: any[] = [];

    // Define range filters

    public sumNavFrom: number | undefined;
    public sumNavTo: number | undefined;

    isLoading: boolean = false;
    public submit: boolean = false;
    public errorMessage: string = '';
    public infoMessage: string = '';
    public currentOperation: string = '';
    public parent: any;
//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2
    //---------------------------------------------------------
    constructor(public activeModal: NgbActiveModal) {
        //Template_Start_Code_DontOverwrite_3
        //Template_End_Code_DontOverwrite_3
     }
    //---------------------------------------------------------

    async ngOnInit(): Promise<void> {
        this.result = this.parent.queryRequest.filter || {};
        await this.getLookups();
        // Initialize range filters if not set

        this.sumNavFrom = this.result.sumNav?.from;
        this.sumNavTo = this.result.sumNav?.to;
//Template_Start_Code_DontOverwrite_3
//Template_End_Code_DontOverwrite_3
    }
    //---------------------------------------------------------
    async getLookups(): Promise<void> {
        let target = '';
        target = "/Investor/list";
        (await this.parent.apiService.post(target, { pageSize: 50 })).subscribe({
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
        (await this.parent.apiService.post(target, { pageSize: 50 })).subscribe({
            next: (response: IQueryResponse) => {
                this.BrokerOptions = response.items;
                this.isLoading = false;
            },
            error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching Broker data.';
                this.isLoading = false;
            }
        });
        target = "/ssPortfolio/list";
        (await this.parent.apiService.post(target, { pageSize: 50 })).subscribe({
            next: (response: IQueryResponse) => {
                this.PortfolioOptions = response.items;
                this.isLoading = false;
            },
            error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching Portfolio of Investor & Broker data.';
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

        this.result.sumNav = { from: this.sumNavFrom, to: this.sumNavTo };

        this.parent.applyFilter(this.result);
    }
        //---------------------------------------------------------
//Template_Start_Code_DontOverwrite_4
// this func. is called in html into theses 2 lines
// <select class="form-select" [(ngModel)]="result.SsPortfolio_InvestorId" name="investorId" (change)="filterChange()">
// <select class="form-select" [(ngModel)]="result.SsPortfolio_BrokerId" name="brokerId" (change)="filterChange()">
// Also add the [disabled] attribute to this line
// <select class="form-select" [(ngModel)]="result.SsPortfolio_PortfolioId" name="portfolioId" [disabled]="!(result.SsPortfolio_InvestorId) && !(result.SsPortfolio_BrokerId)">
 
    async filterChange() {
        let target = "/ssPortfolio/list";
        (await this.parent.apiService.post(target, { filter: { investorId: this.result.SsPortfolio_InvestorId, brokerId: this.result.SsPortfolio_BrokerId }, pageSize: 50 })).subscribe({
            next: (response: IQueryResponse) => {
                this.PortfolioOptions = response.items;
                this.isLoading = false;
            },
            error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching Portfolio of Investor & Broker data.';
                this.isLoading = false;
            }
        });
    }
//Template_End_Code_DontOverwrite_4
}