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
import { IEntity, IQueryResponse, IAction } from '@bfs/_shared/interfaces';

//----------------------- System Specific -------------------------- 
import { MasterService } from '@bfs/master/main/master.service';

//---------------------- Component Specific ------------------------
import {isEnabled, isVisible, type IBfsComponent, initBfsComponent, bfsComponentUntypedFormGroup, getBfsComponentActions} from './bfs-component.shared';
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

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

    // Define autocomplete

    //---------------------------------------------------------

    constructor(activatedRoute: ActivatedRoute) {

       super(activatedRoute);
       this.validationForm = this.formBuilder.group(bfsComponentUntypedFormGroup(this.formBuilder)); // Use Angular Validation Controls

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
         try{
         const [
              bfsSystemResponse,
dataTypeResponse,

         ] = await Promise.all
         ([
        this.apiService.getItems<IQueryResponse>("/BfsSystem/list", { pageSize: 30 }),
this.apiService.getItems<IQueryResponse>("/DataType/list", { pageSize: 30 }),

         ]);
        this.BfsSystemOptions = bfsSystemResponse.items;
this.DataTypeOptions = dataTypeResponse.items;

 } catch (err: any) {
   const msg = err?.message || "An error occurred while loading data.";
   this.messages.push({ text: msg, msgType: "danger" });
 } finally {
   this.isLoading.lookups = false;
 }
 /*
        this.isLoading.lookups = true;
        target = "/[LookupNameCapital]/list";
        (await this.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.[LookupNameCapital]Options = response.items;
                this.isLoading.lookups = false;
            },
                error: (err: any) => {
                this.isLoading.lookups = false;
                var msg = err.message || 'An error occurred while fetching [DisplayName] data.';
                this.messages.push({ text: msg, msgType: "danger" });
            }
        });
        */
    }
    //---------------------------------------------------------

    override getActions(record: IEntity): IAction[] {
        return getBfsComponentActions(this, record);
    }

   //---------------------------------------------------------
    isVisible(fieldName: string): boolean {

        return isVisible(this.entity,this.validationForm, fieldName);
    }
    //--------------------------------------------------------------
    isEnabled(fieldName: string): boolean {

        return isEnabled(this.entity,this.validationForm, fieldName);
    }
    //--------------------------------------------------------------

//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

}
