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
import { type IStrCurrency, type IStrCurrencyRequest, initStrCurrency, strCurrencyUntypedFormGroup } from './str-currency.shared';
import { getStrCurrencyActions,  initStrCurrencyRequest } from './str-currency.shared';

@Component({
    selector: 'str-currency-form',
    imports: [

    CommonModule, NgIcon, NgbPopoverModule, NgbAlertModule, FormsModule, ReactiveFormsModule, NgbDropdownModule, NgbNavModule,RouterLink],
    standalone: true,
    templateUrl: './str-currency.form.component.html',
})
export class StrCurrencyFormComponent extends BaseFormComponent<IStrCurrency > implements OnInit {

    override apiUrl =  '/StrCurrency/';
    override apiService: StoresService = inject(StoresService);
    override componentName: string = 'StrCurrency'.toLowerCase();  // used to grab its related custom field definitions

    // Children filters

    // Define look ups

    //---------------------------------------------------------

    constructor(activatedRoute: ActivatedRoute) {

        super(activatedRoute);
        this.validationForm = this.formBuilder.group(strCurrencyUntypedFormGroup(this.formBuilder)); // Use Angular Validation Controls
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
    override initEntity(): IStrCurrency  {
        return initStrCurrency ();
    }
    //---------------------------------------------------------
    override setChildrenRequests() {

    }
    //---------------------------------------------------------
    override async getLookups(): Promise<void> {
        this.messages = [];
        let target = '';

    }
    //---------------------------------------------------------
    getRecordLinks(record: IEntity): ViewLink[] {
        let actions = getStrCurrencyActions(this,record);
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
        let actions = getStrCurrencyActions(this,record);
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
