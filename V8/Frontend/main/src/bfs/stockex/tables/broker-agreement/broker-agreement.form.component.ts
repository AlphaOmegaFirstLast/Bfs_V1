import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { FormsModule, ReactiveFormsModule} from '@angular/forms';
import { NgbNavModule, NgbPopoverModule, NgbAlertModule, NgbDropdownModule} from '@ng-bootstrap/ng-bootstrap';
import { NgIcon } from '@ng-icons/core';
import { FlatpickrDirective, provideFlatpickrDefaults } from 'angularx-flatpickr';

import { BaseFormComponent } from '@bfs/_shared/components/base-form.component';
import { IEntity, IQueryResponse, IAction } from '@bfs/_shared/interfaces';

//----------------------- System Specific -------------------------- 
import { StockExService } from '@bfs/stockex/main/stockex.service';

//---------------------- Component Specific ------------------------
import {isEnabled, isVisible, type IBrokerAgreement, initBrokerAgreement, brokerAgreementUntypedFormGroup, getBrokerAgreementActions} from './broker-agreement.shared';
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

import {OverdraftPortfolioListComponent} from "../overdraft-portfolio/overdraft-portfolio.list.component"
import {IOverdraftPortfolioFilter, IOverdraftPortfolioRequest, initOverdraftPortfolioRequest} from "../overdraft-portfolio/overdraft-portfolio.shared"
import {CreditInterestListComponent} from "../credit-interest/credit-interest.list.component"
import {ICreditInterestFilter, ICreditInterestRequest, initCreditInterestRequest} from "../credit-interest/credit-interest.shared"
import {DebitInterestListComponent} from "../debit-interest/debit-interest.list.component"
import {IDebitInterestFilter, IDebitInterestRequest, initDebitInterestRequest} from "../debit-interest/debit-interest.shared"

@Component({
    selector: 'broker-agreement-form',
    imports: [
    OverdraftPortfolioListComponent,
CreditInterestListComponent,
DebitInterestListComponent,

    CommonModule, FormsModule, ReactiveFormsModule,RouterLink, 
    NgIcon, NgbNavModule, NgbPopoverModule, NgbAlertModule, NgbDropdownModule
, FlatpickrDirective

    ],
    standalone: true,
    templateUrl: './broker-agreement.form.component.html',
    providers: [
  provideFlatpickrDefaults()

    ],
})
export class BrokerAgreementFormComponent extends BaseFormComponent<IBrokerAgreement > implements OnInit {

    override apiUrl =  '/BrokerAgreement/';
    override apiService: StockExService = inject(StockExService);
    override componentName: string = 'BrokerAgreement'.toLowerCase();  // used to grab its related custom field definitions

    // Children filters
    presetOverdraftPortfolioFilter: IOverdraftPortfolioFilter | undefined;
presetCreditInterestFilter: ICreditInterestFilter | undefined;
presetDebitInterestFilter: IDebitInterestFilter | undefined;

    // Define look ups
    public InvestorOptions: any[] = [];
public BrokerOptions: any[] = [];

    // Define autocomplete

    //---------------------------------------------------------

    constructor(activatedRoute: ActivatedRoute) {

       super(activatedRoute);
       this.validationForm = this.formBuilder.group(brokerAgreementUntypedFormGroup(this.formBuilder)); // Use Angular Validation Controls

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

        this.linkList = this.getRecordLinks(this);
        this.actionList = this.getRecordActions(this);    
        this.buttonList = this.getRecordButtons(this);
    }
    //---------------------------------------------------------
    override initEntity(): IBrokerAgreement  {
        return initBrokerAgreement ();
    }
    //---------------------------------------------------------
    override setChildrenRequests() {
        let presetOverdraftPortfolioRequest: IOverdraftPortfolioRequest = initOverdraftPortfolioRequest();
        this.presetOverdraftPortfolioFilter = presetOverdraftPortfolioRequest.filter;
        if (this.presetOverdraftPortfolioFilter) {
            this.presetOverdraftPortfolioFilter.BrokerAgreementId = this.entity.id;
        }
let presetCreditInterestRequest: ICreditInterestRequest = initCreditInterestRequest();
        this.presetCreditInterestFilter = presetCreditInterestRequest.filter;
        if (this.presetCreditInterestFilter) {
            this.presetCreditInterestFilter.BrokerAgreementId = this.entity.id;
        }
let presetDebitInterestRequest: IDebitInterestRequest = initDebitInterestRequest();
        this.presetDebitInterestFilter = presetDebitInterestRequest.filter;
        if (this.presetDebitInterestFilter) {
            this.presetDebitInterestFilter.BrokerAgreementId = this.entity.id;
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
              investorResponse,
brokerResponse,

         ] = await Promise.all
         ([
        this.apiService.getItems<IQueryResponse>("/Investor/list", { pageSize: 30 }),
this.apiService.getItems<IQueryResponse>("/Broker/list", { pageSize: 30 }),

         ]);
        this.InvestorOptions = investorResponse.items;
this.BrokerOptions = brokerResponse.items;

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
        return getBrokerAgreementActions(this, record);
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
