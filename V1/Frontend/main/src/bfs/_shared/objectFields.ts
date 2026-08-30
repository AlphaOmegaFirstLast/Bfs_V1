import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, ValidationErrors, FormBuilder } from "@angular/forms";
import { DomSanitizer } from "@angular/platform-browser";
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
export function getFieldValidationHeaders(this: any): string {
    var result = `<table class="table table-bordered table-sm">
    
                <tr><th colspan="7" style="border: 1px solid lightblue;" class="text-center">Field Validation</th></tr>
                <tr>
                    <th width="150px">Is Required</th>
                    <th width="150px">Min Length</th>
                    <th width="150px">Max Length</th>
                    <th width="150px">Min Value</th>
                    <th width="150px">Max Value</th>
                    <th width="150px">Regex Pattern</th>
                    <th width="150px">Allowed Values</th>
                </tr>
                </table>`;
    return result;
}
//-------------------------------------------------
export function getFieldValidationData(fieldValidation: IFieldValidation): string {
    if (!fieldValidation) return '';
    try {
      //  const fieldValidation: IFieldValidation = normalizeObjectKeysToLowerFirstLetter(JSON.parse(fieldValidationString) as IFieldValidation);
        var result = `<table class="table table-bordered table-sm">
                   <tr>    
                        <td width="150px"> ${fieldValidation.isRequired ? 'true' : 'false'}</td>
                        <td width="150px"> ${fieldValidation.minLength}</td>
                        <td width="150px"> ${fieldValidation.maxLength}</td>
                        <td width="150px"> ${fieldValidation.minValue}</td>
                        <td width="150px"> ${fieldValidation.maxValue}</td>
                        <td width="150px"> ${fieldValidation.regexPattern}</td>
                        <td width="150px"> ${fieldValidation.allowedValues}</td>
                    </tr>
                </table>`;
        return (result);
    } catch (e) {
        return '';
    }
}
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
export function getFormControlValidation(sFieldValidation?: string) {
    let validatorsArray = [];
    // a list of fieldValidation is returened through Dapper, as list of strings not objects
    let fieldValidation = sFieldValidation ? JSON.parse(sFieldValidation) : null;
    if (fieldValidation) {
        if (fieldValidation.IsRequired) validatorsArray.push(Validators.required);
        if (fieldValidation.MinLength > 0) validatorsArray.push(Validators.minLength(fieldValidation.MinLength));
        if (fieldValidation.MaxLength > 0) validatorsArray.push(Validators.maxLength(Number(fieldValidation.MaxLength)));
        if (fieldValidation.MinValue) validatorsArray.push(Validators.min(+(fieldValidation.MinValue)));
        if (fieldValidation.MaxValue) validatorsArray.push(Validators.max(parseInt(fieldValidation.MaxValue)));
        if (fieldValidation.RegexPattern) validatorsArray.push(Validators.pattern(fieldValidation.RegexPattern));
        if (fieldValidation.AllowedValues) validatorsArray.push(allowedValuesValidator(fieldValidation.AllowedValues.split(';')));
    }
    return validatorsArray;
}
//---------------------------------------------------------
// Interface used in List and reports
export interface IReportInfo {
    parentTable: string;
    isQueryColumn: boolean;
    isColumnVisible: boolean;
    isJoinField: boolean;
    aggregateTypeId: string,
    chartElementId: string,
    columnOrder: string
}
//---------------------------------------------------------
function normalizeObjectKeysToLowerFirstLetter<T extends object>(value: T): T {
    if (!value || typeof value !== 'object' || Array.isArray(value)) {
        return value;
    }

    const result = {} as Record<string, unknown>;
    Object.entries(value as Record<string, unknown>).forEach(([key, val]) => {
        const normalizedKey = key.charAt(0).toLowerCase() + key.slice(1);
        result[normalizedKey] = val;
    });

    return result as T;
}
//---------------------------------------------------------
// Fields of an Entity [used in Entity form]
export function initReportInfo(): IReportInfo {
    return {
        parentTable: '',
        isQueryColumn: true,
        isColumnVisible: true,
        isJoinField: false,
        aggregateTypeId: '1',
        chartElementId: '1',
        columnOrder:'1'
    }
}
//-------------------------------------------------
export function reportInfoUntypedFormGroup(formBuilder: FormBuilder): UntypedFormGroup {
    return formBuilder.group({
        parentTable: [''],
        isQueryColumn: [true],
        isColumnVisible: [true],
        isJoinField: [false],
        aggregateTypeId: ['1'],
        chartElementId: ['1'],
        columnOrder: ['1']
    })
};
//------------------------------------------------
export function getReportInfoHeaders(this: any): string {
    var result = `<table class="table table-bordered table-sm">
    
                <tr><th colspan="6" style="border: 1px solid lightblue;" class="text-center">Report Info</th></tr>
                <tr>
                    <th width="150px">Parent Table</th>
                    <th width="150px">Is Query Column</th>
                    <th width="150px">Is Column Visible</th>
                    <th width="150px">Is Join Field</th>
                    <th width="150px">Aggregate Type Id</th>
                    <th width="150px">Chart Element Id</th>
                </tr>
                </table>`;
    return result;
}
//-------------------------------------------------
export function getReportInfoData(reportInfo: IReportInfo): string {
    if (!reportInfo) return '';
    try {
 //       const reportInfo: IReportInfo = normalizeObjectKeysToLowerFirstLetter(JSON.parse(reportInfoString) as IReportInfo);
        var result = `<table class="table table-bordered table-sm">
                   <tr>    
                        <td width="150px"> ${reportInfo.parentTable??''}</td>
                        <td width="150px"> ${reportInfo.isQueryColumn ? 'true' : 'false'}</td>
                        <td width="150px"> ${reportInfo.isColumnVisible ? 'true' : 'false'}</td>
                        <td width="150px"> ${reportInfo.isJoinField ? 'true' : 'false'}</td>
                        <td width="150px"> ${reportInfo.aggregateTypeId??''}</td>
                        <td width="150px"> ${reportInfo.chartElementId??''}</td>
                    </tr>
                </table>`;
        return (result);
    } catch (e) {
        return '';
    }
}
//------------------------------------------------

