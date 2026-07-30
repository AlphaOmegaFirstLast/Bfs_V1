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
import { type IBfsTenantSystem, type IBfsTenantSystemRequest, initBfsTenantSystem, bfsTenantSystemUntypedFormGroup } from './bfs-tenant-system.shared';
import { getBfsTenantSystemActions,  initBfsTenantSystemRequest } from './bfs-tenant-system.shared';

@Component({
    selector: 'bfs-tenant-system-form',
    imports: [

    CommonModule, NgIcon, NgbPopoverModule, NgbAlertModule, FormsModule, ReactiveFormsModule, NgbDropdownModule, NgbNavModule,RouterLink],
    standalone: true,
    templateUrl: './bfs-tenant-system.form.component.html',
})
export class BfsTenantSystemFormComponent extends BaseFormComponent<IBfsTenantSystem > implements OnInit {

    override apiUrl =  '/BfsTenantSystem/';
    override apiService: MasterService = inject(MasterService);
    override componentName: string = 'BfsTenantSystem'.toLowerCase();  // used to grab its related custom field definitions

    // Children filters

    // Define look ups
    public BfsTenantOptions: any[] = [];
public BfsSystemOptions: any[] = [];

    // Define autocomplete

    //---------------------------------------------------------

    constructor(activatedRoute: ActivatedRoute) {

       super(activatedRoute);
       this.validationForm = this.formBuilder.group(bfsTenantSystemUntypedFormGroup(this.formBuilder)); // Use Angular Validation Controls

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
    override initEntity(): IBfsTenantSystem  {
        return initBfsTenantSystem ();
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
        target = "/BfsTenant/list";
        (await this.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.BfsTenantOptions = response.items;
                this.isLoading.lookups = false;
            },
                error: (err: any) => {
                this.isLoading.lookups = false;
                var msg = err.message || 'An error occurred while fetching Tenant Name data.';
                this.messages.push({ text: msg, msgType: "danger" });
            }
        });
this.isLoading.lookups = true;
        target = "/BfsSystem/list";
        (await this.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.BfsSystemOptions = response.items;
                this.isLoading.lookups = false;
            },
                error: (err: any) => {
                this.isLoading.lookups = false;
                var msg = err.message || 'An error occurred while fetching BestFit System data.';
                this.messages.push({ text: msg, msgType: "danger" });
            }
        });

    }
    //---------------------------------------------------------

    getRecordLinks(record: IEntity): ViewLink[] {
        let actions = getBfsTenantSystemActions(this,record);
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
        let actions = getBfsTenantSystemActions(this,record);
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

