import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, ValidationErrors, FormBuilder } from "@angular/forms";
import { ICustomFieldDefinition } from "@bfs/bestfit-tables/custom-field-definition/custom-field-definition.shared";
import { ICustomFieldDefinitionRecord } from "./interfaces";

export interface ICustomField {
    customFieldDefinitionId?: string;
    name?: string;
    value?: string;
}
//------------------------------------------------
export function initCustomField(definition: ICustomFieldDefinitionRecord, customField?: ICustomField ): ICustomField {
    return {
        customFieldDefinitionId: definition.id,
        name: definition.displayName,
        value: customField? customField.value : ''
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
