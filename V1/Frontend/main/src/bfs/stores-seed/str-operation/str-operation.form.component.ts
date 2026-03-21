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
import { StoresService } from '@bfs/stores-main/stores.service';

//---------------------- Component Specific ------------------------
import { type IStrOperation, type IStrOperationRequest, initStrOperation, strOperationUntypedFormGroup } from './str-operation.shared';
import { getStrOperationActions,  initStrOperationRequest } from './str-operation.shared';

@Component({
    selector: 'str-operation-form',
    imports: [

    CommonModule, NgIcon, NgbPopoverModule, NgbAlertModule, FormsModule, ReactiveFormsModule, NgbDropdownModule, NgbNavModule,RouterLink],
    standalone: true,
    templateUrl: './str-operation.form.component.html',
})
export class StrOperationFormComponent extends BaseFormComponent<IStrOperation > implements OnInit {

    override apiUrl =  '/StrOperation/';
    override apiService: StoresService = inject(StoresService);
    override componentName: string = 'StrOperation'.toLowerCase();  // used to grab its related custom field definitions

    // Children filters

    // Define look ups
    public StrEffectTypeOptions: any[] = [];
public StrThirdPartyTypeOptions: any[] = [];

    //---------------------------------------------------------

    constructor(activatedRoute: ActivatedRoute) {

        super(activatedRoute);
        this.validationForm = this.formBuilder.group(strOperationUntypedFormGroup(this.formBuilder)); // Use Angular Validation Controls
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
    override initEntity(): IStrOperation  {
        return initStrOperation ();
    }
    //---------------------------------------------------------
    override setChildrenRequests() {

    }
    //---------------------------------------------------------
    override async getLookups(): Promise<void> {
        this.messages = [];
        this.isLoading = true;
        let target = '';
        target = "/StrEffectType/list";
        (await this.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.StrEffectTypeOptions = response.items;
                this.isLoading = false;
            },
                error: (err: any) => {
                this.isLoading = false;
                var msg = err.message || 'An error occurred while fetching Effect Type data.';
                this.messages.push({ text: msg, msgType: "danger" });
            }
        });
target = "/StrThirdPartyType/list";
        (await this.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.StrThirdPartyTypeOptions = response.items;
                this.isLoading = false;
            },
                error: (err: any) => {
                this.isLoading = false;
                var msg = err.message || 'An error occurred while fetching Third Party Type data.';
                this.messages.push({ text: msg, msgType: "danger" });
            }
        });

    }
    //---------------------------------------------------------
    getRecordLinks(record: IEntity): ViewLink[] {
        let actions = getStrOperationActions(this,record);
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
        let actions = getStrOperationActions(this,record);
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
