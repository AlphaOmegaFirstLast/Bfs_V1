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
import {isEnabled, isVisible, type IPaymentMode, initPaymentMode, paymentModeUntypedFormGroup, getPaymentModeActions} from './payment-mode.shared';
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

@Component({
    selector: 'payment-mode-form',
    imports: [

    CommonModule, FormsModule, ReactiveFormsModule,RouterLink, 
    NgIcon, NgbNavModule, NgbPopoverModule, NgbAlertModule, NgbDropdownModule

    ],
    standalone: true,
    templateUrl: './payment-mode.form.component.html',
    providers: [

    ],
})
export class PaymentModeFormComponent extends BaseFormComponent<IPaymentMode > implements OnInit {

    override apiUrl =  '/PaymentMode/';
    override apiService: StockExService = inject(StockExService);
    override componentName: string = 'PaymentMode'.toLowerCase();  // used to grab its related custom field definitions

    // Children filters

    // Define look ups

    // Define autocomplete

    //---------------------------------------------------------

    constructor(activatedRoute: ActivatedRoute) {

       super(activatedRoute);
       this.validationForm = this.formBuilder.group(paymentModeUntypedFormGroup(this.formBuilder)); // Use Angular Validation Controls

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
    }
    //---------------------------------------------------------
    override initEntity(): IPaymentMode  {
        return initPaymentMode ();
    }
    //---------------------------------------------------------
    override setChildrenRequests() {

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
        return getPaymentModeActions(this, record);
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
