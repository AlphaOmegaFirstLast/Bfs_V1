
import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { FlatpickrDirective, provideFlatpickrDefaults } from 'angularx-flatpickr';
import { IQueryResponse, ILookup } from '@bfs/_shared/interfaces';

import { IBrokerAgreementFilter } from './broker-agreement.shared';
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

@Component({
    selector: 'app-broker-agreement-filter',
    imports: [FormsModule, CommonModule, FlatpickrDirective],
    templateUrl: './broker-agreement.filter.component.html',
    providers: [provideFlatpickrDefaults()],
    //styles: ``
})
export class BrokerAgreementFilterComponent implements OnInit {

    public result = {} as IBrokerAgreementFilter;

    // Define look ups
    public InvestorOptions:  any[] = [];
public BrokerOptions:  any[] = [];

    // Define autocomplete fields

    // Define range filters
    public AgreementDateFrom: Date | null | undefined;
    public AgreementDateTo: Date | null | undefined;

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
        this.AgreementDateFrom = this.result.AgreementDate?.from;
        this.AgreementDateTo   = this.result.AgreementDate?.to;

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
        this.result.AgreementDate = { from: this.AgreementDateFrom, to: this.AgreementDateTo };

        this.parent.applyFilter(this.result);
    }
//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

}