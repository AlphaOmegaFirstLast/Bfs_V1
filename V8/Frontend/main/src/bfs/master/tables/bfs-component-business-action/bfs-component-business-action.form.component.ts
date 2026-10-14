import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { FormsModule, ReactiveFormsModule} from '@angular/forms';
import { NgbNavModule, NgbPopoverModule, NgbAlertModule, NgbDropdownModule} from '@ng-bootstrap/ng-bootstrap';
import { NgIcon } from '@ng-icons/core';

import { BaseFormComponent } from '@bfs/_shared/components/base-form.component';
import { IEntity, IQueryResponse, IAction } from '@bfs/_shared/interfaces';

//----------------------- System Specific -------------------------- 
import { MasterService } from '@bfs/master/main/master.service';
import { IAutoComplete, AutoCompleteHelper } from '@bfs/_shared/helpers/auto-complete.class';

//---------------------- Component Specific ------------------------
import {isEnabled, isVisible, type IBfsComponentBusinessAction, initBfsComponentBusinessAction, bfsComponentBusinessActionUntypedFormGroup, getBfsComponentBusinessActionActions} from './bfs-component-business-action.shared';
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

@Component({
    selector: 'bfs-component-business-action-form',
    imports: [

    CommonModule, FormsModule, ReactiveFormsModule,RouterLink, 
    NgIcon, NgbNavModule, NgbPopoverModule, NgbAlertModule, NgbDropdownModule

    ],
    standalone: true,
    templateUrl: './bfs-component-business-action.form.component.html',
    providers: [

    ],
})
export class BfsComponentBusinessActionFormComponent extends BaseFormComponent<IBfsComponentBusinessAction > implements OnInit {

    override apiUrl =  '/BfsComponentBusinessAction/';
    override apiService: MasterService = inject(MasterService);
    override componentName: string = 'BfsComponentBusinessAction'.toLowerCase();  // used to grab its related custom field definitions

    // Children filters

    // Define look ups
    public BusinessActionOptions: any[] = [];
override ActionLocationOptions: any[] = [];

    // Define autocomplete
    bfsComponentAuto: AutoCompleteHelper = new AutoCompleteHelper({ queryUrl: "/BfsComponent/list", fieldName: 'bfsComponent', control:null, id: '', name: '', showDropDown: false, options: [], isLoading: false, isInitial: true } as IAutoComplete);

    //---------------------------------------------------------

    constructor(activatedRoute: ActivatedRoute) {

       super(activatedRoute);
       this.validationForm = this.formBuilder.group(bfsComponentBusinessActionUntypedFormGroup(this.formBuilder)); // Use Angular Validation Controls
      this.bfsComponentAuto.control = this.validationForm.get('bfsComponentName');

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

        this.linkList = this.getRecordLinks(this);
        this.actionList = this.getRecordActions(this);    
        this.buttonList = this.getRecordButtons(this);
    }
    //---------------------------------------------------------
    override initEntity(): IBfsComponentBusinessAction  {
        return initBfsComponentBusinessAction ();
    }
    //---------------------------------------------------------
    override setChildrenRequests() {

    }
    //---------------------------------------------------------
    override async getLookups(): Promise<void> {
        this.messages = [];
        let target = '';
        this.isLoading.lookups = true;
// Promise.all to improve performance. apply later
         try{
         const [
              businessActionResponse,
actionLocationResponse,

         ] = await Promise.all
         ([
        this.apiService.getItems<IQueryResponse>("/BusinessAction/list", { pageSize: 30 }),
this.apiService.getItems<IQueryResponse>("/ActionLocation/list", { pageSize: 30 }),

         ]);
        this.BusinessActionOptions = businessActionResponse.items;
this.ActionLocationOptions = actionLocationResponse.items;

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
    override async setAutoComplete() {   
    await this.bfsComponentAuto.setUpForm(this.apiService, this);

    }
    //---------------------------------------------------------
    // required to set the control with the entity initial values
    override async setDataAutoComplete() {
    this.bfsComponentAuto.setData(this.entity.bfsComponentId,this.entity.bfsComponentName);

    }
    //---------------------------------------------------------

    override getActions(record: IEntity): IAction[] {
        return getBfsComponentBusinessActionActions(this, record);
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
