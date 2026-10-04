
import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { FlatpickrDirective, provideFlatpickrDefaults } from 'angularx-flatpickr';
import { IQueryResponse, ILookup } from '@bfs/_shared/interfaces';

import { ICreditInterestFilter } from './credit-interest.shared';
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

@Component({
    selector: 'app-credit-interest-filter',
    imports: [FormsModule, CommonModule, FlatpickrDirective],
    templateUrl: './credit-interest.filter.component.html',
    providers: [provideFlatpickrDefaults()],
    //styles: ``
})
export class CreditInterestFilterComponent implements OnInit {

    public result = {} as ICreditInterestFilter;

    // Define look ups
    public PaymentModeOptions:  any[] = [];
public BrokerAgreementOptions:  any[] = [];

    // Define autocomplete fields

    // Define range filters
    public InterestPercentFrom: number | undefined;
    public InterestPercentTo: number | undefined;

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
        this.InterestPercentFrom = this.result.InterestPercent?.from;
        this.InterestPercentTo   = this.result.InterestPercent?.to;

    }
    //---------------------------------------------------------
    async getLookups(): Promise<void> {
        let target = '';
        target = "/PaymentMode/list";
        (await this.parent.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.PaymentModeOptions = response.items;
                this.isLoading.list = false;
            },
                error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching Payment Mode data.';
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
        this.result.InterestPercent = { from: this.InterestPercentFrom, to: this.InterestPercentTo };

        this.parent.applyFilter(this.result);
    }
//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

}