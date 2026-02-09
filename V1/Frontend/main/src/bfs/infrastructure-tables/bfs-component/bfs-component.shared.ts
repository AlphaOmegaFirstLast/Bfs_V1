
import { IEntityRequest, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import { deleteTree, duplicateRecord, duplicateTree } from '@bfs/infrastructure-main/infrastructure.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const BfsComponentColumns = [
    { fieldName: 'bfsComponentId', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:true },
{ fieldName: 'bfsComponentBfsSystemId', displayName: 'BestFit System', sortName: 'BfsSystem', width: '50px', isVisible:true },
{ fieldName: 'bfsComponentIsSoftDelete', displayName: 'Is Soft Delete', sortName: 'IsSoftDelete', width: '50px', isVisible:true },
{ fieldName: 'bfsComponentName', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:true },
{ fieldName: 'bfsComponentDisplayName', displayName: 'DisplayName', sortName: 'DisplayName', width: '50px', isVisible:true },
{ fieldName: 'bfsComponentDataTypeId', displayName: 'Data Type', sortName: 'DataType', width: '50px', isVisible:true },
{ fieldName: 'bfsComponentMenuName', displayName: 'MenuName', sortName: 'MenuName', width: '50px', isVisible:true },
{ fieldName: 'bfsComponentMenuPlaceHolder', displayName: 'MenuPlaceHolder', sortName: 'MenuPlaceHolder', width: '50px', isVisible:true },
{ fieldName: 'bfsComponentQueryBaseTable', displayName: 'QueryBaseTable', sortName: 'QueryBaseTable', width: '50px', isVisible:true },
{ fieldName: 'bfsComponentNotes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:true },

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

export function getBfsComponentActions(record: IQueryColumn): IAction[] {
        let links: IAction[] = [];

links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/bfs/bfs-component/add', displayText: 'Add New record' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['bfsComponentId'], route:'/bfs/bfs-component/view', displayText: 'View...' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['bfsComponentId'], route:'/bfs/bfs-component/edit', displayText: 'Edit...' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['bfsComponentId'], route:'/bfs/bfs-component/delete', displayText: 'Delete...' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['bfsSystemId'], route:'/bfs/bfs-system/view', displayText:'Go to BfsSystem' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['dataTypeId'], route:'/bfs/data-type/view', displayText:'Go to DataType' 
});

links.push({
actionSource:'0', actionType:'FrontendFunction', actionLocation:'ListRow',recordId: record['bfsComponentId'], action: duplicateRecord, displayText: 'Duplicate Record', data: {recordId: record['bfsComponentId'], postUrl: '/BfsComponent', onSuccessMethodName: 'getReport' } 
});
links.push({
actionSource:'0', actionType:'FrontendFunction', actionLocation:'ListRow',recordId: record['bfsComponentId'], action: duplicateTree, displayText: 'Duplicate Tree', data: { recordId: record['bfsComponentId'], postUrl: '/Operations/BfsComponent/DuplicateTree', onSuccessMethodName: 'getReport' } 
});

        return links;
    }
    //---------------------------------------------------------

