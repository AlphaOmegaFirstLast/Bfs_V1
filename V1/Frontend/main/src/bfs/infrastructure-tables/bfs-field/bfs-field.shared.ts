
import { IEntityRequest, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import { deleteTree, duplicateRecord, duplicateTree } from '@bfs/infrastructure-main/infrastructure.operations';

import { IFieldValidation, initFieldValidation, fieldValidationUntypedFormGroup } from "@bfs/_shared/objectFields";
import { IReportInfo, initReportInfo, reportInfoUntypedFormGroup } from "@bfs/_shared/objectFields";
import { IMatrixInfo, initMatrixInfo, matrixInfoUntypedFormGroup } from "@bfs/_shared/objectFields";
import { IToolTipInfo, initToolTipInfo, toolTipInfoUntypedFormGroup } from "@bfs/_shared/objectFields";
import { IFormInfo, initFormInfo, formInfoUntypedFormGroup } from "@bfs/_shared/objectFields";

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const BfsFieldColumns = [
    { fieldName: 'bfsFieldFieldValidation', displayName: 'Field Validation', sortName: 'FieldValidation', width: '50px', isVisible:true },
{ fieldName: 'bfsFieldId', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:true },
{ fieldName: 'bfsFieldBfsComponentId', displayName: 'Component', sortName: 'BfsComponent', width: '50px', isVisible:true },
{ fieldName: 'bfsFieldField', displayName: 'Field', sortName: 'Field', width: '50px', isVisible:true },
{ fieldName: 'bfsFieldDisplayName', displayName: 'DisplayName', sortName: 'DisplayName', width: '50px', isVisible:true },
{ fieldName: 'bfsFieldIsQueryColumn', displayName: 'IsQueryColumn', sortName: 'IsQueryColumn', width: '50px', isVisible:true },
{ fieldName: 'bfsFieldIsJoinField', displayName: 'IsJoinField', sortName: 'IsJoinField', width: '50px', isVisible:true },
{ fieldName: 'bfsFieldParentTable', displayName: 'ParentTable', sortName: 'ParentTable', width: '50px', isVisible:true },
{ fieldName: 'bfsFieldFilterTypeId', displayName: 'Filter Type', sortName: 'FilterType', width: '50px', isVisible:true },
{ fieldName: 'bfsFieldBackendDataTypeId', displayName: 'Backend Type', sortName: 'BackendDataType', width: '50px', isVisible:true },
{ fieldName: 'bfsFieldReportInfo', displayName: 'Report Info', sortName: 'ReportInfo', width: '50px', isVisible:true },
{ fieldName: 'bfsFieldMatrixInfo', displayName: 'Matrix Info', sortName: 'MatrixInfo', width: '50px', isVisible:true },
{ fieldName: 'bfsFieldToolTipInfo', displayName: 'ToolTip Info', sortName: 'ToolTipInfo', width: '50px', isVisible:true },
{ fieldName: 'bfsFieldFormInfo', displayName: 'Form Info', sortName: 'FormInfo', width: '50px', isVisible:true },

];
//---------------------------------------------------------
export interface IBfsField {
    isDeleted?: boolean;
id?: string;
field?: string;
displayName?: string;
isQueryColumn?: boolean;
isJoinField?: boolean;
parentTable?: string;

    bfsComponentId?: string;
filterTypeId?: number;
backendDataTypeId?: number;

    fieldValidation?: IFieldValidation;
reportInfo?: IReportInfo;
matrixInfo?: IMatrixInfo;
toolTipInfo?: IToolTipInfo;
formInfo?: IFormInfo;

}
//---------------------------------------------------------
export function initBfsField(): IBfsField {
    let entity: IBfsField = {
        isDeleted: false,
id: '0',
field: '',
displayName: '',
isQueryColumn: false,
isJoinField: false,
parentTable: '',

        bfsComponentId: '0',
filterTypeId: 0,
backendDataTypeId: 0,

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
    isDeleted: [false],
id: ['0'],
field: [''],
displayName: [''],
isQueryColumn: [false],
isJoinField: [false],
parentTable: [''],

    bfsComponentId: ['0'],
filterTypeId: [0],
backendDataTypeId: [0],

    fieldValidation: fieldValidationUntypedFormGroup(formBuilder),
reportInfo: reportInfoUntypedFormGroup(formBuilder),
matrixInfo: matrixInfoUntypedFormGroup(formBuilder),
toolTipInfo: toolTipInfoUntypedFormGroup(formBuilder),
formInfo: formInfoUntypedFormGroup(formBuilder),

    };
} 
//---------------------------------------------------------
export interface IBfsFieldWithLookup extends IBfsField{

    bfsComponentName?: string;
filterTypeName?: string;
backendDataTypeName?: string;

}
//---------------------------------------------------------
export interface IBfsFieldRequest extends IEntityRequest<IBfsFieldFilter> {}

//---------------------------------------------------------
export interface IBfsFieldFilter {
    [key: string]: any;

    Field?: string;

    BfsComponentId?: string;
FilterTypeId?: number;
BackendDataTypeId?: number;

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

            Field: undefined ,

            BfsComponentId: undefined ,
FilterTypeId: undefined ,
BackendDataTypeId: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

export function getBfsFieldActions(record: IQueryColumn): IAction[] {
        let links: IAction[] = [];

links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/bfs/bfs-field/add', displayText: 'Add New record' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['bfsFieldId'], route:'/bfs/bfs-field/view', displayText: 'View...' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['bfsFieldId'], route:'/bfs/bfs-field/edit', displayText: 'Edit...' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['bfsFieldId'], route:'/bfs/bfs-field/delete', displayText: 'Delete...' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['bfsComponentId'], route:'/bfs/bfs-component/view', displayText:'Go to BfsComponent' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['filterTypeId'], route:'/bfs/filter-type/view', displayText:'Go to FilterType' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['backendDataTypeId'], route:'/bfs/backend-data-type/view', displayText:'Go to BackendDataType' 
});

links.push({
actionSource:'0', actionType:'FrontendFunction', actionLocation:'ListRow',recordId: record['bfsFieldId'], action: duplicateRecord, displayText: 'Duplicate Record', data: {recordId: record['bfsFieldId'], postUrl: '/BfsField', onSuccessMethodName: 'getReport' } 
});
links.push({
actionSource:'0', actionType:'FrontendFunction', actionLocation:'ListRow',recordId: record['bfsFieldId'], action: duplicateTree, displayText: 'Duplicate Tree', data: { recordId: record['bfsFieldId'], postUrl: '/Operations/BfsField/DuplicateTree', onSuccessMethodName: 'getReport' } 
});

        return links;
    }
    //---------------------------------------------------------

