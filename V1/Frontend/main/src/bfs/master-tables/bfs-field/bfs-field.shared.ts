
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
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
    { fieldName: 'fieldValidation', displayName: 'Field Validation', sortName: 'FieldValidation', width: '50px', isVisible: false },
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible: false },
    { fieldName: 'bfsComponentId', displayName: 'Component', sortName: 'BfsComponentName', width: '50px', isVisible: true },
    { fieldName: 'field', displayName: 'Field', sortName: 'Field', width: '50px', isVisible: true },
    { fieldName: 'displayName', displayName: 'DisplayName', sortName: 'DisplayName', width: '50px', isVisible: true },
    { fieldName: 'filterTypeId', displayName: 'Filter Type', sortName: 'FilterTypeName', width: '50px', isVisible: true },
    { fieldName: 'backendDataTypeId', displayName: 'Backend Type', sortName: 'BackendDataTypeName', width: '50px', isVisible: true },
    { fieldName: 'reportInfo', displayName: 'Report Info', sortName: 'ReportInfo', width: '50px', isVisible: false },
    { fieldName: 'matrixInfo', displayName: 'Matrix Info', sortName: 'MatrixInfo', width: '50px', isVisible: false },
    { fieldName: 'toolTipInfo', displayName: 'ToolTip Info', sortName: 'ToolTipInfo', width: '50px', isVisible: false },
    { fieldName: 'formInfo', displayName: 'Form Info', sortName: 'FormInfo', width: '50px', isVisible: false },

];
//---------------------------------------------------------
export interface IBfsField {
    isDeleted?: boolean;
    id?: string;
    field?: string;
    displayName?: string;

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
export interface IBfsFieldWithLookup extends IBfsField {

    bfsComponentName?: string;
    filterTypeName?: string;
    backendDataTypeName?: string;

}
//---------------------------------------------------------
export interface IBfsFieldRequest extends IEntityRequest<IBfsFieldFilter> { }

//---------------------------------------------------------
export interface IBfsFieldFilter {
    [key: string]: any;
    Id?: string;

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
            Id: undefined,

            Field: undefined,

            BfsComponentId: undefined,
            FilterTypeId: undefined,
            BackendDataTypeId: undefined,

        }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

export function getBfsFieldActions(component: any, record: IEntity): IAction[] {
    let links: IAction[] = [];

    if (component.accessService.isActionAllowed('bfsField', '')) {
        links.push({
            actionSource: 'System', actionType: 'FrontendLink', actionLocation: 'ListHeader', recordId: 0, route: '/mstr/bfs-field/add', displayText: 'Add New record'
        });
    }
    if (component.accessService.isActionAllowed('bfsField', '')) {
        links.push({
            actionSource: 'System', actionType: 'FrontendLink', actionLocation: 'ListRow', recordId: record['id'], route: '/mstr/bfs-field/view', displayText: 'View...'
        });
    }
    if (component.accessService.isActionAllowed('bfsField', '')) {
        links.push({
            actionSource: 'System', actionType: 'FrontendLink', actionLocation: 'ListRow', recordId: record['id'], route: '/mstr/bfs-field/edit', displayText: 'Edit...'
        });
    }
    if (component.accessService.isActionAllowed('bfsField', '')) {
        links.push({
            actionSource: 'System', actionType: 'FrontendLink', actionLocation: 'ListRow', recordId: record['id'], route: '/mstr/bfs-field/delete', displayText: 'Delete...'
        });
    }

    if (component.accessService.isActionAllowed('bfsField', '')) {
        links.push({
            actionSource: 'System', actionType: 'FrontendFunction', actionLocation: 'ListRow', recordId: record['id'], action: operations.duplicateRecord, displayText: 'Duplicate Record', data: { recordId: record['id'], postUrl: '/BfsField', onSuccessMethodName: 'getReport' }
        });
    }

    return links;
}
//---------------------------------------------------------

