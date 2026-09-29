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
import {isEnabled, isVisible, type IStockShare, initStockShare, stockShareUntypedFormGroup, getStockShareActions} from './stock-share.shared';
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

import {CurrentPriceListComponent} from "../current-price/current-price.list.component"
import {ICurrentPriceFilter, ICurrentPriceRequest, initCurrentPriceRequest} from "../current-price/current-price.shared"

@Component({
    selector: 'stock-share-form',
    imports: [
    CurrentPriceListComponent,

    CommonModule, NgIcon, NgbPopoverModule, NgbAlertModule, FormsModule, ReactiveFormsModule, NgbDropdownModule, NgbNavModule,RouterLink],
    standalone: true,
    templateUrl: './stock-share.form.component.html',
})
export class StockShareFormComponent extends BaseFormComponent<IStockShare > implements OnInit {

    override apiUrl =  '/StockShare/';
    override apiService: StockExService = inject(StockExService);
    override componentName: string = 'StockShare'.toLowerCase();  // used to grab its related custom field definitions

    // Children filters
    presetCurrentPriceFilter: ICurrentPriceFilter | undefined;

    // Define look ups
    public TradingRoomOptions: any[] = [];
public CurrencyOptions: any[] = [];

    // Define autocomplete

    //---------------------------------------------------------

    constructor(activatedRoute: ActivatedRoute) {

       super(activatedRoute);
       this.validationForm = this.formBuilder.group(stockShareUntypedFormGroup(this.formBuilder)); // Use Angular Validation Controls

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
    override initEntity(): IStockShare  {
        return initStockShare ();
    }
    //---------------------------------------------------------
    override setChildrenRequests() {
        let presetCurrentPriceRequest: ICurrentPriceRequest = initCurrentPriceRequest();
        this.presetCurrentPriceFilter = presetCurrentPriceRequest.filter;
        if (this.presetCurrentPriceFilter) {
            this.presetCurrentPriceFilter.StockShareId = this.entity.id;
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
              tradingRoomResponse,
currencyResponse,

         ] = await Promise.all
         ([
        this.apiService.getItems<IQueryResponse>("/TradingRoom/list", { pageSize: 30 }),
this.apiService.getItems<IQueryResponse>("/Currency/list", { pageSize: 30 }),

         ]);
        this.TradingRoomOptions = tradingRoomResponse.items;
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
        return getStockShareActions(this, record);
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