export async function getReportInfoLookups(me: any): Promise<void> {
    me.messages = [];
    me.isLoading.lookups = true;
    let target = '';
    target = "/ChartElement/list";
    (await me.apiService.post(target, { pageSize: 30 })).subscribe({
        next: (response: IQueryResponse) => {
            me.ChartElementOptions = response.items;
            me.isLoading.lookups = false;
        },
        error: (err: any) => {
            me.isLoading.lookups = false;
            var msg = err.message || 'An error occurred while fetching Chart Elements data.';
            me.messages.push({ text: msg, msgType: "danger" });
        }
    });
    target = "/AggregateType/list";
    (await me.apiService.post(target, { pageSize: 30 })).subscribe({
        next: (response: IQueryResponse) => {
            me.AggregateTypeOptions = response.items;
            me.isLoading.lookups = false;
        },
        error: (err: any) => {
            me.isLoading.lookups = false;
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
    me.isLoading.lookups = true;
    let target = '';
    target = "/ActionLocation/list";
    (await me.apiService.post(target, { pageSize: 30 })).subscribe({
        next: (response: IQueryResponse) => {
            me.ActionLocationOptions = response.items;
            me.isLoading.lookups = false;
        },
        error: (err: any) => {
            me.isLoading.lookups = false;
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
    me.isLoading.lookups = true;
    let target = '';
    target = "/FormControlType/list";
    (await me.apiService.post(target, { pageSize: 30 })).subscribe({
        next: (response: IQueryResponse) => {
            me.FormControlTypeOptions = response.items;
            me.isLoading.lookups = false;
        },
        error: (err: any) => {
            me.isLoading.lookups = false;
            var msg = err.message || 'An error occurred while fetching Form Control Type data.';
            me.messages.push({ text: msg, msgType: "danger" });
        }
    });
}
//---------------------------------------------------------
