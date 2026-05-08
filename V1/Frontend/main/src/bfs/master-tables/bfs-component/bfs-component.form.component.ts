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
import { type IBfsComponent, type IBfsComponentRequest, initBfsComponent, bfsComponentUntypedFormGroup } from './bfs-component.shared';
import { getBfsComponentActions,  initBfsComponentRequest } from './bfs-component.shared';

import {BfsFieldListComponent} from "../bfs-field/bfs-field.list.component"
import {IBfsFieldFilter, IBfsFieldRequest, initBfsFieldRequest} from "../bfs-field/bfs-field.shared"

import {BfsComponentSystemActionMatrixComponent} from "./bfs-component-system-action.matrix.component"
import {IBfsComponentSystemActionFilter, IBfsComponentSystemActionRequest, initBfsComponentSystemActionRequest} from "../bfs-component-system-action/bfs-component-system-action.shared"
import {BfsComponentBusinessActionMatrixComponent} from "./bfs-component-business-action.matrix.component"
import {IBfsComponentBusinessActionFilter, IBfsComponentBusinessActionRequest, initBfsComponentBusinessActionRequest} from "../bfs-component-business-action/bfs-component-business-action.shared"

@Component({
    selector: 'bfs-component-form',
    imports: [
    BfsFieldListComponent,

    BfsComponentSystemActionMatrixComponent,
BfsComponentBusinessActionMatrixComponent,

    CommonModule, NgIcon, NgbPopoverModule, NgbAlertModule, FormsModule, ReactiveFormsModule, NgbDropdownModule, NgbNavModule,RouterLink],
    standalone: true,
    templateUrl: './bfs-component.form.component.html',
})
export class BfsComponentFormComponent extends BaseFormComponent<IBfsComponent > implements OnInit {

    override apiUrl =  '/BfsComponent/';
    override apiService: MasterService = inject(MasterService);
    override componentName: string = 'BfsComponent'.toLowerCase();  // used to grab its related custom field definitions

    // Children filters
    presetBfsFieldFilter: IBfsFieldFilter | undefined;

    presetBfsComponentSystemActionFilter: IBfsComponentSystemActionFilter | undefined;
presetBfsComponentBusinessActionFilter: IBfsComponentBusinessActionFilter | undefined;

    // Define look ups
    public BfsSystemOptions: any[] = [];
public DataTypeOptions: any[] = [];

    //---------------------------------------------------------

    constructor(activatedRoute: ActivatedRoute) {

        super(activatedRoute);
        this.validationForm = this.formBuilder.group(bfsComponentUntypedFormGroup(this.formBuilder)); // Use Angular Validation Controls
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
    override initEntity(): IBfsComponent  {
        return initBfsComponent ();
    }
    //---------------------------------------------------------
    override setChildrenRequests() {
        let presetBfsFieldRequest: IBfsFieldRequest = initBfsFieldRequest();
        this.presetBfsFieldFilter = presetBfsFieldRequest.filter;
        if (this.presetBfsFieldFilter) {
            this.presetBfsFieldFilter.BfsComponentId = this.entity.id;
        }

        let presetBfsComponentSystemActionRequest: IBfsComponentSystemActionRequest = initBfsComponentSystemActionRequest();
        this.presetBfsComponentSystemActionFilter = presetBfsComponentSystemActionRequest.filter;
        if (this.presetBfsComponentSystemActionFilter) {
            this.presetBfsComponentSystemActionFilter.BfsComponentId = this.entity.id;
        }
let presetBfsComponentBusinessActionRequest: IBfsComponentBusinessActionRequest = initBfsComponentBusinessActionRequest();
        this.presetBfsComponentBusinessActionFilter = presetBfsComponentBusinessActionRequest.filter;
        if (this.presetBfsComponentBusinessActionFilter) {
            this.presetBfsComponentBusinessActionFilter.BfsComponentId = this.entity.id;
        }

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
this.isLoading.lookups = true;
        target = "/DataType/list";
        (await this.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.DataTypeOptions = response.items;
                this.isLoading.lookups = false;
            },
                error: (err: any) => {
                this.isLoading.lookups = false;
                var msg = err.message || 'An error occurred while fetching Data Type data.';
                this.messages.push({ text: msg, msgType: "danger" });
            }
        });

    }
    //---------------------------------------------------------
    getRecordLinks(record: IEntity): ViewLink[] {
        let actions = getBfsComponentActions(this,record);
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
        let actions = getBfsComponentActions(this,record);
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

