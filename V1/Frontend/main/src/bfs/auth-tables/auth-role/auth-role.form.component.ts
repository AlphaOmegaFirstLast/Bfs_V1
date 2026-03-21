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
import { type IAuthRole, type IAuthRoleRequest, initAuthRole, authRoleUntypedFormGroup } from './auth-role.shared';
import { getAuthRoleActions,  initAuthRoleRequest } from './auth-role.shared';

import {AuthRoleAppListComponent} from "../auth-role-app/auth-role-app.list.component"
import {IAuthRoleAppFilter, IAuthRoleAppRequest, initAuthRoleAppRequest} from "../auth-role-app/auth-role-app.shared"
import {AuthRoleUserListComponent} from "../auth-role-user/auth-role-user.list.component"
import {IAuthRoleUserFilter, IAuthRoleUserRequest, initAuthRoleUserRequest} from "../auth-role-user/auth-role-user.shared"

import {AuthRoleComponentSystemActionMatrixComponent} from "./auth-role-component-system-action.matrix.component"
import {IAuthRoleComponentSystemActionFilter, IAuthRoleComponentSystemActionRequest, initAuthRoleComponentSystemActionRequest} from "../auth-role-component-system-action/auth-role-component-system-action.shared"
import { InfrastructureService } from '@bfs/infrastructure-main/infrastructure.service';

@Component({
    selector: 'auth-role-form',
    imports: [
    AuthRoleAppListComponent,
AuthRoleUserListComponent,

    AuthRoleComponentSystemActionMatrixComponent,

    CommonModule, NgIcon, NgbPopoverModule, NgbAlertModule, FormsModule, ReactiveFormsModule, NgbDropdownModule, NgbNavModule,RouterLink],
    standalone: true,
    templateUrl: './auth-role.form.component.html',
})
export class AuthRoleFormComponent extends BaseFormComponent<IAuthRole > implements OnInit {

    override apiUrl =  '/AuthRole/';
    override apiService: AuthService = inject(AuthService);
    override componentName: string = 'AuthRole'.toLowerCase();  // used to grab its related custom field definitions

    // Children filters
    presetAuthRoleAppFilter: IAuthRoleAppFilter | undefined;
presetAuthRoleUserFilter: IAuthRoleUserFilter | undefined;

    presetAuthRoleComponentSystemActionFilter: IAuthRoleComponentSystemActionFilter | undefined;

    // Define look ups

    //---------------------------------------------------------

    constructor(activatedRoute: ActivatedRoute) {

        super(activatedRoute);
        this.validationForm = this.formBuilder.group(authRoleUntypedFormGroup(this.formBuilder)); // Use Angular Validation Controls
    }
    //---------------------------------------------------------
    override async ngOnInit(): Promise<void> {
        this.setChildrenRequests();
        await this.getCustomFieldDefinitions();
        await this.getLookups();
        await this.getObjectFieldLookups();

        if (this.entity.id != '0') {
            this.view();
        }
    }
    //---------------------------------------------------------
    override initEntity(): IAuthRole  {
        return initAuthRole ();
    }
    //---------------------------------------------------------
    override setChildrenRequests() {
        let presetAuthRoleAppRequest: IAuthRoleAppRequest = initAuthRoleAppRequest();
        this.presetAuthRoleAppFilter = presetAuthRoleAppRequest.filter;
        if (this.presetAuthRoleAppFilter) {
            this.presetAuthRoleAppFilter.AuthRoleId = this.entity.id;
        }
let presetAuthRoleUserRequest: IAuthRoleUserRequest = initAuthRoleUserRequest();
        this.presetAuthRoleUserFilter = presetAuthRoleUserRequest.filter;
        if (this.presetAuthRoleUserFilter) {
            this.presetAuthRoleUserFilter.AuthRoleId = this.entity.id;
        }

        let presetAuthRoleComponentSystemActionRequest: IAuthRoleComponentSystemActionRequest = initAuthRoleComponentSystemActionRequest();
        this.presetAuthRoleComponentSystemActionFilter = presetAuthRoleComponentSystemActionRequest.filter;
        if (this.presetAuthRoleComponentSystemActionFilter) {
            this.presetAuthRoleComponentSystemActionFilter.AuthRoleId = this.entity.id;
        }

    }
    //---------------------------------------------------------
    override async getLookups(): Promise<void> {
        this.messages = [];
        let target = '';

    }
    //---------------------------------------------------------
    getRecordLinks(record: IEntity): ViewLink[] {
        let actions = getAuthRoleActions(this,record);
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
        let actions = getAuthRoleActions(this,record);
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
