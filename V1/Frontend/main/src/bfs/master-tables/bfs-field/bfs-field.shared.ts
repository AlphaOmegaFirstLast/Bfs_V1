
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
import { getFormControlValidation } from "@bfs/_shared/objectFields";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/master-main/master.operations';

import { IFieldValidation, initFieldValidation, fieldValidationUntypedFormGroup } from "@bfs/_shared/objectFields";
import { IReportInfo, initReportInfo, reportInfoUntypedFormGroup } from "@bfs/_shared/objectFields";
import { IMatrixInfo, initMatrixInfo, matrixInfoUntypedFormGroup } from "@bfs/_shared/objectFields";
import { IToolTipInfo, initToolTipInfo, toolTipInfoUntypedFormGroup } from "@bfs/_shared/objectFields";
import { IFormInfo, initFormInfo, formInfoUntypedFormGroup } from "@bfs/_shared/objectFields";

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const BfsFieldColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false },
{ fieldName: 'bfsComponentId', displayName: 'Component', sortName: 'BfsComponent_Name', width: '50px', isVisible:true },
{ fieldName: 'field', displayName: 'Field', sortName: 'Field', width: '50px', isVisible:true },
{ fieldName: 'displayName', displayName: 'DisplayName', sortName: 'DisplayName', width: '50px', isVisible:true },
{ fieldName: 'filterTypeId', displayName: 'Filter Type', sortName: 'FilterType_Name', width: '50px', isVisible:true },
{ fieldName: 'backendDataTypeId', displayName: 'Backend Type', sortName: 'BackendDataType_Name', width: '50px', isVisible:true },

{ fieldName: 'fieldValidation', displayName: 'FieldValidation', sortName: 'jsonFieldValidation', width: '50px', isVisible:false },
{ fieldName: 'jsonFieldValidation', displayName: 'Json FieldValidation', sortName: 'jsonFieldValidation', width: '50px', isVisible:false },
{ fieldName: 'reportInfo', displayName: 'ReportInfo', sortName: 'jsonReportInfo', width: '50px', isVisible:false },
{ fieldName: 'jsonReportInfo', displayName: 'Json ReportInfo', sortName: 'jsonReportInfo', width: '50px', isVisible:false },
{ fieldName: 'matrixInfo', displayName: 'MatrixInfo', sortName: 'jsonMatrixInfo', width: '50px', isVisible:false },
{ fieldName: 'jsonMatrixInfo', displayName: 'Json MatrixInfo', sortName: 'jsonMatrixInfo', width: '50px', isVisible:false },
{ fieldName: 'toolTipInfo', displayName: 'ToolTipInfo', sortName: 'jsonToolTipInfo', width: '50px', isVisible:false },
{ fieldName: 'jsonToolTipInfo', displayName: 'Json ToolTipInfo', sortName: 'jsonToolTipInfo', width: '50px', isVisible:false },
{ fieldName: 'formInfo', displayName: 'FormInfo', sortName: 'jsonFormInfo', width: '50px', isVisible:false },
{ fieldName: 'jsonFormInfo', displayName: 'Json FormInfo', sortName: 'jsonFormInfo', width: '50px', isVisible:false },

];
//---------------------------------------------------------
export interface IBfsField {
    isDeleted?: boolean;
id?: string;
field?: string;
displayName?: string;

    filterTypeId?: number;
backendDataTypeId?: number;

    bfsComponentId?: string;
    bfsComponentName?: string;

    fieldValidation?: IFieldValidation;
    jsonFieldValidation?: string;
reportInfo?: IReportInfo;
    jsonReportInfo?: string;
matrixInfo?: IMatrixInfo;
    jsonMatrixInfo?: string;
toolTipInfo?: IToolTipInfo;
    jsonToolTipInfo?: string;
formInfo?: IFormInfo;
    jsonFormInfo?: string;

}
//---------------------------------------------------------
export function initBfsField(): IBfsField {
    let entity: IBfsField = {
        isDeleted: false,
id: '0',
field: '',
displayName: '',

        filterTypeId: 0,
backendDataTypeId: 0,

        bfsComponentId: '0',
        bfsComponentName: '',

        fieldValidation: initFieldValidation(),
reportInfo: initReportInfo(),
matrixInfo: initMatrixInfo(),
toolTipInfo: initToolTipInfo(),
formInfo: initFormInfo(),

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function bfsFieldUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
id: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
field: ['',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
displayName: ['',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

    filterTypeId: [0,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
backendDataTypeId: [0,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

    bfsComponentId: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
    bfsComponentName: [''],

    fieldValidation: fieldValidationUntypedFormGroup(formBuilder),
reportInfo: reportInfoUntypedFormGroup(formBuilder),
matrixInfo: matrixInfoUntypedFormGroup(formBuilder),
toolTipInfo: toolTipInfoUntypedFormGroup(formBuilder),
formInfo: formInfoUntypedFormGroup(formBuilder),

    };
} 
//---------------------------------------------------------
export interface IBfsFieldWithLookup extends IBfsField{

    filterTypeName?: string;
backendDataTypeName?: string;

    bfsComponentName?: string;

}
//---------------------------------------------------------
export interface IBfsFieldRequest extends IEntityRequest<IBfsFieldFilter> {}

//---------------------------------------------------------
export interface IBfsFieldFilter {
    [key: string]: any;
    Id?: string;

    Field?: string;

    FilterTypeId?: number;
BackendDataTypeId?: number;

    BfsComponentId?: string;
    BfsComponentName?: string;

}
//---------------------------------------------------------
export function initBfsFieldRequest(): IBfsFieldRequest {
    let request: IBfsFieldRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: BfsFieldColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {
            Id: undefined ,

            Field: undefined ,

            FilterTypeId: undefined ,
BackendDataTypeId: undefined ,

            BfsComponentId: undefined ,
            BfsComponentName: undefined ,
            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

export function getBfsFieldActions(component: any, record: IEntity): IAction[] {
        let links: IAction[] = [];

if (component.accessService.isActionAllowed('bfsField', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/mstr/bfs-field/add', displayText: 'Add New record'
});
}
if (component.accessService.isActionAllowed('bfsField', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/mstr/bfs-field/view', displayText: 'View...'
});
}
if (component.accessService.isActionAllowed('bfsField', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/mstr/bfs-field/edit', displayText: 'Edit...' 
});
}
if (component.accessService.isActionAllowed('bfsField', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/mstr/bfs-field/delete', displayText: 'Delete...' 
});
}

if (component.accessService.isActionAllowed('bfsField', ''))
{links.push({
actionSource:'System', actionType:'FrontendFunction', actionLocation:'ListRow',recordId: record['id'], action: operations.duplicateRecord, displayText: 'Duplicate Record', data: {recordId: record['id'], postUrl:'/BfsField', onSuccessMethodName: 'getReport' }
});
}

        return links;
    }
    //---------------------------------------------------------

