import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { FormsModule, ReactiveFormsModule} from '@angular/forms';
import { NgbNavModule, NgbPopoverModule, NgbAlertModule, NgbDropdownModule} from '@ng-bootstrap/ng-bootstrap';
import { NgIcon } from '@ng-icons/core';
import { takeUntil } from 'rxjs/operators';
import { FlatpickrDirective, provideFlatpickrDefaults } from 'angularx-flatpickr';

import { BaseFormComponent } from '@bfs/_shared/components/base-form.component';
import { IEntity, IQueryResponse, IAction } from '@bfs/_shared/interfaces';

//----------------------- System Specific -------------------------- 
import { StockExService } from '@bfs/stockex/main/stockex.service';

//---------------------- Component Specific ------------------------
import {fields,type ICoupon, initCoupon, couponUntypedFormGroup, getCouponActions} from './coupon.shared';
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

@Component({
    selector: 'coupon-form',
    imports: [

    CommonModule, FormsModule, ReactiveFormsModule,RouterLink, 
    NgIcon, NgbNavModule, NgbPopoverModule, NgbAlertModule, NgbDropdownModule
, FlatpickrDirective

    ],
    standalone: true,
    templateUrl: './coupon.form.component.html',
    providers: [
  provideFlatpickrDefaults()

    ],
})
export class CouponFormComponent extends BaseFormComponent<ICoupon > implements OnInit {

    override apiUrl =  '/Coupon/';
    override apiService: StockExService = inject(StockExService);
    override componentName: string = 'Coupon'.toLowerCase();  // used to grab its related custom field definitions

    // Children filters

    // Define look ups
    public TradingRoomOptions: any[] = [];
public StockShareOptions: any[] = [];
public CouponTypeOptions: any[] = [];
public CouponStatusOptions: any[] = [];

    // Define autocomplete

    fields:any=fields;
    //---------------------------------------------------------

    constructor(activatedRoute: ActivatedRoute) {

       super(activatedRoute);
       this.validationForm = this.formBuilder.group(couponUntypedFormGroup(this.formBuilder)); // Use Angular Validation Controls

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
        //Template_Start_Code_DontOverwrite_2
this.validationForm.valueChanges
            .pipe(takeUntil(this.destroy$))
            .subscribe(formValue => {
                // Read values safely
                var couponTypeId = this.validationForm.get("couponTypeId")?.value || 0;
                const couponPercentControl = this.validationForm.get('couponPercent');
                const valueControl = this.validationForm.get('value'); // <-- Fixed typo here (was couponPercent)
                const quantityControl = this.validationForm.get('quantity');
                const bonusQuantityControl = this.validationForm.get('bonusQuantity'); // <-- Fixed typo here (was couponPercent)

                if (couponTypeId ==1) {
                    const value = this.validationForm.get("value")?.value || 0;
                    const percent = this.validationForm.get("couponPercent")?.value || 0;

                    // Correct mutually exclusive logic
                    if (value > 0) {
                        this.fields.value.enabled = true;
                        this.fields.couponPercent.enabled = false;
                    } else if (percent > 0) {
                        this.fields.value.enabled = false;
                        this.fields.couponPercent.enabled = true;
                    } else {
                        // If both are 0/empty, allow entering into either
                        this.fields.value.enabled = true;
                        this.fields.couponPercent.enabled = true;
                    }

                    // Apply state changes with { emitEvent: false } to prevent infinite loops
                    if (this.fields.couponPercent.enabled) {
                        couponPercentControl?.enable({ emitEvent: false });
                    } else {
                        couponPercentControl?.disable({ emitEvent: false });
                    }

                    if (this.fields.value.enabled) {
                        valueControl?.enable({ emitEvent: false });
                    } else {
                        valueControl?.disable({ emitEvent: false });
                    }
                    quantityControl?.disable({ emitEvent: false });
                    bonusQuantityControl?.disable({ emitEvent: false });
                    quantityControl?.setValue(0,{ emitEvent: false });
                    bonusQuantityControl?.setValue(0,{ emitEvent: false });
                }

                if (couponTypeId == 2) {
                    quantityControl?.enable({ emitEvent: false });
                    bonusQuantityControl?.enable({ emitEvent: false });
                    valueControl?.disable({ emitEvent: false });
                    couponPercentControl?.disable({ emitEvent: false });
                    valueControl?.setValue(0,{ emitEvent: false });
                    couponPercentControl?.setValue(0,{ emitEvent: false });
                }
            });
//Template_End_Code_DontOverwrite_2

    }
    //---------------------------------------------------------
    override initEntity(): ICoupon  {
        return initCoupon ();
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
              tradingRoomResponse,
stockShareResponse,
couponTypeResponse,
couponStatusResponse,

         ] = await Promise.all
         ([
        this.apiService.getItems<IQueryResponse>("/TradingRoom/list", { pageSize: 30 }),
this.apiService.getItems<IQueryResponse>("/StockShare/list", { pageSize: 30 }),
this.apiService.getItems<IQueryResponse>("/CouponType/list", { pageSize: 30 }),
this.apiService.getItems<IQueryResponse>("/CouponStatus/list", { pageSize: 30 }),

         ]);
        this.TradingRoomOptions = tradingRoomResponse.items;
this.StockShareOptions = stockShareResponse.items;
this.CouponTypeOptions = couponTypeResponse.items;
this.CouponStatusOptions = couponStatusResponse.items;

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
        return getCouponActions(this, record);
    }
   //---------------------------------------------------------

//Template_Start_Code_DontOverwrite_3
//Template_End_Code_DontOverwrite_3

}
