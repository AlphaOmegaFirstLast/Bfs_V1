import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { debounceTime, distinctUntilChanged, filter, switchMap, finalize, mergeMap } from 'rxjs/operators';
//Template_Component_AutoComplete_1

import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { NgbAlertModule } from '@ng-bootstrap/ng-bootstrap';
import { NgbNavModule } from '@ng-bootstrap/ng-bootstrap';
import { NgbPopoverModule } from '@ng-bootstrap/ng-bootstrap';
import { NgIcon } from '@ng-icons/core';
import { BaseFormComponent } from '@bfs/_shared/components/base-form.component';
import { IEntity, IQueryResponse, IAction, ILookup } from '@bfs/_shared/interfaces';

//----------------------- System Specific -------------------------- 
import { MasterService } from '@bfs/master-main/master.service';

//---------------------- Component Specific ------------------------
import { type IBfsField, initBfsField, bfsFieldUntypedFormGroup, getBfsFieldActions } from './bfs-field.shared';
//import { IAutoComplete, setAuto, isAutoShowError, isAutoShowSpin, onAutoFocus, isAutoShowList, onAutoSelect, hideAutoOverlay } from '@bfs/_shared/helpers/auto-complete.helper';
import { IAutoComplete, AutoCompleteHelper } from '@bfs/_shared/helpers/auto-complete.class';

@Component({
    selector: 'bfs-field-form',
    imports: [
    CommonModule, NgIcon, NgbPopoverModule, NgbAlertModule, FormsModule, ReactiveFormsModule, NgbDropdownModule, NgbNavModule, RouterLink],
    standalone: true,
    templateUrl: './bfs-field.form.component.html',
})
export class BfsFieldFormComponent extends BaseFormComponent<IBfsField> implements OnInit {

    override apiUrl = '/BfsField/';
    override apiService: MasterService = inject(MasterService);
    override componentName: string = 'BfsField'.toLowerCase();  // used to grab its related custom field definitions

    // Children filters

    // Define look ups
    public FilterTypeOptions: any[] = [];
    public BackendDataTypeOptions: any[] = [];

   // isAutoShowError:any; isAutoShowSpin:any; onAutoSelect:any; onAutoFocus:any; isAutoShowList:any; hideAutoOverlay:any;

    //autoBfsComponent: IAutoComplete = { queryUrl: "/BfsComponent/list", fieldName: 'bfsComponent', control:null, id: '', name: '', showDropDown: false, options: [], isLoading: false, isInitial: true };
    bfsComponentAuto: AutoCompleteHelper = new AutoCompleteHelper(this.apiService,{ queryUrl: "/BfsComponent/list", fieldName: 'bfsComponent', control:null, id: '', name: '', showDropDown: false, options: [], isLoading: false, isInitial: true } as IAutoComplete);
    //---------------------------------------------------------

    constructor(activatedRoute: ActivatedRoute) {
        super(activatedRoute);
        this.validationForm = this.formBuilder.group(bfsFieldUntypedFormGroup(this.formBuilder)); // Use Angular Validation Controls
        this.bfsComponentAuto.control = this.validationForm.get('bfsComponentName');

        // this.isAutoShowSpin = isAutoShowSpin;
        // this.onAutoFocus = onAutoFocus;
        // this.isAutoShowList = isAutoShowList;
        // this.onAutoSelect = onAutoSelect;
        // this.hideAutoOverlay = hideAutoOverlay;
        // this.isAutoShowError = isAutoShowError;
      //  this.bfsComponent = new AutoCompleteHelper(this.apiService, this.autoBfsComponent);
    }
    //---------------------------------------------------------
    override async ngOnInit(): Promise<void> {
        this.setChildrenRequests();
        await this.getCustomFieldDefinitions();
        await this.getLookups();
        await this.getObjectFieldLookups();
        await this.setAutoComplete();

        if (this.entity.id != '0') {
            await this.view();
        }
    }
    //---------------------------------------------------------
    override initEntity(): IBfsField {
        return initBfsField();
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
        try {
            const [
                filterTypeResponse,
                backendDataTypeResponse,
            ] = await Promise.all
                ([
                    this.apiService.getItems<IQueryResponse>("/FilterType/list", { pageSize: 300 }),
                    this.apiService.getItems<IQueryResponse>("/BackendDataType/list", { pageSize: 300 }),
                ]);
            this.FilterTypeOptions = filterTypeResponse.items;
            this.BackendDataTypeOptions = backendDataTypeResponse.items;
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
        // this.autoBfsComponent.name = this.entity.bfsComponentName;
        // this.autoBfsComponent.id = this.entity.bfsComponentId;
     //   await setAuto(this.apiService, this.validationForm,this.entity, this.autoBfsComponent);
     
    await this.bfsComponentAuto.setOnChangeHandler(this);
    }
    //---------------------------------------------------------

    override async setDataAutoComplete() {
        this.bfsComponentAuto.setData(this.entity.bfsComponentId,this.entity.bfsComponentName);
    }
    //---------------------------------------------------------
    override getActions(record: IEntity): IAction[] {
        return getBfsFieldActions(this, record);
    }
}

