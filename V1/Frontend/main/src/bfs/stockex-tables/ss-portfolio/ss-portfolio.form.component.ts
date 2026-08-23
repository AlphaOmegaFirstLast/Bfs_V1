import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
//Template_Start_Component_AutoComplete
import { debounceTime, distinctUntilChanged, filter, switchMap, finalize, mergeMap, startWith } from 'rxjs/operators';
//Template_End_Component_AutoComplete

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
import { type ISsPortfolio, type ISsPortfolioRequest, initSsPortfolio, ssPortfolioUntypedFormGroup } from './ss-portfolio.shared';
import { getSsPortfolioActions, initSsPortfolioRequest } from './ss-portfolio.shared';

import { SspTransactionListComponent } from "../ssp-transaction/ssp-transaction.list.component"
import { ISspTransactionFilter, ISspTransactionRequest, initSspTransactionRequest } from "../ssp-transaction/ssp-transaction.shared"
import { CashTransactionListComponent } from "../cash-transaction/cash-transaction.list.component"
import { ICashTransactionFilter, ICashTransactionRequest, initCashTransactionRequest } from "../cash-transaction/cash-transaction.shared"
import { SsPortfolioBalanceListComponent } from "../ss-portfolio-balance/ss-portfolio-balance.list.component"
import { ISsPortfolioBalanceFilter, ISsPortfolioBalanceRequest, initSsPortfolioBalanceRequest } from "../ss-portfolio-balance/ss-portfolio-balance.shared"
import { OverdraftPortfolioListComponent } from "../overdraft-portfolio/overdraft-portfolio.list.component"
import { IOverdraftPortfolioFilter, IOverdraftPortfolioRequest, initOverdraftPortfolioRequest } from "../overdraft-portfolio/overdraft-portfolio.shared"
import { SspStockListComponent } from "../ssp-stock/ssp-stock.list.component"
import { ISspStockFilter, ISspStockRequest, initSspStockRequest } from "../ssp-stock/ssp-stock.shared"
import { formatFileSize } from '@/app/utils/file-utils';

@Component({
    selector: 'ss-portfolio-form',
    imports: [
        SspTransactionListComponent,
        CashTransactionListComponent,
        SsPortfolioBalanceListComponent,
        OverdraftPortfolioListComponent,
        SspStockListComponent,

        CommonModule, NgIcon, NgbPopoverModule, NgbAlertModule, FormsModule, ReactiveFormsModule, NgbDropdownModule, NgbNavModule, RouterLink],
    standalone: true,
    templateUrl: './ss-portfolio.form.component.html',
})
export class SsPortfolioFormComponent extends BaseFormComponent<ISsPortfolio> implements OnInit {

    override apiUrl = '/SsPortfolio/';
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
    showBroker = false; // Toggle for the overlay
    brokerOptions: any[] = [];
    brokerControl: any;
    showInvestor = false; // Toggle for the overlay
    investorOptions: any[] = [];
    investorControl: any;

    //---------------------------------------------------------

