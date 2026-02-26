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
import { type IBfsClient, type IBfsClientRequest, initBfsClient, bfsClientUntypedFormGroup } from './bfs-client.shared';
import { getBfsClientActions,  initBfsClientRequest } from './bfs-client.shared';

import {BfsClientSystemMatrixComponent} from "./bfs-client-system.matrix.component"
import {IBfsClientSystemFilter, IBfsClientSystemRequest, initBfsClientSystemRequest} from "../bfs-client-system/bfs-client-system.shared"

@Component({
    selector: 'bfs-client-form',
    imports: [

    BfsClientSystemMatrixComponent,

    CommonModule, NgIcon, NgbPopoverModule, NgbAlertModule, FormsModule, ReactiveFormsModule, NgbDropdownModule, NgbNavModule,RouterLink],
    standalone: true,
    templateUrl: './bfs-client.form.component.html',
})
export class BfsClientFormComponent extends BaseFormComponent<IBfsClient > implements OnInit {

    override apiUrl =  '/BfsClient/';
    override apiService: InfrastructureService = inject(InfrastructureService);
    override componentName: string = 'BfsClient'.toLowerCase();  // used to grab its related custom field definitions

    // Children filters

    presetBfsClientSystemFilter: IBfsClientSystemFilter | undefined;

    // Define look ups

    //---------------------------------------------------------

    constructor(activatedRoute: ActivatedRoute) {

        super(activatedRoute);
        this.validationForm = this.formBuilder.group(bfsClientUntypedFormGroup(this.formBuilder)); // Use Angular Validation Controls
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
    override initEntity(): IBfsClient  {
        return initBfsClient ();
    }
    //---------------------------------------------------------
    override setChildrenRequests() {

        let presetBfsClientSystemRequest: IBfsClientSystemRequest = initBfsClientSystemRequest();
        this.presetBfsClientSystemFilter = presetBfsClientSystemRequest.filter;
        if (this.presetBfsClientSystemFilter) {
            this.presetBfsClientSystemFilter.BfsClientId = this.entity.id;
        }

    }
    //---------------------------------------------------------
    override async getLookups(): Promise<void> {
        this.messages = [];
        this.isLoading = true;
        let target = '';

    }
    //---------------------------------------------------------
    getRecordLinks(record: IEntity): ViewLink[] {
        let actions = getBfsClientActions(this,record);
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
        let actions = getBfsClientActions(this,record);
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
//Template_Start_Code_DontOverwrite_2

//Template_End_Code_DontOverwrite_2
//Template_Start_Code_DontOverwrite_3

//Template_End_Code_DontOverwrite_3

