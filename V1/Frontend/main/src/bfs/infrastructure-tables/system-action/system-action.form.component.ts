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
import { InfrastructureService } from '@bfs/infrastructure-main/infrastructure.service';

//---------------------- Component Specific ------------------------
import { type ISystemAction, type ISystemActionRequest, initSystemAction, systemActionUntypedFormGroup } from './system-action.shared';
import { getSystemActionActions,  initSystemActionRequest } from './system-action.shared';

@Component({
    selector: 'system-action-form',
    imports: [

    CommonModule, NgIcon, NgbPopoverModule, NgbAlertModule, FormsModule, ReactiveFormsModule, NgbDropdownModule, NgbNavModule,RouterLink],
    standalone: true,
    templateUrl: './system-action.form.component.html',
})
export class SystemActionFormComponent extends BaseFormComponent<ISystemAction > implements OnInit {

    override apiUrl =  '/SystemAction/';
    override apiService: InfrastructureService = inject(InfrastructureService);
    override componentName: string = 'SystemAction'.toLowerCase();  // used to grab its related custom field definitions

    // Children filters

    // Define look ups
    public ActionTypeOptions: any[] = [];
public WriterTypeOptions: any[] = [];

    //---------------------------------------------------------

    constructor(activatedRoute: ActivatedRoute) {

        super(activatedRoute);
        this.validationForm = this.formBuilder.group(systemActionUntypedFormGroup(this.formBuilder)); // Use Angular Validation Controls
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
    override initEntity(): ISystemAction  {
        return initSystemAction ();
    }
    //---------------------------------------------------------
    override setChildrenRequests() {

    }
    //---------------------------------------------------------
    override async getLookups(): Promise<void> {
        this.messages = [];
        let target = '';
        this.isLoading = true;
        target = "/ActionType/list";
        (await this.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.ActionTypeOptions = response.items;
                this.isLoading = false;
            },
                error: (err: any) => {
                this.isLoading = false;
                var msg = err.message || 'An error occurred while fetching Action Type data.';
                this.messages.push({ text: msg, msgType: "danger" });
            }
        });
this.isLoading = true;
        target = "/WriterType/list";
        (await this.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.WriterTypeOptions = response.items;
                this.isLoading = false;
            },
                error: (err: any) => {
                this.isLoading = false;
                var msg = err.message || 'An error occurred while fetching Writer Type data.';
                this.messages.push({ text: msg, msgType: "danger" });
            }
        });

    }
    //---------------------------------------------------------
    getRecordLinks(record: IEntity): ViewLink[] {
        let actions = getSystemActionActions(this,record);
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
        let actions = getSystemActionActions(this,record);
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

//Template_Start_Code_DontOverwrite_1

//Template_End_Code_DontOverwrite_1

