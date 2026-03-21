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
import { type IAuthApp, type IAuthAppRequest, initAuthApp, authAppUntypedFormGroup } from './auth-app.shared';
import { getAuthAppActions,  initAuthAppRequest } from './auth-app.shared';
import { InfrastructureService } from '@bfs/infrastructure-main/infrastructure.service';

@Component({
    selector: 'auth-app-form',
    imports: [

    CommonModule, NgIcon, NgbPopoverModule, NgbAlertModule, FormsModule, ReactiveFormsModule, NgbDropdownModule, NgbNavModule,RouterLink],
    standalone: true,
    templateUrl: './auth-app.form.component.html',
})
export class AuthAppFormComponent extends BaseFormComponent<IAuthApp > implements OnInit {

    bfsService: InfrastructureService = inject(InfrastructureService);
    override apiUrl =  '/AuthApp/';
    override apiService: AuthService = inject(AuthService);
    override componentName: string = 'AuthApp'.toLowerCase();  // used to grab its related custom field definitions

    // Children filters

    // Define look ups
    public BfsSystemOptions: any[] = [];

    //---------------------------------------------------------

    constructor(activatedRoute: ActivatedRoute) {

        super(activatedRoute);
        this.validationForm = this.formBuilder.group(authAppUntypedFormGroup(this.formBuilder)); // Use Angular Validation Controls
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
    override initEntity(): IAuthApp  {
        return initAuthApp ();
    }
    //---------------------------------------------------------
    override setChildrenRequests() {

    }
    //---------------------------------------------------------
    override async getLookups(): Promise<void> {
        this.messages = [];
        let target = '';
        this.isLoading = true;
        target = "/BfsSystem/list";
        (await this.bfsService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.BfsSystemOptions = response.items;
                this.isLoading = false;
            },
                error: (err: any) => {
                this.isLoading = false;
                var msg = err.message || 'An error occurred while fetching BestFit System data.';
                this.messages.push({ text: msg, msgType: "danger" });
            }
        });

    }
    //---------------------------------------------------------
    getRecordLinks(record: IEntity): ViewLink[] {
        let actions = getAuthAppActions(this,record);
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
        let actions = getAuthAppActions(this,record);
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
