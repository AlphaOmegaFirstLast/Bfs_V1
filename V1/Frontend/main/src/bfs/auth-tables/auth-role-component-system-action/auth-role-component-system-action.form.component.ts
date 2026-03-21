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
import { type IAuthRoleComponentSystemAction, type IAuthRoleComponentSystemActionRequest, initAuthRoleComponentSystemAction, authRoleComponentSystemActionUntypedFormGroup } from './auth-role-component-system-action.shared';
import { getAuthRoleComponentSystemActionActions,  initAuthRoleComponentSystemActionRequest } from './auth-role-component-system-action.shared';
import { InfrastructureService } from '@bfs/infrastructure-main/infrastructure.service';

@Component({
    selector: 'auth-role-component-system-action-form',
    imports: [

    CommonModule, NgIcon, NgbPopoverModule, NgbAlertModule, FormsModule, ReactiveFormsModule, NgbDropdownModule, NgbNavModule,RouterLink],
    standalone: true,
    templateUrl: './auth-role-component-system-action.form.component.html',
})
export class AuthRoleComponentSystemActionFormComponent extends BaseFormComponent<IAuthRoleComponentSystemAction > implements OnInit {

    bfsService: InfrastructureService = inject(InfrastructureService);
    override apiUrl =  '/AuthRoleComponentSystemAction/';
    override apiService: AuthService = inject(AuthService);
    override componentName: string = 'AuthRoleComponentSystemAction'.toLowerCase();  // used to grab its related custom field definitions

    // Children filters

    // Define look ups
    public BfsComponentOptions: any[] = [];
public SystemActionOptions: any[] = [];
public AuthRoleOptions: any[] = [];

    //---------------------------------------------------------

    constructor(activatedRoute: ActivatedRoute) {

        super(activatedRoute);
        this.validationForm = this.formBuilder.group(authRoleComponentSystemActionUntypedFormGroup(this.formBuilder)); // Use Angular Validation Controls
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
    override initEntity(): IAuthRoleComponentSystemAction  {
        return initAuthRoleComponentSystemAction ();
    }
    //---------------------------------------------------------
    override setChildrenRequests() {

    }
    //---------------------------------------------------------
    override async getLookups(): Promise<void> {
        this.messages = [];
        let target = '';
        this.isLoading = true;
        target = "/BfsComponent/list";
        (await this.bfsService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.BfsComponentOptions = response.items;
                this.isLoading = false;
            },
                error: (err: any) => {
                this.isLoading = false;
                var msg = err.message || 'An error occurred while fetching Component Name data.';
                this.messages.push({ text: msg, msgType: "danger" });
            }
        });
this.isLoading = true;
        target = "/SystemAction/list";
        (await this.bfsService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.SystemActionOptions = response.items;
                this.isLoading = false;
            },
                error: (err: any) => {
                this.isLoading = false;
                var msg = err.message || 'An error occurred while fetching System Action data.';
                this.messages.push({ text: msg, msgType: "danger" });
            }
        });
this.isLoading = true;
        target = "/AuthRole/list";
        (await this.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.AuthRoleOptions = response.items;
                this.isLoading = false;
            },
                error: (err: any) => {
                this.isLoading = false;
                var msg = err.message || 'An error occurred while fetching Role data.';
                this.messages.push({ text: msg, msgType: "danger" });
            }
        });

    }
    //---------------------------------------------------------
    getRecordLinks(record: IEntity): ViewLink[] {
        let actions = getAuthRoleComponentSystemActionActions(this,record);
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
        let actions = getAuthRoleComponentSystemActionActions(this,record);
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
