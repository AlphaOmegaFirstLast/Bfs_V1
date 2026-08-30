
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
import { getFormControlValidation } from "@bfs/_shared/objectFields";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/master-main/master.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const BfsComponentColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false },
{ fieldName: 'bfsSystemId', displayName: 'BestFit System', sortName: 'BfsSystem_Name', width: '50px', isVisible:true },
{ fieldName: 'isSoftDelete', displayName: 'Is Soft Delete', sortName: 'IsSoftDelete', width: '50px', isVisible:false },
{ fieldName: 'name', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:true },
{ fieldName: 'displayName', displayName: 'DisplayName', sortName: 'DisplayName', width: '50px', isVisible:false },
{ fieldName: 'dataTypeId', displayName: 'Data Type', sortName: 'DataType_Name', width: '50px', isVisible:true },
{ fieldName: 'menuName', displayName: 'MenuName', sortName: 'MenuName', width: '50px', isVisible:false },
{ fieldName: 'menuPlaceHolder', displayName: 'MenuPlaceHolder', sortName: 'MenuPlaceHolder', width: '50px', isVisible:false },
{ fieldName: 'queryBaseTable', displayName: 'QueryBaseTable', sortName: 'QueryBaseTable', width: '50px', isVisible:true },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:false },
{ fieldName: 'interfaceRequired', displayName: 'Interface to Implement', sortName: 'InterfaceRequired', width: '50px', isVisible:false },

];
//---------------------------------------------------------
export interface IBfsComponent {
    isDeleted?: boolean;
id?: string;
isSoftDelete?: boolean;
name?: string;
displayName?: string;
menuName?: string;
menuPlaceHolder?: string;
queryBaseTable?: string;
notes?: string;
interfaceRequired?: string;

    bfsSystemId?: string;
dataTypeId?: number;

}
//---------------------------------------------------------
export function initBfsComponent(): IBfsComponent {
    let entity: IBfsComponent = {
        isDeleted: false,
id: '0',
isSoftDelete: false,
name: '',
displayName: '',
menuName: '',
menuPlaceHolder: '',
queryBaseTable: '',
notes: '',
interfaceRequired: '',

        bfsSystemId: '0',
dataTypeId: 0,

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function bfsComponentUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
id: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
isSoftDelete: [false,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
name: ['',getFormControlValidation('{"IsRequired":true,"MinLength":"3","MaxLength":"50","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
displayName: ['',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
menuName: ['',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
menuPlaceHolder: ['',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
queryBaseTable: ['',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
notes: ['',getFormControlValidation('{"IsRequired":false,"MinLength":"","MaxLength":"500","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
interfaceRequired: ['',getFormControlValidation('{"IsRequired":false,"MinLength":"0","MaxLength":"100","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

    bfsSystemId: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
dataTypeId: [0,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

    };
} 
//---------------------------------------------------------
export interface IBfsComponentWithLookup extends IBfsComponent{

    bfsSystemName?: string;
dataTypeName?: string;

}
//---------------------------------------------------------
export interface IBfsComponentRequest extends IEntityRequest<IBfsComponentFilter> {}

//---------------------------------------------------------
export interface IBfsComponentFilter {
    [key: string]: any;
    Id?: string;

    Name?: string;
InterfaceRequired?: string;

    BfsSystemId?: string;
DataTypeId?: number;

}
//---------------------------------------------------------
export function initBfsComponentRequest(): IBfsComponentRequest {
    let request: IBfsComponentRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: BfsComponentColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {
            Id: undefined ,

            Name: undefined ,
InterfaceRequired: undefined ,

            BfsSystemId: undefined ,
DataTypeId: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

export function getBfsComponentActions(component: any, record: IEntity): IAction[] {
        let links: IAction[] = [];

if (component.accessService.isActionAllowed('bfsComponent', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/mstr/bfs-component/add', displayText: 'Add New record'
});
}
if (component.accessService.isActionAllowed('bfsComponent', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/mstr/bfs-component/view', displayText: 'View...'
});
}
if (component.accessService.isActionAllowed('bfsComponent', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/mstr/bfs-component/edit', displayText: 'Edit...' 
});
}
if (component.accessService.isActionAllowed('bfsComponent', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/mstr/bfs-component/delete', displayText: 'Delete...' 
});
}
if (component.accessService.isActionAllowed('bfsComponent', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['bfsSystemId'], route:'/mstr/bfs-system/view', displayText:'Go to BfsSystem'
});
}
if (component.accessService.isActionAllowed('bfsComponent', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['dataTypeId'], route:'/mstr/data-type/view', displayText:'Go to DataType'
});
}

if (component.accessService.isActionAllowed('bfsComponent', ''))
{links.push({
actionSource:'System', actionType:'FrontendFunction', actionLocation:'ListRow',recordId: record['id'], action: operations.duplicateRecord, displayText: 'Duplicate Record', data: {recordId: record['id'], postUrl:'/BfsComponent', onSuccessMethodName: 'getReport' }
});
}
if (component.accessService.isActionAllowed('bfsComponent', ''))
{links.push({
actionSource:'System', actionType:'FrontendFunction', actionLocation:'ListRow',recordId: record['id'], action: operations.duplicateTree, displayText: 'Duplicate Tree', data: { recordId: record['id'], postUrl: '/Operations/BfsComponent/DuplicateTree', onSuccessMethodName: 'getReport' }
});
}
if (component.accessService.isActionAllowed('bfsComponent', ''))
{links.push({
actionSource:'System', actionType:'FrontendFunction', actionLocation:'ListRow',recordId: record['id'], action: operations.deleteTree, displayText: 'Delete Tree', data: { recordId: record['id'], deleteUrl: '/Operations/BfsComponent/DeleteTree', onSuccessMethodName: 'getReport' }
});
}
if (component.accessService.isActionAllowed('bfsComponent', ''))
{links.push({
actionSource:'System', actionType:'FrontendFunction', actionLocation:'FormHeader',recordId: record['id'], action: operations.generateTestData, displayText: 'Generate Test Data', data: { recordId: record['id'], getUrl: '/Operations/bfsComponent/TestData' }
});
}
if (component.accessService.isActionAllowed('bfsComponent', ''))
{links.push({
actionSource:'System', actionType:'FrontendFunction', actionLocation:'ListRow',recordId: record['id'], action: operations.setComponentDefaultActions, displayText: 'Set Component Default Actions', data: {}
});
}
if (component.accessService.isActionAllowed('bfsComponent', ''))
{links.push({
actionSource:'System', actionType:'FrontendFunction', actionLocation:'FormHeader',recordId: record['id'], action: operations.setComponentDefaultActions, displayText: 'Set Component Default Actions', data: {}
});
}

        return links;
    }
    //---------------------------------------------------------

