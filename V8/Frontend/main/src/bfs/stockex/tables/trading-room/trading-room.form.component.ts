import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { FormsModule, ReactiveFormsModule} from '@angular/forms';
import { NgbNavModule, NgbPopoverModule, NgbAlertModule, NgbDropdownModule} from '@ng-bootstrap/ng-bootstrap';
import { NgIcon } from '@ng-icons/core';

import { BaseFormComponent } from '@bfs/_shared/components/base-form.component';
import { IEntity, IQueryResponse, IAction } from '@bfs/_shared/interfaces';

//----------------------- System Specific -------------------------- 
import { StockExService } from '@bfs/stockex/main/stockex.service';

//---------------------- Component Specific ------------------------
import {isEnabled, isVisible, type ITradingRoom, initTradingRoom, tradingRoomUntypedFormGroup, getTradingRoomActions} from './trading-room.shared';
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

import {StockShareListComponent} from "../stock-share/stock-share.list.component"
import {IStockShareFilter, IStockShareRequest, initStockShareRequest} from "../stock-share/stock-share.shared"
import {BrokerListComponent} from "../broker/broker.list.component"
import {IBrokerFilter, IBrokerRequest, initBrokerRequest} from "../broker/broker.shared"

@Component({
    selector: 'trading-room-form',
    imports: [
    StockShareListComponent,
BrokerListComponent,

    CommonModule, FormsModule, ReactiveFormsModule,RouterLink, 
    NgIcon, NgbNavModule, NgbPopoverModule, NgbAlertModule, NgbDropdownModule

    ],
    standalone: true,
    templateUrl: './trading-room.form.component.html',
    providers: [

    ],
})
export class TradingRoomFormComponent extends BaseFormComponent<ITradingRoom > implements OnInit {

    override apiUrl =  '/TradingRoom/';
    override apiService: StockExService = inject(StockExService);
    override componentName: string = 'TradingRoom'.toLowerCase();  // used to grab its related custom field definitions

    // Children filters
    presetStockShareFilter: IStockShareFilter | undefined;
presetBrokerFilter: IBrokerFilter | undefined;

    // Define look ups
    public CurrencyOptions: any[] = [];

    // Define autocomplete

    //---------------------------------------------------------

    constructor(activatedRoute: ActivatedRoute) {

       super(activatedRoute);
       this.validationForm = this.formBuilder.group(tradingRoomUntypedFormGroup(this.formBuilder)); // Use Angular Validation Controls

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
    override initEntity(): ITradingRoom  {
        return initTradingRoom ();
    }
    //---------------------------------------------------------
    override setChildrenRequests() {
        let presetStockShareRequest: IStockShareRequest = initStockShareRequest();
        this.presetStockShareFilter = presetStockShareRequest.filter;
        if (this.presetStockShareFilter) {
            this.presetStockShareFilter.TradingRoomId = this.entity.id;
        }
let presetBrokerRequest: IBrokerRequest = initBrokerRequest();
        this.presetBrokerFilter = presetBrokerRequest.filter;
        if (this.presetBrokerFilter) {
            this.presetBrokerFilter.TradingRoomId = this.entity.id;
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
              currencyResponse,

         ] = await Promise.all
         ([
        this.apiService.getItems<IQueryResponse>("/Currency/list", { pageSize: 30 }),

         ]);
        this.CurrencyOptions = currencyResponse.items;

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
        return getTradingRoomActions(this, record);
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
