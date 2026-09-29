
import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { IQueryResponse, ILookup } from '@bfs/_shared/interfaces';
import { IAutoComplete, AutoCompleteHelper } from '@bfs/_shared/helpers/auto-complete.class';

import { ISsPortfolioFilter } from './ss-portfolio.shared';
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

@Component({
    selector: 'app-ss-portfolio-filter',
    imports: [FormsModule, CommonModule],
    templateUrl: './ss-portfolio.filter.component.html'
    //styles: ``
})
export class SsPortfolioFilterComponent implements OnInit {

    public result = {} as ISsPortfolioFilter;

    // Define look ups

    // Define autocomplete fields
    brokerAuto: AutoCompleteHelper;
investorAuto: AutoCompleteHelper;

    // Define range filters

    public isLoading: any = { list: false, view: false, save: false, lookups: false, autoComplete: false };
    public submit: boolean = false;
    public errorMessage: string = '';
    public infoMessage: string = '';
    public currentOperation: string = '';
    public parent: any;
    //---------------------------------------------------------
    constructor(public activeModal: NgbActiveModal) {
    this.brokerAuto = new AutoCompleteHelper({ queryUrl: "/Broker/list", fieldName: 'Broker', control: null, id: '', name: '', showDropDown: false, options: [], isLoading: false, isInitial: true } as IAutoComplete);
this.investorAuto = new AutoCompleteHelper({ queryUrl: "/Investor/list", fieldName: 'Investor', control: null, id: '', name: '', showDropDown: false, options: [], isLoading: false, isInitial: true } as IAutoComplete);

    }

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
    this.brokerAuto.setupFilter(this.parent.apiService, this.result);
this.investorAuto.setupFilter(this.parent.apiService, this.result);

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
//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

}