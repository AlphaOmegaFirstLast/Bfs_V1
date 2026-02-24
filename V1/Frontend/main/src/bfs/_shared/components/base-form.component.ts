import { Component, Directive, inject, OnInit } from '@angular/core';
//import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';
//import { FormsModule, ReactiveFormsModule, FormBuilder, Validators,  ValidationErrors } from '@angular/forms';

import { UntypedFormBuilder, UntypedFormArray, type UntypedFormGroup, AbstractControl } from '@angular/forms';
import { ClipboardService } from '@core/services/clipboard.service';

import { IQueryResponse, ILookup, IUIMessage, IEntity , ICustomFieldDefinitionRecord, ViewLink, ActionLink} from '@bfs/_shared/interfaces';
import { getFormControlValidation, getFormInfoLookups } from '@bfs/_shared/objectFields';
import { getMatrixInfoLookups, getReportInfoLookups, getToolTipInfoLookups } from '@bfs/_shared/objectFields';

import { initCustomField, ICustomField } from '@bfs/_shared/customFields';
//------------------------------------------- Component Specific ------------------------------------------------

@Directive()
export class BaseFormComponent<Entity extends IEntity> implements OnInit {

    public apiUrl = '';
    public apiService!: any;
    public entityDisplayName: string = '';
    public componentName: string = '';

    private clipboard = inject(ClipboardService);
    public formBuilder = inject(UntypedFormBuilder);
    public validationForm!: UntypedFormGroup;
    public customFieldFormControlList!: UntypedFormArray['controls'];
    public route: ActivatedRoute;
    public submit: boolean = false;
    public isLoading: boolean = false;
    public currentOperation: string = '';
    public parent: any;
    public messages: IUIMessage[] = [];

    entity: Entity;
    me: any = this;
    //-----------------------Object Fields Lookups----------------------------------
    public ChartElementOptions: any[] = []; //{ id: number, name: string }[] = [];
    public AggregateTypeOptions: any[] = []; //{ id: number, name: string }[] = [];
    public FormControlTypeOptions: any[] = [];//{ id: number, name: string }[] = [];
    public ActionLocationOptions: any[] = [];

    constructor(public activatedRoute: ActivatedRoute) {

        let entityId = '0';
        this.route = activatedRoute;
        if (this.route.snapshot.url.length == 4) {
            this.currentOperation = this.route.snapshot.url[this.route.snapshot.url.length - 2].path;
            entityId = this.route.snapshot.url[this.route.snapshot.url.length - 1].path;
        }

        this.entity = this.initEntity();
        this.entity.id = entityId ? entityId : '0';
    }
    //---------------------------------------------------------
    async ngOnInit(): Promise<void> {
        this.setChildrenRequests();
        await this.getCustomFieldDefinitions();
        await this.getLookups();
        await this.getObjectFieldLookups();
        if (this.entity.id != '0') {
            this.view();
        }
    }
    //---------------------------------------------------------
    initEntity(): Entity {
        return {} as Entity;
    }
    //---------------------------------------------------------
    async getLookups(): Promise<void> {
        this.messages = [];
        this.isLoading = true;
        let target = '';
    }
    //---------------------------------------------------------
    setChildrenRequests() {
    }
    //---------------------------------------------------------
    async getObjectFieldLookups() {
        await getReportInfoLookups(this);
        await getMatrixInfoLookups(this);
        await getToolTipInfoLookups(this);
        await getFormInfoLookups(this);
    }
    //---------------------------------------------------------
    getValidationErrorMessage(fieldName: string, control: any): string {
        if (!control || !control.errors || !control.touched) return '';

        // Find the first error key (e.g., 'required')
        const firstErrorKey = Object.keys(control.errors)[0];

        if (firstErrorKey.includes('required')) return `${fieldName} is required.`;
        else if (firstErrorKey.includes('email')) return `${fieldName} is not a valid email address.`;
        else if (firstErrorKey.includes('minlength')) return `${fieldName} must be at least ${control.errors?.['minlength'].requiredLength} characters long.`;
        else if (firstErrorKey.includes('maxlength')) return `${fieldName} cannot be more than ${control.errors?.['maxlength'].requiredLength} characters long.`;
        else if (firstErrorKey.includes('minvalue')) return `${fieldName} must be at least ${control.errors?.['minvalue'].minValue} characters long.`;
        else if (firstErrorKey.includes('maxvalue')) return `${fieldName} must be at most ${control.errors?.['maxvalue'].maxValue} characters long.`;
        else if (firstErrorKey.includes('pattern')) return `${fieldName} has an invalid format.`;
        else if (firstErrorKey.includes('notallowed')) return `${fieldName} has an unallowed value.`;

        return `Invalid ${fieldName}.`;
    }
    //---------------------------------------------------------
    // Example: populate customFields from an existing entity
    //fb.group({...}) creates a new UntypedFormGroup with three controls: customFieldDefinitionId, name, and value.

