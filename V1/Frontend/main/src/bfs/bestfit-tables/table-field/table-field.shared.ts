import { IEntityRequest } from "@bfs/_shared/interfaces";
import { IFieldValidation, initFieldValidation, fieldValidationUntypedFormGroup } from "@bfs/_shared/objectFields";
import { IReportInfo, initReportInfo, reportInfoUntypedFormGroup } from "@bfs/_shared/objectFields";
import { IMatrixInfo, initMatrixInfo, matrixInfoUntypedFormGroup } from "@bfs/_shared/objectFields";
import { IToolTipInfo, initToolTipInfo, toolTipInfoUntypedFormGroup } from "@bfs/_shared/objectFields";
import { IFormInfo, initFormInfo, formInfoUntypedFormGroup } from "@bfs/_shared/objectFields";

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const TableFieldColumns = [
    { fieldName: 'isDeleted', displayName: 'IsDeleted', sortName:'IsDeleted', width: '50px', isVisible:false },
{ fieldName: 'id', displayName: 'ID', sortName:'Id', width: '50px', isVisible:false },
{ fieldName: 'field', displayName: 'Field', sortName:'Field', width: '50px', isVisible:true },
{ fieldName: 'displayName', displayName: 'DisplayName', sortName:'DisplayName', width: '50px', isVisible:false },
{ fieldName: 'isQueryColumn', displayName: 'IsQueryColumn', sortName:'IsQueryColumn', width: '50px', isVisible:true },
{ fieldName: 'isJoinField', displayName: 'IsJoinField', sortName:'IsJoinField', width: '50px', isVisible:false },
{ fieldName: 'parentTable', displayName: 'ParentTable', sortName:'ParentTable', width: '50px', isVisible:false },
{ fieldName: 'uiFormControlOrder', displayName: 'UIFormControlOrder', sortName:'UiFormControlOrder', width: '50px', isVisible:false },

    { fieldName: 'componentId', displayName: 'Component', sortName:'Component', width: '50px', isVisible:false },
{ fieldName: 'filterTypeId', displayName: 'Filter Type', sortName:'FilterType', width: '50px', isVisible:false },
{ fieldName: 'backendDataTypeId', displayName: 'Backend Type', sortName:'BackendDataType', width: '50px', isVisible:true },
{ fieldName: 'formControlTypeId', displayName: 'Form Control Type', sortName:'FormControlType', width: '50px', isVisible:false },

    { fieldName: 'fieldValidation', displayName: 'Field Validation', sortName:'FieldValidation', width: '50px', isVisible:false },
{ fieldName: 'reportInfo', displayName: 'Report Info', sortName:'ReportInfo', width: '50px', isVisible:false },
{ fieldName: 'matrixInfo', displayName: 'Matrix Info', sortName:'MatrixInfo', width: '50px', isVisible:false },
{ fieldName: 'toolTipInfo', displayName: 'ToolTip Info', sortName:'ToolTipInfo', width: '50px', isVisible:false },
{ fieldName: 'formInfo', displayName: 'Form Info', sortName:'FormInfo', width: '50px', isVisible:false },

];
//---------------------------------------------------------
// Fields of an Entity [used in Entity form]
export function tableFieldUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],
field: [''],
displayName: [''],
isQueryColumn: [false],
isJoinField: [false],
parentTable: [''],
uiFormControlOrder: [0],

    componentId: ['0'],
filterTypeId: [0],
backendDataTypeId: [0],
formControlTypeId: [0],

    fieldValidation: fieldValidationUntypedFormGroup(formBuilder),
reportInfo: reportInfoUntypedFormGroup(formBuilder),
matrixInfo: matrixInfoUntypedFormGroup(formBuilder),
toolTipInfo: toolTipInfoUntypedFormGroup(formBuilder),
formInfo: formInfoUntypedFormGroup(formBuilder),

    };
} 
//---------------------------------------------------------

export interface ITableField {
    isDeleted?: boolean;
id?: string;
field?: string;
displayName?: string;
isQueryColumn?: boolean;
isJoinField?: boolean;
parentTable?: string;
uiFormControlOrder?: number;

    componentId?: string;
filterTypeId?: number;
backendDataTypeId?: number;
formControlTypeId?: number;

    fieldValidation?: IFieldValidation;
reportInfo?: IReportInfo;
matrixInfo?: IMatrixInfo;
toolTipInfo?: IToolTipInfo;
formInfo?: IFormInfo;

}
//---------------------------------------------------------
export interface ITableFieldWithLookup extends ITableField{

    component?: string;
filterType?: string;
backendDataType?: string;
formControlType?: string;

}
//---------------------------------------------------------

export function initTableField(): ITableField {
    let entity: ITableField = {
        isDeleted: false,
id: '0',
field: '',
displayName: '',
isQueryColumn: false,
isJoinField: false,
parentTable: '',
uiFormControlOrder: 0,

        componentId: '0',
filterTypeId: 0,
backendDataTypeId: 0,
formControlTypeId: 0,

        fieldValidation: initFieldValidation(),
reportInfo: initReportInfo(),
matrixInfo: initMatrixInfo(),
toolTipInfo: initToolTipInfo(),
formInfo: initFormInfo(),

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------
export interface ITableFieldRequest extends IEntityRequest<ITableFieldFilter> {}

//---------------------------------------------------------
export interface ITableFieldFilter {
    [key: string]: any;

    Field?: string;

    ComponentId?: string;
FilterTypeId?: number;
BackendDataTypeId?: number;
FormControlTypeId?: number;

}
//---------------------------------------------------------
export function initTableFieldRequest(): ITableFieldRequest {
    let request: ITableFieldRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: TableFieldColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {

            Field: undefined ,

            ComponentId: undefined ,
FilterTypeId: undefined ,
BackendDataTypeId: undefined ,
FormControlTypeId: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

