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
import { IQueryResponse, ILookup, IUIMessage, IQueryColumn, ActionLink, ViewLink, IEntity } from '@bfs/_shared/interfaces';

//----------------------- System Specific -------------------------- 
import { StockExService } from '@bfs/stockex-main/stockex.service';

//---------------------- Component Specific ------------------------
import { type ITransactionType, type ITransactionTypeRequest, initTransactionType, transactionTypeUntypedFormGroup } from './transaction-type.shared';
import { getTransactionTypeActions,  initTransactionTypeRequest } from './transaction-type.shared';

@Component({
    selector: 'transaction-type-form',
    imports: [

    CommonModule, NgIcon, NgbPopoverModule, NgbAlertModule, FormsModule, ReactiveFormsModule, NgbDropdownModule, NgbNavModule,RouterLink],
    standalone: true,
    templateUrl: './transaction-type.form.component.html',
})
export class TransactionTypeFormComponent extends BaseFormComponent<ITransactionType > implements OnInit {

    override apiUrl =  '/TransactionType/';
    override apiService: StockExService = inject(StockExService);
    override componentName: string = 'TransactionType'.toLowerCase();  // used to grab its related custom field definitions

    // Children filters

    // Define look ups
    public EffectTypeOptions: any[] = [];
public StockEntityTypeOptions: any[] = [];
public CalculationMethodOptions: any[] = [];
public SourceTypeOptions: any[] = [];
public StockFieldTypeOptions: any[] = [];
public NextTransactionTypeOptions: any[] = [];

    // Define autocomplete

    //---------------------------------------------------------

    constructor(activatedRoute: ActivatedRoute) {

       super(activatedRoute);
       this.validationForm = this.formBuilder.group(transactionTypeUntypedFormGroup(this.formBuilder)); // Use Angular Validation Controls

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
    override initEntity(): ITransactionType  {
        return initTransactionType ();
    }
    //---------------------------------------------------------
    override setChildrenRequests() {

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
        target = "/EffectType/list";
        (await this.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.EffectTypeOptions = response.items;
                this.isLoading.lookups = false;
            },
                error: (err: any) => {
                this.isLoading.lookups = false;
                var msg = err.message || 'An error occurred while fetching Effect Type data.';
                this.messages.push({ text: msg, msgType: "danger" });
            }
        });
this.isLoading.lookups = true;
        target = "/StockEntityType/list";
        (await this.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.StockEntityTypeOptions = response.items;
                this.isLoading.lookups = false;
            },
                error: (err: any) => {
                this.isLoading.lookups = false;
                var msg = err.message || 'An error occurred while fetching Applicable To Entity data.';
                this.messages.push({ text: msg, msgType: "danger" });
            }
        });
this.isLoading.lookups = true;
        target = "/CalculationMethod/list";
        (await this.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.CalculationMethodOptions = response.items;
                this.isLoading.lookups = false;
            },
                error: (err: any) => {
                this.isLoading.lookups = false;
                var msg = err.message || 'An error occurred while fetching Calculation Method data.';
                this.messages.push({ text: msg, msgType: "danger" });
            }
        });
this.isLoading.lookups = true;
        target = "/SourceType/list";
        (await this.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.SourceTypeOptions = response.items;
                this.isLoading.lookups = false;
            },
                error: (err: any) => {
                this.isLoading.lookups = false;
                var msg = err.message || 'An error occurred while fetching Source Type data.';
                this.messages.push({ text: msg, msgType: "danger" });
            }
        });
this.isLoading.lookups = true;
        target = "/StockFieldType/list";
        (await this.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.StockFieldTypeOptions = response.items;
                this.isLoading.lookups = false;
            },
                error: (err: any) => {
                this.isLoading.lookups = false;
                var msg = err.message || 'An error occurred while fetching Applicable To Field data.';
                this.messages.push({ text: msg, msgType: "danger" });
            }
        });
this.isLoading.lookups = true;
        target = "/TransactionType/list";
        (await this.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.NextTransactionTypeOptions = response.items;
                this.isLoading.lookups = false;
            },
                error: (err: any) => {
                this.isLoading.lookups = false;
                var msg = err.message || 'An error occurred while fetching Next Transaction Type data.';
                this.messages.push({ text: msg, msgType: "danger" });
            }
        });

    }
    //---------------------------------------------------------

    getRecordLinks(record: IEntity): ViewLink[] {
        let actions = getTransactionTypeActions(this,record);
        let links: ViewLink[] = actions.filter(action => 
               action.actionType == 'FrontendLink'
            && action.actionLocation == 'FormHeader'
            ).map(action => {
            return { recordId: action.recordId, route: action.route?? '', displayText: action.displayText}
        });

        return links;
    }
    //---------------------------------------------------------
    getRecordActions(record: IEntity): ActionLink[] {
        let actions = getTransactionTypeActions(this,record);
        let links: ActionLink[] = actions.filter(action => 
               action.actionType == 'FrontendFunction'
            && action.actionLocation == 'FormHeader'
            ).map(action => {
            return { recordId: action.recordId, action: action.action?? null, displayText: action.displayText, data: action.data}
        });

        return links;
    }
   //--------------------------------------------------------------

}

