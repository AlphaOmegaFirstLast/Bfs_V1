
import { IEntityRequest, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import { deleteTree, duplicateRecord, duplicateTree } from '@bfs/infrastructure-main/infrastructure.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const BfsComponentColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:true },
{ fieldName: 'bfsSystemId', displayName: 'BestFit System', sortName: 'BfsSystem', width: '50px', isVisible:true },
{ fieldName: 'isSoftDelete', displayName: 'Is Soft Delete', sortName: 'IsSoftDelete', width: '50px', isVisible:true },
{ fieldName: 'name', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:true },
{ fieldName: 'displayName', displayName: 'DisplayName', sortName: 'DisplayName', width: '50px', isVisible:true },
{ fieldName: 'dataTypeId', displayName: 'Data Type', sortName: 'DataType', width: '50px', isVisible:true },
{ fieldName: 'menuName', displayName: 'MenuName', sortName: 'MenuName', width: '50px', isVisible:false },
{ fieldName: 'menuPlaceHolder', displayName: 'MenuPlaceHolder', sortName: 'MenuPlaceHolder', width: '50px', isVisible:false },
{ fieldName: 'queryBaseTable', displayName: 'QueryBaseTable', sortName: 'QueryBaseTable', width: '50px', isVisible:true },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:false },
{ fieldName: 'bfsField', displayName: 'Structure \ Fields', sortName: 'BfsField', width: '50px', isVisible:false },
{ fieldName: 'systemAction', displayName: 'System Actions', sortName: 'SystemAction', width: '50px', isVisible:false },
{ fieldName: 'businessAction', displayName: 'Business Actions', sortName: 'BusinessAction', width: '50px', isVisible:false },

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

        bfsSystemId: '0',
dataTypeId: 0,

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function bfsComponentUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],
isSoftDelete: [false],
name: [''],
displayName: [''],
menuName: [''],
menuPlaceHolder: [''],
queryBaseTable: [''],
notes: [''],

    bfsSystemId: ['0'],
dataTypeId: [0],

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

    Name?: string;

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

            Name: undefined ,

            BfsSystemId: undefined ,
DataTypeId: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

export function getBfsComponentActions(component: any, record: IQueryColumn): IAction[] {
        let links: IAction[] = [];

links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/bfs/bfs-component/view', displayText: 'View...'
});
links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/bfs/bfs-component/edit', displayText: 'Edit...' 
});
links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/bfs/bfs-component/delete', displayText: 'Delete...' 
});
links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/bfs/bfs-component/add', displayText: 'Add New record'
});
links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/bfs/bfs-component/view', displayText: 'View...'
});
links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/bfs/bfs-component/edit', displayText: 'Edit...' 
});
links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/bfs/bfs-component/delete', displayText: 'Delete...' 
});
links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/bfs/bfs-component/add', displayText: 'Add New record'
});
links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/bfs/bfs-component/view', displayText: 'View...'
});
links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/bfs/bfs-component/edit', displayText: 'Edit...' 
});
links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/bfs/bfs-component/delete', displayText: 'Delete...' 
});
links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['bfsSystemId'], route:'/bfs/bfs-system/view', displayText:'Go to BfsSystem'
});
links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['dataTypeId'], route:'/bfs/data-type/view', displayText:'Go to DataType'
});

links.push({
actionSource:'System', actionType:'FrontendFunction', actionLocation:'ListRow',recordId: record['id'], action: component.goToCustomReport, displayText: 'Go To Custom Report', data: {'record':record}
});
links.push({
actionSource:'System', actionType:'FrontendFunction', actionLocation:'ListRow',recordId: record['id'], action: duplicateRecord, displayText: 'Duplicate Record', data: {recordId: record['id'], postUrl:'/BfsComponent', onSuccessMethodName: 'getReport' }
});
links.push({
actionSource:'System', actionType:'FrontendFunction', actionLocation:'ListRow',recordId: record['id'], action: duplicateTree, displayText: 'Duplicate Tree', data: { recordId: record['id'], postUrl: '/Operations/BfsComponent/DuplicateTree', onSuccessMethodName: 'getReport' }
});
links.push({
actionSource:'System', actionType:'FrontendFunction', actionLocation:'ListRow',recordId: record['id'], action: deleteTree, displayText: 'Delete Tree', data: { recordId: record['id'], postUrl: '/Operations/BfsComponent/DeleteTree', onSuccessMethodName: 'getReport' }
});

        return links;
    }
    //---------------------------------------------------------

