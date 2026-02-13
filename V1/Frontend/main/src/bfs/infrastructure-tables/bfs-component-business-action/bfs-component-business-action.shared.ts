
import { IEntityRequest, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import { deleteTree, duplicateRecord, duplicateTree } from '@bfs/infrastructure-main/infrastructure.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const BfsComponentBusinessActionColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:true },
{ fieldName: 'bfsComponentId', displayName: 'Component Name', sortName: 'BfsComponent', width: '50px', isVisible:true },
{ fieldName: 'businessActionId', displayName: 'Business Action', sortName: 'BusinessAction', width: '50px', isVisible:true },
{ fieldName: 'actionLocationId', displayName: 'Menu Action', sortName: 'ActionLocation', width: '50px', isVisible:true },

];
//---------------------------------------------------------
export interface IBfsComponentBusinessAction {
    isDeleted?: boolean;
id?: string;

    bfsComponentId?: string;
businessActionId?: string;
actionLocationId?: number;

}
//---------------------------------------------------------
export function initBfsComponentBusinessAction(): IBfsComponentBusinessAction {
    let entity: IBfsComponentBusinessAction = {
        isDeleted: false,
id: '0',

        bfsComponentId: '0',
businessActionId: '0',
actionLocationId: 0,

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function bfsComponentBusinessActionUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],

    bfsComponentId: ['0'],
businessActionId: ['0'],
actionLocationId: [0],

    };
} 
//---------------------------------------------------------
export interface IBfsComponentBusinessActionWithLookup extends IBfsComponentBusinessAction{

    bfsComponentName?: string;
businessActionName?: string;
actionLocationName?: string;

}
//---------------------------------------------------------
export interface IBfsComponentBusinessActionRequest extends IEntityRequest<IBfsComponentBusinessActionFilter> {}

//---------------------------------------------------------
export interface IBfsComponentBusinessActionFilter {
    [key: string]: any;

    BfsComponentId?: string;
BusinessActionId?: string;
ActionLocationId?: number;

}
//---------------------------------------------------------
export function initBfsComponentBusinessActionRequest(): IBfsComponentBusinessActionRequest {
    let request: IBfsComponentBusinessActionRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: BfsComponentBusinessActionColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {

            BfsComponentId: undefined ,
BusinessActionId: undefined ,
ActionLocationId: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

export function getBfsComponentBusinessActionActions(record: IQueryColumn): IAction[] {
        let links: IAction[] = [];

links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/bfs/bfs-component-business-action/add', displayText: 'Add New record' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/bfs/bfs-component-business-action/view', displayText: 'View...' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/bfs/bfs-component-business-action/edit', displayText: 'Edit...' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/bfs/bfs-component-business-action/delete', displayText: 'Delete...' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['bfsComponentId'], route:'/bfs/bfs-component/view', displayText:'Go to BfsComponent' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['businessActionId'], route:'/bfs/business-action/view', displayText:'Go to BusinessAction' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['actionLocationId'], route:'/bfs/action-location/view', displayText:'Go to ActionLocation' 
});

links.push({
actionSource:'0', actionType:'FrontendFunction', actionLocation:'ListRow',recordId: record['id'], action: duplicateRecord, displayText: 'Duplicate Record', data: {recordId: record['id'], postUrl: '/BfsComponentBusinessAction', onSuccessMethodName: 'getReport' } 
});
links.push({
actionSource:'0', actionType:'FrontendFunction', actionLocation:'ListRow',recordId: record['id'], action: duplicateTree, displayText: 'Duplicate Tree', data: { recordId: record['id'], postUrl: '/Operations/BfsComponentBusinessAction/DuplicateTree', onSuccessMethodName: 'getReport' } 
});

        return links;
    }
    //---------------------------------------------------------

