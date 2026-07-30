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
import { MasterService } from '@bfs/master-main/master.service';

//---------------------- Component Specific ------------------------
import { type IBfsTenant, type IBfsTenantRequest, initBfsTenant, bfsTenantUntypedFormGroup } from './bfs-tenant.shared';
import { getBfsTenantActions,  initBfsTenantRequest } from './bfs-tenant.shared';

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

    }
    //---------------------------------------------------------

    getRecordLinks(record: IEntity): ViewLink[] {
        let actions = getBfsTenantActions(this,record);
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
        let actions = getBfsTenantActions(this,record);
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

