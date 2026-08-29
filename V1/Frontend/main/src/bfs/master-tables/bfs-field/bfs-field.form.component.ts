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
import { debounceTime, distinctUntilChanged, filter, switchMap, finalize, mergeMap } from 'rxjs/operators';

//----------------------- System Specific -------------------------- 
import { MasterService } from '@bfs/master-main/master.service';

//---------------------- Component Specific ------------------------
import { type IBfsField, type IBfsFieldRequest, initBfsField, bfsFieldUntypedFormGroup } from './bfs-field.shared';
import { getBfsFieldActions,  initBfsFieldRequest } from './bfs-field.shared';

@Component({
    selector: 'bfs-field-form',
    imports: [

    CommonModule, NgIcon, NgbPopoverModule, NgbAlertModule, FormsModule, ReactiveFormsModule, NgbDropdownModule, NgbNavModule,RouterLink],
    standalone: true,
    templateUrl: './bfs-field.form.component.html',
})
export class BfsFieldFormComponent extends BaseFormComponent<IBfsField > implements OnInit {

    override apiUrl =  '/BfsField/';
    override apiService: MasterService = inject(MasterService);
    override componentName: string = 'BfsField'.toLowerCase();  // used to grab its related custom field definitions

    // Children filters

    // Define look ups
    public FilterTypeOptions: any[] = [];
public BackendDataTypeOptions: any[] = [];

    // Define autocomplete
    showBfsComponent = false; // Toggle for the overlay
    bfsComponentOptions: any[] = [];
    bfsComponentControl: any;

    //---------------------------------------------------------

    constructor(activatedRoute: ActivatedRoute) {

       super(activatedRoute);
       this.validationForm = this.formBuilder.group(bfsFieldUntypedFormGroup(this.formBuilder)); // Use Angular Validation Controls
      this.bfsComponentControl = this.validationForm.get('bfsComponentName') as any;

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
    override initEntity(): IBfsField  {
        return initBfsField ();
    }
    //---------------------------------------------------------
    override setChildrenRequests() {

    }
    //---------------------------------------------------------
    override async setAutoComplete() {
    await this.bfsComponentAutoComplete();

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
        target = "/FilterType/list";
        (await this.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.FilterTypeOptions = response.items;
                this.isLoading.lookups = false;
            },
                error: (err: any) => {
                this.isLoading.lookups = false;
                var msg = err.message || 'An error occurred while fetching Filter Type data.';
                this.messages.push({ text: msg, msgType: "danger" });
            }
        });
this.isLoading.lookups = true;
        target = "/BackendDataType/list";
        (await this.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.BackendDataTypeOptions = response.items;
                this.isLoading.lookups = false;
            },
                error: (err: any) => {
                this.isLoading.lookups = false;
                var msg = err.message || 'An error occurred while fetching Backend Type data.';
                this.messages.push({ text: msg, msgType: "danger" });
            }
        });

    }
    //---------------------------------------------------------
    async bfsComponentAutoComplete() {
        this.validationForm.get('bfsComponentName')?.valueChanges.pipe(
            // 1. Only proceed if input length >= 2
            filter(val => val && val.length >= 2),
            // 2. Wait 300ms after last keystroke to avoid API spam
            debounceTime(300),
            // 3. Only trigger if the value actually changed
            distinctUntilChanged(),
            // 4. Switch to API call
            switchMap(async (searchTerm) => {
                this.isLoading.autoComplete = true;
                this.showBfsComponent = this.entity.bfsComponentName != searchTerm; // Show dropdown when searching starts
                try {
                    const request = { pageSize: 20, filter: { name: searchTerm } };
                    const response: any = await this.apiService.postAutoComplete("/BfsComponent/list", request);
                    return response.items; // Return the data directly
                } catch (err: any) {
                    this.messages.push({ text: err.msg || "Error fetching data", msgType: "danger" });
                    return []; // Return empty array on error to keep the stream alive
                } finally {
                    this.isLoading.autoComplete = false;
                }
            })
        ).subscribe(items => {
            this.bfsComponentOptions = items;
        });
    }
    //---------------------------------------------------------
    selectBfsComponent(selectedOption: any) {
        // Set the input value to the selected name
        this.validationForm.get('bfsComponentName')?.setValue(selectedOption.name, { emitEvent: false });
        this.validationForm.get('bfsComponentId')?.setValue(selectedOption.id, { emitEvent: false });
        // Update your hidden form control or parent logic here
        this.bfsComponentOptions = [];
        this.showBfsComponent = false;
    }
    //---------------------------------------------------------
    // Close dropdown when input loses focus (with a slight delay to allow clicks)
    hideBfsComponentOverlay() {
        setTimeout(() => this.showBfsComponent = false, 200);
    }
    //---------------------------------------------------------

    getRecordLinks(record: IEntity): ViewLink[] {
        let actions = getBfsFieldActions(this,record);
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
        let actions = getBfsFieldActions(this,record);
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

