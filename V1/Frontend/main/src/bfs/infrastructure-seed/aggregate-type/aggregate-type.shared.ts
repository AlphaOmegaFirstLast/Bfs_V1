
import { IEntityRequest, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import { deleteTree, duplicateRecord, duplicateTree } from '@bfs/infrastructure-main/infrastructure.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const AggregateTypeColumns = [
    { fieldName: 'aggregateTypeId', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:true },
{ fieldName: 'aggregateTypeName', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:true },
{ fieldName: 'aggregateTypeNotes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:true },

];
//---------------------------------------------------------
export interface IAggregateType {
    isDeleted?: boolean;
id?: string;
name?: string;
notes?: string;

}
//---------------------------------------------------------
export function initAggregateType(): IAggregateType {
    let entity: IAggregateType = {
        isDeleted: false,
id: '0',
name: '',
notes: '',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function aggregateTypeUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],
name: [''],
notes: [''],

    };
} 
//---------------------------------------------------------
export interface IAggregateTypeWithLookup extends IAggregateType{

}
//---------------------------------------------------------
export interface IAggregateTypeRequest extends IEntityRequest<IAggregateTypeFilter> {}

//---------------------------------------------------------
export interface IAggregateTypeFilter {
    [key: string]: any;

    Name?: string;

}
//---------------------------------------------------------
export function initAggregateTypeRequest(): IAggregateTypeRequest {
    let request: IAggregateTypeRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: AggregateTypeColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {

            Name: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

export function getAggregateTypeActions(record: IQueryColumn): IAction[] {
        let links: IAction[] = [];

links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/bfs/aggregate-type/add', displayText: 'Add New record' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['aggregateTypeId'], route:'/bfs/aggregate-type/view', displayText: 'View...' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['aggregateTypeId'], route:'/bfs/aggregate-type/edit', displayText: 'Edit...' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['aggregateTypeId'], route:'/bfs/aggregate-type/delete', displayText: 'Delete...' 
});

links.push({
actionSource:'0', actionType:'FrontendFunction', actionLocation:'ListRow',recordId: record['aggregateTypeId'], action: duplicateRecord, displayText: 'Duplicate Record', data: { postUrl: '/AggregateType', onSuccessMethodName: 'getReport' } 
});
links.push({
actionSource:'0', actionType:'FrontendFunction', actionLocation:'ListRow',recordId: record['aggregateTypeId'], action: duplicateTree, displayText: 'Duplicate Tree', data: { postUrl: '/Operations/AggregateType/DuplicateTree', onSuccessMethodName: 'getReport' } 
});

        return links;
    }
    //---------------------------------------------------------

