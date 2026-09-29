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
import { IAutoComplete, AutoCompleteHelper } from '@bfs/_shared/helpers/auto-complete.class';

//---------------------- Component Specific ------------------------
import {isEnabled, isVisible, type ISsPortfolio, initSsPortfolio, ssPortfolioUntypedFormGroup, getSsPortfolioActions} from './ss-portfolio.shared';
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

import {SspTransactionListComponent} from "../ssp-transaction/ssp-transaction.list.component"
import {ISspTransactionFilter, ISspTransactionRequest, initSspTransactionRequest} from "../ssp-transaction/ssp-transaction.shared"
import {CashTransactionListComponent} from "../cash-transaction/cash-transaction.list.component"
import {ICashTransactionFilter, ICashTransactionRequest, initCashTransactionRequest} from "../cash-transaction/cash-transaction.shared"
import {SsPortfolioBalanceListComponent} from "../ss-portfolio-balance/ss-portfolio-balance.list.component"
import {ISsPortfolioBalanceFilter, ISsPortfolioBalanceRequest, initSsPortfolioBalanceRequest} from "../ss-portfolio-balance/ss-portfolio-balance.shared"
import {OverdraftPortfolioListComponent} from "../overdraft-portfolio/overdraft-portfolio.list.component"
import {IOverdraftPortfolioFilter, IOverdraftPortfolioRequest, initOverdraftPortfolioRequest} from "../overdraft-portfolio/overdraft-portfolio.shared"
import {SspStockListComponent} from "../ssp-stock/ssp-stock.list.component"
import {ISspStockFilter, ISspStockRequest, initSspStockRequest} from "../ssp-stock/ssp-stock.shared"

@Component({
    selector: 'ss-portfolio-form',
    imports: [
    SspTransactionListComponent,
CashTransactionListComponent,
SsPortfolioBalanceListComponent,
OverdraftPortfolioListComponent,
SspStockListComponent,

    CommonModule, NgIcon, NgbPopoverModule, NgbAlertModule, FormsModule, ReactiveFormsModule, NgbDropdownModule, NgbNavModule,RouterLink],
    standalone: true,
    templateUrl: './ss-portfolio.form.component.html',
})
export class SsPortfolioFormComponent extends BaseFormComponent<ISsPortfolio > implements OnInit {

    override apiUrl =  '/SsPortfolio/';
    override apiService: StockExService = inject(StockExService);
    override componentName: string = 'SsPortfolio'.toLowerCase();  // used to grab its related custom field definitions

    // Children filters
    presetSspTransactionFilter: ISspTransactionFilter | undefined;
presetCashTransactionFilter: ICashTransactionFilter | undefined;
presetSsPortfolioBalanceFilter: ISsPortfolioBalanceFilter | undefined;
presetOverdraftPortfolioFilter: IOverdraftPortfolioFilter | undefined;
presetSspStockFilter: ISspStockFilter | undefined;

    // Define look ups

    // Define autocomplete
    brokerAuto: AutoCompleteHelper = new AutoCompleteHelper({ queryUrl: "/Broker/list", fieldName: 'broker', control:null, id: '', name: '', showDropDown: false, options: [], isLoading: false, isInitial: true } as IAutoComplete);
investorAuto: AutoCompleteHelper = new AutoCompleteHelper({ queryUrl: "/Investor/list", fieldName: 'investor', control:null, id: '', name: '', showDropDown: false, options: [], isLoading: false, isInitial: true } as IAutoComplete);

    //---------------------------------------------------------

    constructor(activatedRoute: ActivatedRoute) {

       super(activatedRoute);
       this.validationForm = this.formBuilder.group(ssPortfolioUntypedFormGroup(this.formBuilder)); // Use Angular Validation Controls
      this.brokerAuto.control = this.validationForm.get('brokerName');
this.investorAuto.control = this.validationForm.get('investorName');

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
    override initEntity(): ISsPortfolio  {
        return initSsPortfolio ();
    }
    //---------------------------------------------------------
    override setChildrenRequests() {
        let presetSspTransactionRequest: ISspTransactionRequest = initSspTransactionRequest();
        this.presetSspTransactionFilter = presetSspTransactionRequest.filter;
        if (this.presetSspTransactionFilter) {
            this.presetSspTransactionFilter.SsPortfolioId = this.entity.id;
        }
let presetCashTransactionRequest: ICashTransactionRequest = initCashTransactionRequest();
        this.presetCashTransactionFilter = presetCashTransactionRequest.filter;
        if (this.presetCashTransactionFilter) {
            this.presetCashTransactionFilter.SsPortfolioId = this.entity.id;
        }
let presetSsPortfolioBalanceRequest: ISsPortfolioBalanceRequest = initSsPortfolioBalanceRequest();
        this.presetSsPortfolioBalanceFilter = presetSsPortfolioBalanceRequest.filter;
        if (this.presetSsPortfolioBalanceFilter) {
            this.presetSsPortfolioBalanceFilter.SsPortfolioId = this.entity.id;
        }
let presetOverdraftPortfolioRequest: IOverdraftPortfolioRequest = initOverdraftPortfolioRequest();
        this.presetOverdraftPortfolioFilter = presetOverdraftPortfolioRequest.filter;
        if (this.presetOverdraftPortfolioFilter) {
            this.presetOverdraftPortfolioFilter.SsPortfolioId = this.entity.id;
        }
let presetSspStockRequest: ISspStockRequest = initSspStockRequest();
        this.presetSspStockFilter = presetSspStockRequest.filter;
        if (this.presetSspStockFilter) {
            this.presetSspStockFilter.SsPortfolioId = this.entity.id;
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
    await this.brokerAuto.setUpForm(this.apiService, this);
await this.investorAuto.setUpForm(this.apiService, this);

    }
    //---------------------------------------------------------
    // required to set the control with the entity initial values
    override async setDataAutoComplete() {
    this.brokerAuto.setData(this.entity.brokerId,this.entity.brokerName);
this.investorAuto.setData(this.entity.investorId,this.entity.investorName);

    }
    //---------------------------------------------------------

    override getActions(record: IEntity): IAction[] {
        return getSsPortfolioActions(this, record);
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
