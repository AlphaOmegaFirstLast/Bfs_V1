import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbNavModule, NgbPopoverModule, NgbAlertModule, NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { NgIcon } from '@ng-icons/core';

import { BaseFormComponent } from '@bfs/_shared/components/base-form.component';
import { IEntity, IQueryResponse, IAction } from '@bfs/_shared/interfaces';

//----------------------- System Specific -------------------------- 
import { StockExService } from '@bfs/stockex/main/stockex.service';
import { IAutoComplete, AutoCompleteHelper } from '@bfs/_shared/helpers/auto-complete.class';

//---------------------- Component Specific ------------------------
import { isEnabled, isVisible, type IOverdraftPortfolio, initOverdraftPortfolio, overdraftPortfolioUntypedFormGroup, getOverdraftPortfolioActions } from './overdraft-portfolio.shared';
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

@Component({
    selector: 'overdraft-portfolio-form',
    imports: [

        CommonModule, FormsModule, ReactiveFormsModule, RouterLink,
        NgIcon, NgbNavModule, NgbPopoverModule, NgbAlertModule, NgbDropdownModule

    ],
    standalone: true,
    templateUrl: './overdraft-portfolio.form.component.html',
    providers: [

    ],
})
export class OverdraftPortfolioFormComponent extends BaseFormComponent<IOverdraftPortfolio> implements OnInit {

    override apiUrl = '/OverdraftPortfolio/';
    override apiService: StockExService = inject(StockExService);
    override componentName: string = 'OverdraftPortfolio'.toLowerCase();  // used to grab its related custom field definitions

    // Children filters

    // Define look ups
    public SsPortfolioOptions: any[] = [];

    // Define autocomplete
    brokerAgreementAuto: AutoCompleteHelper = new AutoCompleteHelper({ queryUrl: "/BrokerAgreement/list", fieldName: 'brokerAgreement', control: null, id: '', name: '', showDropDown: false, options: [], isLoading: false, isInitial: true } as IAutoComplete);

    //---------------------------------------------------------

    constructor(activatedRoute: ActivatedRoute) {

        super(activatedRoute);
        this.validationForm = this.formBuilder.group(overdraftPortfolioUntypedFormGroup(this.formBuilder)); // Use Angular Validation Controls
        this.brokerAgreementAuto.control = this.validationForm.get('brokerAgreementName');

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
    override initEntity(): IOverdraftPortfolio {
        return initOverdraftPortfolio();
    }
    //---------------------------------------------------------
    override setChildrenRequests() {

    }
    //---------------------------------------------------------
    /*
    override async getLookups(): Promise<void> {
        this.messages = [];
        let target = '';
        this.isLoading.lookups = true;
        // Promise.all to improve performance. apply later
        try {
            const [
                ssPortfolioResponse,

            ] = await Promise.all
                ([
                    this.apiService.getItems<IQueryResponse>("/SsPortfolio/list", { pageSize: 30 }),

                ]);
            this.SsPortfolioOptions = ssPortfolioResponse.items;

        } catch (err: any) {
            const msg = err?.message || "An error occurred while loading data.";
            this.messages.push({ text: msg, msgType: "danger" });
        } finally {
            this.isLoading.lookups = false;
        }

    }
    */
    //---------------------------------------------------------
    override async setAutoComplete() {
        await this.brokerAgreementAuto.setUpForm(this.apiService, this);

    }
    //---------------------------------------------------------
    // required to set the control with the entity initial values
    override async setDataAutoComplete() {
        this.brokerAgreementAuto.setData(this.entity.brokerAgreementId, this.entity.brokerAgreementName);

    }
    //---------------------------------------------------------

    override getActions(record: IEntity): IAction[] {
        return getOverdraftPortfolioActions(this, record);
    }

    //---------------------------------------------------------
    isVisible(fieldName: string): boolean {

        return isVisible(this.entity, this.validationForm, fieldName);
    }
    //--------------------------------------------------------------
    isEnabled(fieldName: string): boolean {

        return isEnabled(this.entity, this.validationForm, fieldName);
    }
    //--------------------------------------------------------------

    //Template_Start_Code_DontOverwrite_2
    async BrokerAgreementChange() {
        const brokerAgreementId = this.validationForm.get('brokerAgreementId')?.value;
        var brokerAgreementRecord = this.brokerAgreementAuto.list.find(X => X.id == brokerAgreementId);
        var brokerId = brokerAgreementRecord?.brokerId;
        var investorId = brokerAgreementRecord?.investorId;

        this.messages = [];
        let target = '';
        this.isLoading.lookups = true;

        // Promise.all to improve performance. apply later
        try {
            const [
                ssPortfolioResponse,
            ] = await Promise.all
                ([
                    this.apiService.getItems<IQueryResponse>("/SsPortfolio/list", { filter: { BrokerId: brokerId, InvestorId: investorId }, pageSize: 30 }),
                ]);
            this.SsPortfolioOptions = ssPortfolioResponse.items;

        } catch (err: any) {
            const msg = err?.message || "An error occurred while loading data.";
            this.messages.push({ text: msg, msgType: "danger" });
        } finally {
            this.isLoading.lookups = false;
        }
    }
    //Template_End_Code_DontOverwrite_2
}
