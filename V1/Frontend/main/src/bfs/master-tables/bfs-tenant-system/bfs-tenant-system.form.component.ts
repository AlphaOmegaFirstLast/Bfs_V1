import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { debounceTime, distinctUntilChanged, filter, switchMap, finalize, mergeMap } from 'rxjs/operators';
import { of, from } from 'rxjs';

import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { NgbAlertModule } from '@ng-bootstrap/ng-bootstrap';
import { NgbNavModule } from '@ng-bootstrap/ng-bootstrap';
import { NgbPopoverModule } from '@ng-bootstrap/ng-bootstrap';
import { NgIcon } from '@ng-icons/core';
import { BaseFormComponent } from '@bfs/_shared/components/base-form.component';
import { IQueryResponse, ILookup, IUIMessage, IQueryColumn, ActionLink, ViewLink, IEntity } from '@bfs/_shared/interfaces';

//----------------------- System Specific -------------------------- 
import { MasterService } from '@bfs/master-main/master.service';

//---------------------- Component Specific ------------------------
import { type IBfsTenantSystem, type IBfsTenantSystemRequest, initBfsTenantSystem, bfsTenantSystemUntypedFormGroup } from './bfs-tenant-system.shared';
import { getBfsTenantSystemActions, initBfsTenantSystemRequest } from './bfs-tenant-system.shared';

@Component({
    selector: 'bfs-tenant-system-form',
    imports: [

        CommonModule, NgIcon, NgbPopoverModule, NgbAlertModule, FormsModule, ReactiveFormsModule, NgbDropdownModule, NgbNavModule, RouterLink],
    standalone: true,
    templateUrl: './bfs-tenant-system.form.component.html',
})
export class BfsTenantSystemFormComponent extends BaseFormComponent<IBfsTenantSystem> implements OnInit {

    override apiUrl = '/BfsTenantSystem/';
    override apiService: MasterService = inject(MasterService);
    override componentName: string = 'BfsTenantSystem'.toLowerCase();  // used to grab its related custom field definitions

    // Children filters

    // Define look ups
    public BfsSystemOptions: any[] = [];

    // Define autocomplete
    showTenantOptions = false; // Toggle for the overlay
    BfsTenantOptions: any[] = [];
    searchControl: any;

    //---------------------------------------------------------

    constructor(activatedRoute: ActivatedRoute) {

        super(activatedRoute);
        this.validationForm = this.formBuilder.group(bfsTenantSystemUntypedFormGroup(this.formBuilder)); // Use Angular Validation Controls
        this.searchControl = this.validationForm.get('bfsTenantName') as any;
    }
    //---------------------------------------------------------
    // to test smoking gun
    override async ngOnInit(): Promise<void> {
        this.setChildrenRequests();
        await this.getCustomFieldDefinitions();
        await this.getLookups();
        await this.setAutoComplete();
        await this.getObjectFieldLookups();

        if (this.entity.id != '0') {
            this.view();
        }
    }
    //---------------------------------------------------------
    override initEntity(): IBfsTenantSystem {
        return initBfsTenantSystem();
    }
    //---------------------------------------------------------
    override setChildrenRequests() {
    }
    //---------------------------------------------------------
    override async setAutoComplete() {
        await this.bfsTenantAutoComplete();
    }
    //---------------------------------------------------------
    override async getLookups(): Promise<void> {
        this.messages = [];
        let target = '';
        this.isLoading = true;
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
        //   this.isLoading = false;
        // }

        // this.isLoading = true;
        // target = "/BfsTenant/list";
        // (await this.apiService.post(target,  {pageSize:50})).subscribe({
        //     next: (response: IQueryResponse) => {
        //         this.BfsTenantOptions = response.items;
        //         this.isLoading = false;
        //     },
        //         error: (err: any) => {
        //         this.isLoading = false;
        //         var msg = err.message || 'An error occurred while fetching Tenant Name data.';
        //         this.messages.push({ text: msg, msgType: "danger" });
        //     }
        // });
        this.isLoading = true;
        target = "/BfsSystem/list";
        (await this.apiService.post(target, { pageSize: 50 })).subscribe({
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
    async bfsTenantAutoComplete() {
     //   this.validationForm.get('bfsTenantName')?.setValue("tenant initial setup");

        this.validationForm.get('bfsTenantName')?.valueChanges.pipe(
            // 1. Only proceed if input length >= 2
            filter(val => val && val.length >= 2),
            // 2. Wait 300ms after last keystroke to avoid API spam
            debounceTime(300),
            // 3. Only trigger if the value actually changed
            distinctUntilChanged(),
            // 4. Switch to API call
            switchMap(async (searchTerm) => {
                this.isLoading = true;
                this.showTenantOptions = this.entity.bfsTenantName != searchTerm; // Show dropdown when searching starts
                try {
                    const request = { pageSize: 20, filter: { name: searchTerm } };
                    const response: any = await this.apiService.postAutoComplete("/BfsTenant/list", request);
                    return response.items; // Return the data directly
                } catch (err: any) {
                    this.messages.push({ text: err.msg || "Error fetching data", msgType: "danger" });
                    return []; // Return empty array on error to keep the stream alive
                } finally {
                    this.isLoading = false;
                }
            })
        ).subscribe(items => {
            this.BfsTenantOptions = items;
        });
    }
    //---------------------------------------------------------
    selectTenant(tenant: any) {
        // Set the input value to the selected name
        this.validationForm.get('bfsTenantName')?.setValue(tenant.name, { emitEvent: false });
        this.validationForm.get('bfsTenantId')?.setValue(tenant.id, { emitEvent: false });
        // Update your hidden form control or parent logic here
        this.BfsTenantOptions = [];
        this.showTenantOptions = false;
    }
    //---------------------------------------------------------
    // Close dropdown when input loses focus (with a slight delay to allow clicks)
    hideOverlay() {
        setTimeout(() => this.showTenantOptions = false, 200);
    }
    //---------------------------------------------------------    
  
    getRecordLinks(record: IEntity): ViewLink[] {
        let actions = getBfsTenantSystemActions(this, record);
        let links: ViewLink[] = actions.filter(action =>
            action.actionType == 'FrontendLink'
            && action.actionLocation == 'FormHeader'
        ).map(action => {
            return { recordId: action.recordId, route: action.route ?? '', displayText: action.displayText }
        });

        return links;
    }
    //---------------------------------------------------------
    getRecordActions(record: IEntity): ActionLink[] {
        let actions = getBfsTenantSystemActions(this, record);
        let links: ActionLink[] = actions.filter(action =>
            action.actionType == 'FrontendFunction'
            && action.actionLocation == 'FormHeader'
        ).map(action => {
            return { recordId: action.recordId, action: action.action ?? null, displayText: action.displayText, data: action.data }
        });

        return links;
    }
    //--------------------------------------------------------------

}

