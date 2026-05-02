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
import { AuthService } from '@bfs/auth-main/auth.service';

//---------------------- Component Specific ------------------------
import { type IRole, type IRoleRequest, initRole, roleUntypedFormGroup } from './role.shared';
import { getRoleActions,  initRoleRequest } from './role.shared';

import {RoleAppListComponent} from "../role-app/role-app.list.component"
import {IRoleAppFilter, IRoleAppRequest, initRoleAppRequest} from "../role-app/role-app.shared"
import {RoleUserListComponent} from "../role-user/role-user.list.component"
import {IRoleUserFilter, IRoleUserRequest, initRoleUserRequest} from "../role-user/role-user.shared"

import {RoleComponentSystemActionMatrixComponent} from "./role-component-system-action.matrix.component"
import {IRoleComponentSystemActionFilter, IRoleComponentSystemActionRequest, initRoleComponentSystemActionRequest} from "../role-component-system-action/role-component-system-action.shared"

@Component({
    selector: 'role-form',
    imports: [
    RoleAppListComponent,
RoleUserListComponent,

    RoleComponentSystemActionMatrixComponent,

    CommonModule, NgIcon, NgbPopoverModule, NgbAlertModule, FormsModule, ReactiveFormsModule, NgbDropdownModule, NgbNavModule,RouterLink],
    standalone: true,
    templateUrl: './role.form.component.html',
})
export class RoleFormComponent extends BaseFormComponent<IRole > implements OnInit {

    override apiUrl =  '/Role/';
    override apiService: AuthService = inject(AuthService);
    override componentName: string = 'Role'.toLowerCase();  // used to grab its related custom field definitions

    // Children filters
    presetRoleAppFilter: IRoleAppFilter | undefined;
presetRoleUserFilter: IRoleUserFilter | undefined;

    presetRoleComponentSystemActionFilter: IRoleComponentSystemActionFilter | undefined;

    // Define look ups

    // Define autocomplete

    //---------------------------------------------------------

    constructor(activatedRoute: ActivatedRoute) {

       super(activatedRoute);
       this.validationForm = this.formBuilder.group(roleUntypedFormGroup(this.formBuilder)); // Use Angular Validation Controls

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
    override initEntity(): IRole  {
        return initRole ();
    }
    //---------------------------------------------------------
    override setChildrenRequests() {
        let presetRoleAppRequest: IRoleAppRequest = initRoleAppRequest();
        this.presetRoleAppFilter = presetRoleAppRequest.filter;
        if (this.presetRoleAppFilter) {
            this.presetRoleAppFilter.RoleId = this.entity.id;
        }
let presetRoleUserRequest: IRoleUserRequest = initRoleUserRequest();
        this.presetRoleUserFilter = presetRoleUserRequest.filter;
        if (this.presetRoleUserFilter) {
            this.presetRoleUserFilter.RoleId = this.entity.id;
        }

        let presetRoleComponentSystemActionRequest: IRoleComponentSystemActionRequest = initRoleComponentSystemActionRequest();
        this.presetRoleComponentSystemActionFilter = presetRoleComponentSystemActionRequest.filter;
        if (this.presetRoleComponentSystemActionFilter) {
            this.presetRoleComponentSystemActionFilter.RoleId = this.entity.id;
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
        let actions = getRoleActions(this,record);
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
        let actions = getRoleActions(this,record);
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