    constructor(activatedRoute: ActivatedRoute) {

        super(activatedRoute);
        this.validationForm = this.formBuilder.group(ssPortfolioUntypedFormGroup(this.formBuilder)); // Use Angular Validation Controls
        this.brokerControl = this.validationForm.get('brokerName') as any;
        this.investorControl = this.validationForm.get('investorName') as any;

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
    override initEntity(): ISsPortfolio {
        return initSsPortfolio();
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
    override async setAutoComplete() {
        await this.brokerAutoComplete();
        await this.investorAutoComplete();

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

    }
    //---------------------------------------------------------
    async brokerAutoComplete() {

            this.brokerControl.valueChanges.pipe(
                startWith(this.brokerControl.value ?? ''),
                // 1. Only proceed if input length >= 2
                filter((val: unknown): val is string => typeof val === 'string' && val.trim().length >= 2),
                // 2. Wait 300ms after last keystroke to avoid API spam
                debounceTime(300),
                // 3. Only trigger if the value actually changed
                distinctUntilChanged((previous: unknown, current: unknown) => String(previous).trim() === String(current).trim()),
                // 4. Switch to API call
                switchMap(async (searchTerm: string): Promise<any[]> => {
                    searchTerm = searchTerm.trim();

                    var inputFilter = { name: searchTerm };
                    this.isLoading.autoComplete = true;
                    this.showBroker = this.entity.brokerName != searchTerm; // Show dropdown when searching starts
                    try {
                        const request = { pageSize: 20, filter: inputFilter };
                        const response: any = await this.apiService.postAutoComplete("/Broker/list", request);
                        return response?.items ?? []; // Return the data directly
                    } catch (err: any) {
                        this.messages.push({ text: err?.msg || err?.message || "Error fetching data", msgType: "danger" });
                        return []; // Return empty array on error to keep the stream alive
                    } finally {
                        this.isLoading.autoComplete = false;
                    }
                })
            ).subscribe((items: any[]) => {
                this.brokerOptions = items;
            });
     
    }
    //---------------------------------------------------------
    selectBroker(selectedOption: any) {
        // Set the input value to the selected name
        this.validationForm.get('brokerName')?.setValue(selectedOption.name, { emitEvent: false });
        this.validationForm.get('brokerId')?.setValue(selectedOption.id, { emitEvent: false });
        // Update your hidden form control or parent logic here
        this.brokerOptions = [];
        this.showBroker = false;
    }
    //---------------------------------------------------------
    // Close dropdown when input loses focus (with a slight delay to allow clicks)
    hideBrokerOverlay() {
        setTimeout(() => this.showBroker = false, 200);
    }
    //---------------------------------------------------------
    async investorAutoComplete() {
        this.investorControl.valueChanges.pipe(
            startWith(this.investorControl.value ?? ''),
            // 1. Only proceed if input length >= 2
            filter((val: unknown): val is string => typeof val === 'string' && val.trim().length >= 2),
            // 2. Wait 300ms after last keystroke to avoid API spam
            debounceTime(300),
            // 3. Only trigger if the value actually changed
            distinctUntilChanged((previous: unknown, current: unknown) => String(previous).trim() === String(current).trim()),
            // 4. Switch to API call
            switchMap(async (searchTerm: string): Promise<any[]> => {
                searchTerm = searchTerm.trim();
                this.isLoading.autoComplete = true;
                this.showInvestor = this.entity.investorName != searchTerm; // Show dropdown when searching starts
                try {
                    const request = { pageSize: 20, filter: { name: searchTerm } };
                    const response: any = await this.apiService.postAutoComplete("/Investor/list", request);
                    return response?.items ?? []; // Return the data directly
                } catch (err: any) {
                    this.messages.push({ text: err?.msg || err?.message || "Error fetching data", msgType: "danger" });
                    return []; // Return empty array on error to keep the stream alive
                } finally {
                    this.isLoading.autoComplete = false;
                }
            })
        ).subscribe((items: any[]) => {
            this.investorOptions = items;
        });
    }
    //---------------------------------------------------------
    selectInvestor(selectedOption: any) {
        // Set the input value to the selected name
        this.validationForm.get('investorName')?.setValue(selectedOption.name, { emitEvent: false });
        this.validationForm.get('investorId')?.setValue(selectedOption.id, { emitEvent: false });
        // Update your hidden form control or parent logic here
        this.investorOptions = [];
        this.showInvestor = false;
    }
    //---------------------------------------------------------
    // Close dropdown when input loses focus (with a slight delay to allow clicks)
    hideInvestorOverlay() {
        setTimeout(() => this.showInvestor = false, 200);
    }
    //---------------------------------------------------------

    getRecordLinks(record: IEntity): ViewLink[] {
        let actions = getSsPortfolioActions(this, record);
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
        let actions = getSsPortfolioActions(this, record);
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

