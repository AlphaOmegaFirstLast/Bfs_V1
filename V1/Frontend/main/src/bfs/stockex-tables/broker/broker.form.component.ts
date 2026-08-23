import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { NgbAlertModule } from '@ng-bootstrap/ng-bootstrap';
import { NgbNavModule } from '@ng-bootstrap/ng-bootstrap';
import { NgbPopoverModule } from '@ng-bootstrap/ng-bootstrap';
import { NgIcon } from '@ng-icons/core';
import { BaseFormComponent } from '@bfs/_shared/components/base-form.component';
import { IQueryResponse, ILookup, IUIMessage, IQueryColumn, ActionLink, ViewLink, IEntity } from '@bfs/_shared/interfaces';

//----------------------- System Specific -------------------------- 
import { StockExService } from '@bfs/stockex-main/stockex.service';

//---------------------- Component Specific ------------------------
import { type IBroker, type IBrokerRequest, initBroker, brokerUntypedFormGroup } from './broker.shared';
import { getBrokerActions, initBrokerRequest } from './broker.shared';

import { SsPortfolioListComponent } from "../ss-portfolio/ss-portfolio.list.component"
import { ISsPortfolioFilter, ISsPortfolioRequest, initSsPortfolioRequest } from "../ss-portfolio/ss-portfolio.shared"
import { InvestorBrokerFundListComponent } from "../investor-broker-fund/investor-broker-fund.list.component"
import { IInvestorBrokerFundFilter, IInvestorBrokerFundRequest, initInvestorBrokerFundRequest } from "../investor-broker-fund/investor-broker-fund.shared"
import { BrokerAgreementListComponent } from "../broker-agreement/broker-agreement.list.component"
import { IBrokerAgreementFilter, IBrokerAgreementRequest, initBrokerAgreementRequest } from "../broker-agreement/broker-agreement.shared"

@Component({
    selector: 'broker-form',
    imports: [
        SsPortfolioListComponent,
        InvestorBrokerFundListComponent,
        BrokerAgreementListComponent,

        CommonModule, NgIcon, NgbPopoverModule, NgbAlertModule, FormsModule, ReactiveFormsModule, NgbDropdownModule, NgbNavModule, RouterLink],
    standalone: true,
    templateUrl: './broker.form.component.html',
})
export class BrokerFormComponent extends BaseFormComponent<IBroker> implements OnInit {

    override apiUrl = '/Broker/';
    override apiService: StockExService = inject(StockExService);
    override componentName: string = 'Broker'.toLowerCase();  // used to grab its related custom field definitions

    // Children filters
    presetSsPortfolioFilter: ISsPortfolioFilter | undefined;
    presetInvestorBrokerFundFilter: IInvestorBrokerFundFilter | undefined;
    presetBrokerAgreementFilter: IBrokerAgreementFilter | undefined;

    // Define look ups
    public TradingRoomOptions: any[] = [];

    // Define autocomplete

    //---------------------------------------------------------

    constructor(activatedRoute: ActivatedRoute) {

        super(activatedRoute);
        this.validationForm = this.formBuilder.group(brokerUntypedFormGroup(this.formBuilder)); // Use Angular Validation Controls

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
    override initEntity(): IBroker {
        return initBroker();
    }
    //---------------------------------------------------------
    override setChildrenRequests() {
        let presetSsPortfolioRequest: ISsPortfolioRequest = initSsPortfolioRequest();
        this.presetSsPortfolioFilter = presetSsPortfolioRequest.filter;
        if (this.presetSsPortfolioFilter) {
            this.presetSsPortfolioFilter.BrokerId = this.entity.id;
        }
        let presetInvestorBrokerFundRequest: IInvestorBrokerFundRequest = initInvestorBrokerFundRequest();
        this.presetInvestorBrokerFundFilter = presetInvestorBrokerFundRequest.filter;
        if (this.presetInvestorBrokerFundFilter) {
            this.presetInvestorBrokerFundFilter.BrokerId = this.entity.id;
        }
        let presetBrokerAgreementRequest: IBrokerAgreementRequest = initBrokerAgreementRequest();
        this.presetBrokerAgreementFilter = presetBrokerAgreementRequest.filter;
        if (this.presetBrokerAgreementFilter) {
            this.presetBrokerAgreementFilter.BrokerId = this.entity.id;
        }

    }
    //---------------------------------------------------------
    override async setAutoComplete() {

    }
    //---------------------------------------------------------
    override async getLookups(): Promise<void> {
        this.messages = [];
        let target = '';
        this.isLoading.lookups = true;
        // Promise.all to improve performance. apply later
        //         try{
        //         const [
        //             BfsSystemList, 
        //             DataTypeList,
        //         ] = await Promise.all
        //         ([
        //             this.apiService.getItems<IQueryResponse>("/BfsSystem/list", { pageSize: 300 }),
        //             this.apiService.getItems<IQueryResponse>("/DataType/list", { pageSize: 300 }),
        //         ]);
        //         this.BfsSystemOptions = BfsSystemList.items;
        //         this.DataTypeOptions = DataTypeList.items;
        // } catch (err: any) {
        //   const msg = err?.message || "An error occurred while loading data.";
        //   this.messages.push({ text: msg, msgType: "danger" });
        // } finally {
        //   this.isLoading.lookups = false;
        // }
        this.isLoading.lookups = true;
        target = "/TradingRoom/list";
        (await this.apiService.post(target, { pageSize: 1 })).subscribe({
            next: (response: IQueryResponse) => {
                this.TradingRoomOptions = response.items;
                this.isLoading.lookups = false;
            },
            error: (err: any) => {
                this.isLoading.lookups = false;
                var msg = err.message || 'An error occurred while fetching Trading Room data.';
                this.messages.push({ text: msg, msgType: "danger" });
            }
        });

    }
    //---------------------------------------------------------

    getRecordLinks(record: IEntity): ViewLink[] {
        let actions = getBrokerActions(this, record);
        let links: ViewLink[] = actions.filter(action =>
            action.actionType == 'FrontendLink'
            && action.actionLocation == 'FormHeader'
        ).map(action => {
            return { recordId: action.recordId, route: action.route ?? '', displayText: action.displayText }
        });

        return links;
    }
    //---------------------------------------------------------
    getRecordActions(record: IEntity): ActionLink[] {
        let actions = getBrokerActions(this, record);
        let links: ActionLink[] = actions.filter(action =>
            action.actionType == 'FrontendFunction'
            && action.actionLocation == 'FormHeader'
        ).map(action => {
            return { recordId: action.recordId, action: action.action ?? null, displayText: action.displayText, data: action.data }
        });

        return links;
    }
    //--------------------------------------------------------------

}

