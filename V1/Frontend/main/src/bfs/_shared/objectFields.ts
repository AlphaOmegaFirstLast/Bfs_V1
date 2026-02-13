import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, ValidationErrors, FormBuilder } from "@angular/forms";
import { IQueryResponse } from "./interfaces";

export interface IFieldValidation {
    isRequired: boolean;
    minLength: number;
    maxLength: number;
    minValue: string;
    maxValue: string;
    regexPattern?: string;
    allowedValues?: string; // Semi-Colon separated values
}
//------------------------------------------------
export function initFieldValidation(): IFieldValidation {
    return {
        isRequired: false,
        minLength: 0,
        maxLength: 0,
        minValue: '0',
        maxValue: '0',
        regexPattern: '',
        allowedValues: ''  // Semi-Colon separated values
    }
}
//---------------------------------------------------------
// Fields of an Entity [used in Entity form]
export function fieldValidationUntypedFormGroup(formBuilder: FormBuilder): UntypedFormGroup {
    return formBuilder.group({
        isRequired: [false],
        minLength: [''],
        maxLength: [''],
        minValue: [''],
        maxValue: [''],
        regexPattern: [''],
        allowedValues: ['']
    })
};
//------------------------------------------------
// Custom validator: check against allowed values
export function allowedValuesValidator(allowed: string[]) {
    return (control: AbstractControl): ValidationErrors | null => {
        if (!control.value) return null; // let required handle empties
        return allowed.includes(control.value)
            ? null
            : { notAllowed: { value: control.value } };
    };
}
//------------------------------------------------
export function getFormControlValidation(fieldValidation?: IFieldValidation) {
    let validatorsArray = [];
    if (fieldValidation) {
        if (fieldValidation.isRequired) validatorsArray.push(Validators.required);
        if (fieldValidation.minLength > 0) validatorsArray.push(Validators.minLength(fieldValidation.minLength));
        if (fieldValidation.maxLength > 0) validatorsArray.push(Validators.maxLength(Number(fieldValidation.maxLength)));
        if (fieldValidation.minValue) validatorsArray.push(Validators.min(+(fieldValidation.minValue)));
        if (fieldValidation.maxValue) validatorsArray.push(Validators.max(parseInt(fieldValidation.maxValue)));
        if (fieldValidation.regexPattern) validatorsArray.push(Validators.pattern(fieldValidation.regexPattern));
        if (fieldValidation.allowedValues) validatorsArray.push(allowedValuesValidator(fieldValidation.allowedValues.split(';')));
    }
    return validatorsArray;
}
//---------------------------------------------------------

export interface IReportInfo {
    parentTable: string;
    isQueryColumn: boolean;
    isColumnVisible: boolean;
    isJoinField: boolean;
    aggregateTypeId: string,
    chartElementId: string
}
//------------------------------------------------
export function initReportInfo(): IReportInfo {
    return {
        parentTable: '',
        isQueryColumn: true,
        isColumnVisible: true,
        isJoinField: false,
        aggregateTypeId: '1',
        chartElementId: '1'
    }
}
//---------------------------------------------------------
// Fields of an Entity [used in Entity form]
export function reportInfoUntypedFormGroup(formBuilder: FormBuilder): UntypedFormGroup {
    return formBuilder.group({
        parentTable: [''],
        isQueryColumn: [true],
        isColumnVisible: [true],
        isJoinField: [false],
        aggregateTypeId: ['1'],
        chartElementId: ['1']
    })
};
//------------------------------------------------
export async function getReportInfoLookups(me: any): Promise<void> {
    me.messages = [];
    me.isLoading = true;
    let target = '';
    target = "/ChartElement/list";
    (await me.apiService.post(target, { pageSize: 30 })).subscribe({
        next: (response: IQueryResponse) => {
            me.ChartElementOptions = response.items;
            me.isLoading = false;
        },
        error: (err: any) => {
            me.isLoading = false;
            var msg = err.message || 'An error occurred while fetching Chart Elements data.';
            me.messages.push({ text: msg, msgType: "danger" });
        }
    });
    target = "/AggregateType/list";
    (await me.apiService.post(target, { pageSize: 30 })).subscribe({
        next: (response: IQueryResponse) => {
            me.AggregateTypeOptions = response.items;
            me.isLoading = false;
        },
        error: (err: any) => {
            me.isLoading = false;
            var msg = err.message || 'An error occurred while fetching Aggregate Type data.';
            me.messages.push({ text: msg, msgType: "danger" });
        }
    });
}
//---------------------------------------------------------


export interface IToolTipInfo {
        actionLocationId: string,
        note: string,
        icon: string,
}
//------------------------------------------------
export function initToolTipInfo(): IToolTipInfo {
    return {
        actionLocationId: '1',
        note: '',
        icon: '',
    }
}
//---------------------------------------------------------
// Fields of an Entity [used in Entity form]
export function toolTipInfoUntypedFormGroup(formBuilder: FormBuilder): UntypedFormGroup {
    return formBuilder.group({
        actionLocationId: ['1'],
        note: [''],
        icon: [''],
    })
};
//------------------------------------------------
export async function getToolTipInfoLookups(me: any): Promise<void> {
    me.messages = [];
    me.isLoading = true;
    let target = '';
    target = "/ActionLocation/list";
    (await me.apiService.post(target, { pageSize: 30 })).subscribe({
        next: (response: IQueryResponse) => {
            me.ActionLocationOptions = response.items;
            me.isLoading = false;
        },
        error: (err: any) => {
            me.isLoading = false;
            var msg = err.message || 'An error occurred while fetching Chart Elements data.';
            me.messages.push({ text: msg, msgType: "danger" });
        }
    });
}
//---------------------------------------------------------

export interface IMatrixInfo {
    parentApi: string,
    verticalApi: string,
    horizontalApi: string
}
//------------------------------------------------
export function initMatrixInfo(): IMatrixInfo {
    return {
        parentApi: '',
        verticalApi: '',
        horizontalApi: ''   
    }
}
//---------------------------------------------------------
// Fields of an Entity [used in Entity form]
export function matrixInfoUntypedFormGroup(formBuilder: FormBuilder): UntypedFormGroup {
    return formBuilder.group({
        parentApi: [''],
        verticalApi: [''],
        horizontalApi: ['']
    })
};
//------------------------------------------------
export async function getMatrixInfoLookups(me: any): Promise<void> {
}
//---------------------------------------------------------


export interface IFormInfo {
        formControlTypeId: string,
        column: string,
        row: string,
}
//------------------------------------------------
export function initFormInfo(): IFormInfo {
    return {
        formControlTypeId: '1',
        column: '0',
        row: '0',
    }
}
//---------------------------------------------------------
// Fields of an Entity [used in Entity form]
export function formInfoUntypedFormGroup(formBuilder: FormBuilder): UntypedFormGroup {
    return formBuilder.group({
        formControlTypeId: ['1'],
        column: ['0'],
        row: ['0'],
    })
};
//------------------------------------------------
export async function getFormInfoLookups(me: any): Promise<void> {
    me.messages = [];
    me.isLoading = true;
    let target = '';
    target = "/FormControlType/list";
    (await me.apiService.post(target, { pageSize: 30 })).subscribe({
        next: (response: IQueryResponse) => {
            me.FormControlTypeOptions = response.items;
            me.isLoading = false;
        },
        error: (err: any) => {
            me.isLoading = false;
            var msg = err.message || 'An error occurred while fetching Form Control Type data.';
            me.messages.push({ text: msg, msgType: "danger" });
        }
    });
}
//---------------------------------------------------------
