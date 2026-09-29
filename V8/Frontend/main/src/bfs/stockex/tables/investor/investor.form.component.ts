import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { FormsModule, ReactiveFormsModule} from '@angular/forms';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { NgbAlertModule } from '@ng-bootstrap/ng-bootstrap';
import { NgbNavModule } from '@ng-bootstrap/ng-bootstrap';
import {NgbPopoverModule} from '@ng-bootstrap/ng-bootstrap';
import { NgIcon } from '@ng-icons/core';
import { BaseFormComponent } from '@bfs/_shared/components/base-form.component';
import { IEntity, IQueryResponse, IAction } from '@bfs/_shared/interfaces';

//----------------------- System Specific -------------------------- 
import { StockExService } from '@bfs/stockex/main/stockex.service';

//---------------------- Component Specific ------------------------
import {isEnabled, isVisible, type IInvestor, initInvestor, investorUntypedFormGroup, getInvestorActions} from './investor.shared';
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

import {SsPortfolioListComponent} from "../ss-portfolio/ss-portfolio.list.component"
import {ISsPortfolioFilter, ISsPortfolioRequest, initSsPortfolioRequest} from "../ss-portfolio/ss-portfolio.shared"
import {InvestorBrokerFundListComponent} from "../investor-broker-fund/investor-broker-fund.list.component"
import {IInvestorBrokerFundFilter, IInvestorBrokerFundRequest, initInvestorBrokerFundRequest} from "../investor-broker-fund/investor-broker-fund.shared"
import {BrokerAgreementListComponent} from "../broker-agreement/broker-agreement.list.component"
import {IBrokerAgreementFilter, IBrokerAgreementRequest, initBrokerAgreementRequest} from "../broker-agreement/broker-agreement.shared"

@Component({
    selector: 'investor-form',
    imports: [
    SsPortfolioListComponent,
InvestorBrokerFundListComponent,
BrokerAgreementListComponent,

    CommonModule, NgIcon, NgbPopoverModule, NgbAlertModule, FormsModule, ReactiveFormsModule, NgbDropdownModule, NgbNavModule,RouterLink],
    standalone: true,
    templateUrl: './investor.form.component.html',
})
export class InvestorFormComponent extends BaseFormComponent<IInvestor > implements OnInit {

    override apiUrl =  '/Investor/';
    override apiService: StockExService = inject(StockExService);
    override componentName: string = 'Investor'.toLowerCase();  // used to grab its related custom field definitions

    // Children filters
    presetSsPortfolioFilter: ISsPortfolioFilter | undefined;
presetInvestorBrokerFundFilter: IInvestorBrokerFundFilter | undefined;
presetBrokerAgreementFilter: IBrokerAgreementFilter | undefined;

    // Define look ups

    // Define autocomplete

    //---------------------------------------------------------

    constructor(activatedRoute: ActivatedRoute) {

       super(activatedRoute);
       this.validationForm = this.formBuilder.group(investorUntypedFormGroup(this.formBuilder)); // Use Angular Validation Controls

    }
    //---------------------------------------------------------
    override async ngOnInit(): Promise<void> {
        this.setChildrenRequests();
        await this.getCustomFieldDefinitions();
        await this.setAutoComplete();
        await this.getLookups();
        await this.getObjectFieldLookups();

        if (this.entity.id != '0') {
            this.view();
        }
    }
    //---------------------------------------------------------
    override initEntity(): IInvestor  {
        return initInvestor ();
    }
    //---------------------------------------------------------
    override setChildrenRequests() {
        let presetSsPortfolioRequest: ISsPortfolioRequest = initSsPortfolioRequest();
        this.presetSsPortfolioFilter = presetSsPortfolioRequest.filter;
        if (this.presetSsPortfolioFilter) {
            this.presetSsPortfolioFilter.InvestorId = this.entity.id;
        }
let presetInvestorBrokerFundRequest: IInvestorBrokerFundRequest = initInvestorBrokerFundRequest();
        this.presetInvestorBrokerFundFilter = presetInvestorBrokerFundRequest.filter;
        if (this.presetInvestorBrokerFundFilter) {
            this.presetInvestorBrokerFundFilter.InvestorId = this.entity.id;
        }
let presetBrokerAgreementRequest: IBrokerAgreementRequest = initBrokerAgreementRequest();
        this.presetBrokerAgreementFilter = presetBrokerAgreementRequest.filter;
        if (this.presetBrokerAgreementFilter) {
            this.presetBrokerAgreementFilter.InvestorId = this.entity.id;
        }

    }
    //---------------------------------------------------------
    override async getLookups(): Promise<void> {
        this.messages = [];
        let target = '';
        this.isLoading.lookups = true;
// Promise.all to improve performance. apply later
         try{
         const [

         ] = await Promise.all
         ([

         ]);

 } catch (err: any) {
   const msg = err?.message || "An error occurred while loading data.";
   this.messages.push({ text: msg, msgType: "danger" });
 } finally {
   this.isLoading.lookups = false;
 }
 /*
        this.isLoading.lookups = true;
        target = "/[LookupNameCapital]/list";
        (await this.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.[LookupNameCapital]Options = response.items;
                this.isLoading.lookups = false;
            },
                error: (err: any) => {
                this.isLoading.lookups = false;
                var msg = err.message || 'An error occurred while fetching [DisplayName] data.';
                this.messages.push({ text: msg, msgType: "danger" });
            }
        });
        */
    }
    //---------------------------------------------------------
    override async setAutoComplete() {   

    }
    //---------------------------------------------------------
    // required to set the control with the entity initial values
    override async setDataAutoComplete() {

    }
    //---------------------------------------------------------

    override getActions(record: IEntity): IAction[] {
        return getInvestorActions(this, record);
    }

   //---------------------------------------------------------
    isVisible(fieldName: string): boolean {

        return isVisible(this.entity,this.validationForm, fieldName);
    }
    //--------------------------------------------------------------
    isEnabled(fieldName: string): boolean {

        return isEnabled(this.entity,this.validationForm, fieldName);
    }
    //--------------------------------------------------------------

//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

}