    setCustomFieldControls(customFieldDefinitionList?: ICustomFieldDefinitionRecord[]) {

        let newCustomFields = []
        let controls = this.validationForm.controls;
        let customFieldsFormArray = controls['customFields'] as UntypedFormArray;
        let customFields = this.entity.customFields || [];
        let definitionList = customFieldDefinitionList || [];
        definitionList = definitionList.filter(x => x.bfsComponentName?.toLowerCase() == this.componentName);


        for (var definition of definitionList) {
            let foundCustomField = customFields.find(x => x.customFieldDefinitionId == definition.id);
            var customField = initCustomField(definition, foundCustomField);
            newCustomFields.push(customField);

            // Build a FormGroup controls for current customField
            var fieldValidators = getFormControlValidation(definition?.fieldValidation);
            var formGroup = this.formBuilder.group({
                customFieldDefinitionId: [customField.customFieldDefinitionId],
                name: [customField.name],
                value: [customField.value, fieldValidators]
            });

            // adds this new FormGroup into the FormArray.
            customFieldsFormArray.push(formGroup);
        }

        this.entity.customFields = newCustomFields;
        this.customFieldFormControlList = customFieldsFormArray?.controls || [];
    }
    //---------------------------------------------------------

    async getCustomFieldDefinitions(): Promise<void> {
        let target = '';
        target = '/CustomFieldDefinition/list';
        (await this.apiService.post(target, { pageSize: 30 })).subscribe({
            next: (response: IQueryResponse) => {
                this.isLoading = false;
                this.setCustomFieldControls(response.items);
            },
            error: (err: any) => {
                this.isLoading = false;
                var msg = err.message || `An error occurred while fetching ${this.entityDisplayName} data.`;
                this.messages.push({ text: msg, msgType: "danger" });
            }
        });
    }
    //---------------------------------------------------------
    copyFromElement(el: HTMLElement) {
        const value = (el as HTMLInputElement).value || el.innerText;
        this.clipboard.highlightElementText(el);
        this.clipboard.copyText(value)
    }
    //---------------------------------------------------------
    async view() {
        var target = this.apiUrl + this.entity.id;
        (await this.apiService.get(target)).subscribe({
            next: (response: Entity) => {
                this.entity = response;
                this.validationForm.patchValue(this.entity);
                this.isLoading = false;
            },
            error: (err: any) => {
                this.isLoading = false;
                var msg = err.message || `An error occurred while fetching ${this.entityDisplayName} data.`;
                this.messages.push({ text: msg, msgType: "danger" });
            }
        });
    }
    //---------------------------------------------------------
    async add() {
        var target = this.apiUrl;
        (await this.apiService.post(target, this.entity)).subscribe({
            next: (response: Entity) => {
                this.isLoading = false;
                this.submit = false;
                this.validationForm.patchValue(this.entity);
                this.messages.push({ text: `${this.entityDisplayName} was added successfully`, msgType: "info" });
            },
            error: (err: any) => {
                this.isLoading = false;
                var msg = err.message || `An error occurred while adding ${this.entityDisplayName} data.`;
                this.messages.push({ text: msg, msgType: "danger" });
            }
        });
    }
    //---------------------------------------------------------
    async update() {
        var target = this.apiUrl;
        (await this.apiService.put(target, this.entity)).subscribe({
            next: (response: Entity) => {
                this.isLoading = false;
                this.submit = false;
                this.validationForm.patchValue(this.entity);
                this.messages.push({ text: `${this.entityDisplayName} was updated successfully`, msgType: "info" });

            },
            error: (err: any) => {
                this.isLoading = false;
                var msg = err.message || `An error occurred while updating ${this.entityDisplayName} data.`;
                this.messages.push({ text: msg, msgType: "danger" });
            }
        });
    }
    //---------------------------------------------------------
    async delete() {
        var target = this.apiUrl + this.entity.id;
        (await this.apiService.delete(target)).subscribe({
            next: (response: Entity) => {
                this.isLoading = false;
                this.submit = false;
                this.entity = this.initEntity();
                this.validationForm.patchValue(this.entity);
                this.messages.push({ text: `${this.entityDisplayName} was deleted successfully`, msgType: "info" });
            },
            error: (err: any) => {
                this.isLoading = false;
                var msg = err.message || `An error occurred while deleting ${this.entityDisplayName} data.`;
                this.messages.push({ text: msg, msgType: "danger" });
            }
        });
    }
    //---------------------------------------------------------
    validSubmit() {
        this.submit = true
        if (this.validationForm.valid) {
            // One-time sync to entity object
            this.entity = this.validationForm.getRawValue();
            // this.validationForm.disable();
            this.messages = [];
            if (!this.isLoading) {  // to prevent multiple requests
                this.isLoading = true;
                this.applyOperation();
            }
        }
    }
    //---------------------------------------------------------
    applyOperation() {
        switch (this.currentOperation) {
            case 'add':
                this.add();
                break;
            case 'edit':
                this.update();
                break;
            case 'view':
                this.view();
                break;
            case 'delete':
                this.delete();
                break;
            default:
                console.error('Unknown operation:', this.currentOperation);
                break;
        }
    }
    //---------------------------------------------------------
      
    isAccessible(linkOrAction: ViewLink | ActionLink): boolean {
           return true;
    }
    //--------------------------------------------------------- 
    get form() {
        return this.validationForm.controls
    }
    //---------------------------------------------------------
    resetForm() {
        this.submit = false
        this.entity = this.initEntity();
        this.validationForm.patchValue(this.entity);
    }
    //---------------------------------------------------------
    
}

