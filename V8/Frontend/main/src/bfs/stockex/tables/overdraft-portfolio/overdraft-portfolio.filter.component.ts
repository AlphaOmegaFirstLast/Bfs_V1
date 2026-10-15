
import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { LucideAngularModule, LucideSearch } from 'lucide-angular';

import { IQueryResponse, ILookup } from '@bfs/_shared/interfaces';
import { IAutoComplete, AutoCompleteHelper } from '@bfs/_shared/helpers/auto-complete.class';

import { IOverdraftPortfolioFilter } from './overdraft-portfolio.shared';
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

@Component({
    selector: 'app-overdraft-portfolio-filter',
    imports: [LucideAngularModule, FormsModule, CommonModule

    ],
    templateUrl: './overdraft-portfolio.filter.component.html',
    providers: [

    ],
  //styles: ``
})
export class OverdraftPortfolioFilterComponent implements OnInit {

    public result = {} as IOverdraftPortfolioFilter;
    protected readonly LucideSearch = LucideSearch;

    // Define look ups
    public SsPortfolioOptions:  any[] = [];

    // Define autocomplete fields
    brokerAgreementAuto: AutoCompleteHelper;

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
//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

    //---------------------------------------------------------
    constructor(public activeModal: NgbActiveModal) {
    this.brokerAgreementAuto = new AutoCompleteHelper({ queryUrl: "/BrokerAgreement/list", fieldName: 'BrokerAgreement', control: null, id: '', name: '', showDropDown: false, options: [], isLoading: false, isInitial: true } as IAutoComplete);

//Template_Start_Code_DontOverwrite_3
//Template_End_Code_DontOverwrite_3

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

    }
    //---------------------------------------------------------
    async setAutoComplete() {
    this.brokerAgreementAuto.setupFilter(this.parent.apiService, this.result);

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
//Template_Start_Code_DontOverwrite_4
//Template_End_Code_DontOverwrite_4
}