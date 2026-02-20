
import { IEntityRequest, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import { deleteTree, duplicateRecord, duplicateTree } from '@bfs/infrastructure-main/infrastructure.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const SystemActionColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:true },
{ fieldName: 'name', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:true },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:false },
{ fieldName: 'actionTypeId', displayName: 'Action Type', sortName: 'ActionType', width: '50px', isVisible:true },
{ fieldName: 'writerTypeId', displayName: 'Writer Type', sortName: 'WriterType', width: '50px', isVisible:true },
{ fieldName: 'matchProperty', displayName: 'Writer Matching Property', sortName: 'MatchProperty', width: '50px', isVisible:true },
{ fieldName: 'matchValues', displayName: 'Writer Matching Values', sortName: 'MatchValues', width: '50px', isVisible:true },
{ fieldName: 'actionTemplate', displayName: 'Action Template', sortName: 'ActionTemplate', width: '50px', isVisible:false },

];
//---------------------------------------------------------
export interface ISystemAction {
    isDeleted?: boolean;
id?: string;
name?: string;
notes?: string;
matchProperty?: string;
matchValues?: string;
actionTemplate?: string;

    actionTypeId?: number;
writerTypeId?: number;

}
//---------------------------------------------------------
export function initSystemAction(): ISystemAction {
    let entity: ISystemAction = {
        isDeleted: false,
id: '0',
name: '',
notes: '',
matchProperty: '',
matchValues: '',
actionTemplate: '',

        actionTypeId: 0,
writerTypeId: 0,

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function systemActionUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],
name: [''],
notes: [''],
matchProperty: [''],
matchValues: [''],
actionTemplate: [''],

    actionTypeId: [0],
writerTypeId: [0],

    };
} 
//---------------------------------------------------------
export interface ISystemActionWithLookup extends ISystemAction{

    actionTypeName?: string;
writerTypeName?: string;

}
//---------------------------------------------------------
export interface ISystemActionRequest extends IEntityRequest<ISystemActionFilter> {}

//---------------------------------------------------------
export interface ISystemActionFilter {
    [key: string]: any;

    Name?: string;
MatchProperty?: string;
MatchValues?: string;

    ActionTypeId?: number;
WriterTypeId?: number;

}
//---------------------------------------------------------
export function initSystemActionRequest(): ISystemActionRequest {
    let request: ISystemActionRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: SystemActionColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {

            Name: undefined ,
MatchProperty: undefined ,
MatchValues: undefined ,

            ActionTypeId: undefined ,
WriterTypeId: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

export function getSystemActionActions(record: IQueryColumn): IAction[] {
        let links: IAction[] = [];

links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/bfs/system-action/view', displayText: 'View...'
});
links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/bfs/system-action/edit', displayText: 'Edit...' 
});
links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/bfs/system-action/delete', displayText: 'Delete...' 
});
links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/bfs/system-action/add', displayText: 'Add New record'
});
links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/bfs/system-action/view', displayText: 'View...'
});
links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/bfs/system-action/edit', displayText: 'Edit...' 
});
links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/bfs/system-action/delete', displayText: 'Delete...' 
});

links.push({
actionSource:'System', actionType:'FrontendFunction', actionLocation:'ListRow',recordId: record['id'], action: 'this.goToCustomReport', displayText: 'Go To Custom Report', data: {'record':record}
});

        return links;
    }
    //---------------------------------------------------------

