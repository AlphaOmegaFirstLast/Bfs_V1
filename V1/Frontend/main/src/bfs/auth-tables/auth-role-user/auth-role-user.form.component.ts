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
import { type IAuthRoleUser, type IAuthRoleUserRequest, initAuthRoleUser, authRoleUserUntypedFormGroup } from './auth-role-user.shared';
import { getAuthRoleUserActions,  initAuthRoleUserRequest } from './auth-role-user.shared';

@Component({
    selector: 'auth-role-user-form',
    imports: [

    CommonModule, NgIcon, NgbPopoverModule, NgbAlertModule, FormsModule, ReactiveFormsModule, NgbDropdownModule, NgbNavModule,RouterLink],
    standalone: true,
    templateUrl: './auth-role-user.form.component.html',
})
export class AuthRoleUserFormComponent extends BaseFormComponent<IAuthRoleUser > implements OnInit {

    override apiUrl =  '/AuthRoleUser/';
    override apiService: AuthService = inject(AuthService);
    override componentName: string = 'AuthRoleUser'.toLowerCase();  // used to grab its related custom field definitions

    // Children filters

    // Define look ups
    public AuthRoleOptions: any[] = [];
public AuthUserOptions: any[] = [];

    //---------------------------------------------------------

    constructor(activatedRoute: ActivatedRoute) {

        super(activatedRoute);
        this.validationForm = this.formBuilder.group(authRoleUserUntypedFormGroup(this.formBuilder)); // Use Angular Validation Controls
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
    override initEntity(): IAuthRoleUser  {
        return initAuthRoleUser ();
    }
    //---------------------------------------------------------
    override setChildrenRequests() {

    }
    //---------------------------------------------------------
    override async getLookups(): Promise<void> {
        this.messages = [];
        let target = '';
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
this.isLoading = true;
        target = "/AuthUser/list";
        (await this.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.AuthUserOptions = response.items;
                this.isLoading = false;
            },
                error: (err: any) => {
                this.isLoading = false;
                var msg = err.message || 'An error occurred while fetching Users data.';
                this.messages.push({ text: msg, msgType: "danger" });
            }
        });

    }
    //---------------------------------------------------------
    getRecordLinks(record: IEntity): ViewLink[] {
        let actions = getAuthRoleUserActions(this,record);
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
        let actions = getAuthRoleUserActions(this,record);
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
