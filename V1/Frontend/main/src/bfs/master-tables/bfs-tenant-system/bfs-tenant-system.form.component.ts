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
import { MasterService } from '@bfs/master-main/master.service';
import { IAutoComplete, AutoCompleteHelper } from '@bfs/_shared/helpers/auto-complete.class';

//---------------------- Component Specific ------------------------
import { type IBfsTenantSystem, initBfsTenantSystem, bfsTenantSystemUntypedFormGroup, getBfsTenantSystemActions} from './bfs-tenant-system.shared';

@Component({
    selector: 'bfs-tenant-system-form',
    imports: [

    CommonModule, NgIcon, NgbPopoverModule, NgbAlertModule, FormsModule, ReactiveFormsModule, NgbDropdownModule, NgbNavModule,RouterLink],
    standalone: true,
    templateUrl: './bfs-tenant-system.form.component.html',
})
export class BfsTenantSystemFormComponent extends BaseFormComponent<IBfsTenantSystem > implements OnInit {

    override apiUrl =  '/BfsTenantSystem/';
    override apiService: MasterService = inject(MasterService);
    override componentName: string = 'BfsTenantSystem'.toLowerCase();  // used to grab its related custom field definitions

    // Children filters

    // Define look ups
    public BfsTenantOptions: any[] = [];

    // Define autocomplete
    bfsSystemAuto: AutoCompleteHelper = new AutoCompleteHelper({ queryUrl: "/BfsSystem/list", fieldName: 'bfsSystem', control:null, id: '', name: '', showDropDown: false, options: [], isLoading: false, isInitial: true } as IAutoComplete);

    //---------------------------------------------------------

    constructor(activatedRoute: ActivatedRoute) {

       super(activatedRoute);
       this.validationForm = this.formBuilder.group(bfsTenantSystemUntypedFormGroup(this.formBuilder)); // Use Angular Validation Controls
      this.bfsSystemAuto.control = this.validationForm.get('bfsSystemName');

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
    override initEntity(): IBfsTenantSystem  {
        return initBfsTenantSystem ();
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
              bfsTenantResponse,

         ] = await Promise.all
         ([
        this.apiService.getItems<IQueryResponse>("/BfsTenant/list", { pageSize: 30 }),

         ]);
        this.BfsTenantOptions = bfsTenantResponse.items;

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
    //---------------------------------------------------------
    override async setAutoComplete() {   
       await this.bfsSystemAuto.setUpForm(this.apiService, this);
    }
    //---------------------------------------------------------
    // required to set the control with the entity initial values
    override async setDataAutoComplete() {
        this.bfsSystemAuto.setData(this.entity.bfsSystemId,this.entity.bfsSystemName);
    }
    //---------------------------------------------------------

    override getActions(record: IEntity): IAction[] {
        return getBfsTenantSystemActions(this, record);
    }
}

