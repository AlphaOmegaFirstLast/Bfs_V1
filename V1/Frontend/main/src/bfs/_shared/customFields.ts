import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, ValidationErrors, FormBuilder } from "@angular/forms";

export interface ICustomField {
    customFieldDefinitionId?: string;
    name?: string;
    value?: string;
}
//------------------------------------------------
export function initCustomField(): ICustomField {
    return {
        customFieldDefinitionId: '0',
        name: '',
        value: ''
    }
}
//---------------------------------------------------------
// Fields of an Entity [used in Entity form]
export function customFieldUntypedFormGroup(formBuilder: FormBuilder): UntypedFormGroup {
    return formBuilder.group({
        customFieldDefinitionId: [''],
        name: [''],
        value: ['']
    })
};
//------------------------------------------------
export function initCustomFields(): ICustomField[] {
    return [];
}
//---------------------------------------------------------
