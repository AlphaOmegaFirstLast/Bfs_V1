
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/stores-main/stores.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const OperationColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false },
{ fieldName: 'name', displayName: 'Name', sortName: 'NameName', width: '50px', isVisible:true },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'NotesName', width: '50px', isVisible:false },
{ fieldName: 'effectTypeId', displayName: 'Effect Type', sortName: 'EffectTypeName', width: '50px', isVisible:true },
{ fieldName: 'thirdPartyTypeId', displayName: 'Third Party Type', sortName: 'ThirdPartyTypeName', width: '50px', isVisible:true },

];
//---------------------------------------------------------
export interface IOperation {
    isDeleted?: boolean;
id?: string;
name?: string;
notes?: string;

    effectTypeId?: number;
thirdPartyTypeId?: number;

}
//---------------------------------------------------------
export function initOperation(): IOperation {
    let entity: IOperation = {
        isDeleted: false,
id: '0',
name: '',
notes: '',

        effectTypeId: 0,
thirdPartyTypeId: 0,

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function operationUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],
name: [''],
notes: [''],

    effectTypeId: [0],
thirdPartyTypeId: [0],

    };
} 
//---------------------------------------------------------
export interface IOperationWithLookup extends IOperation{

    effectTypeName?: string;
thirdPartyTypeName?: string;

}
//---------------------------------------------------------
export interface IOperationRequest extends IEntityRequest<IOperationFilter> {}

//---------------------------------------------------------
export interface IOperationFilter {
    [key: string]: any;

    Name?: string;

    EffectTypeId?: number;
ThirdPartyTypeId?: number;

}
//---------------------------------------------------------
export function initOperationRequest(): IOperationRequest {
    let request: IOperationRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: OperationColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {

            Name: undefined ,

            EffectTypeId: undefined ,
ThirdPartyTypeId: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

export function getOperationActions(component: any, record: IEntity): IAction[] {
        let links: IAction[] = [];

if (component.accessService.isActionAllowed('operation', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/str/operation/add', displayText: 'Add New record'
});
}
if (component.accessService.isActionAllowed('operation', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/str/operation/view', displayText: 'View...'
});
}
if (component.accessService.isActionAllowed('operation', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/str/operation/edit', displayText: 'Edit...' 
});
}
if (component.accessService.isActionAllowed('operation', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/str/operation/delete', displayText: 'Delete...' 
});
}
if (component.accessService.isActionAllowed('operation', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['effectTypeId'], route:'/str/effect-type/view', displayText:'Go to EffectType'
});
}
if (component.accessService.isActionAllowed('operation', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['thirdPartyTypeId'], route:'/str/third-party-type/view', displayText:'Go to ThirdPartyType'
});
}

        return links;
    }
    //---------------------------------------------------------

