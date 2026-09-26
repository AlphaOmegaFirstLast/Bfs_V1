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
import { MasterService } from '@bfs/master/main/master.service';

//---------------------- Component Specific ------------------------
import {isEnabled, isVisible, type IBfsTenant, initBfsTenant, bfsTenantUntypedFormGroup, getBfsTenantActions} from './bfs-tenant.shared';
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

import {BfsTenantSystemMatrixComponent} from "./bfs-tenant-system.matrix.component"
import {IBfsTenantSystemFilter, IBfsTenantSystemRequest, initBfsTenantSystemRequest} from "../bfs-tenant-system/bfs-tenant-system.shared"

@Component({
    selector: 'bfs-tenant-form',
    imports: [

    BfsTenantSystemMatrixComponent,

    CommonModule, NgIcon, NgbPopoverModule, NgbAlertModule, FormsModule, ReactiveFormsModule, NgbDropdownModule, NgbNavModule,RouterLink],
    standalone: true,
    templateUrl: './bfs-tenant.form.component.html',
})
export class BfsTenantFormComponent extends BaseFormComponent<IBfsTenant > implements OnInit {

    override apiUrl =  '/BfsTenant/';
    override apiService: MasterService = inject(MasterService);
    override componentName: string = 'BfsTenant'.toLowerCase();  // used to grab its related custom field definitions

    // Children filters

    presetBfsTenantSystemFilter: IBfsTenantSystemFilter | undefined;

    // Define look ups

    // Define autocomplete

    //---------------------------------------------------------

    constructor(activatedRoute: ActivatedRoute) {

       super(activatedRoute);
       this.validationForm = this.formBuilder.group(bfsTenantUntypedFormGroup(this.formBuilder)); // Use Angular Validation Controls

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
    override initEntity(): IBfsTenant  {
        return initBfsTenant ();
    }
    //---------------------------------------------------------
    override setChildrenRequests() {

        let presetBfsTenantSystemRequest: IBfsTenantSystemRequest = initBfsTenantSystemRequest();
        this.presetBfsTenantSystemFilter = presetBfsTenantSystemRequest.filter;
        if (this.presetBfsTenantSystemFilter) {
            this.presetBfsTenantSystemFilter.BfsTenantId = this.entity.id;
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

    override getActions(record: IEntity): IAction[] {
        return getBfsTenantActions(this, record);
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
