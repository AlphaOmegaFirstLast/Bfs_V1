
import { IEntityRequest, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import { deleteTree, duplicateRecord, duplicateTree } from '@bfs/infrastructure-main/infrastructure.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const FilterTypeColumns = [
    { fieldName: 'filterTypeId', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:true },
{ fieldName: 'filterTypeName', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:true },
{ fieldName: 'filterTypeNotes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:true },

];
//---------------------------------------------------------
export interface IFilterType {
    isDeleted?: boolean;
id?: string;
name?: string;
notes?: string;

}
//---------------------------------------------------------
export function initFilterType(): IFilterType {
    let entity: IFilterType = {
        isDeleted: false,
id: '0',
name: '',
notes: '',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function filterTypeUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],
name: [''],
notes: [''],

    };
} 
//---------------------------------------------------------
export interface IFilterTypeWithLookup extends IFilterType{

}
//---------------------------------------------------------
export interface IFilterTypeRequest extends IEntityRequest<IFilterTypeFilter> {}

//---------------------------------------------------------
export interface IFilterTypeFilter {
    [key: string]: any;

    Name?: string;

}
//---------------------------------------------------------
export function initFilterTypeRequest(): IFilterTypeRequest {
    let request: IFilterTypeRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: FilterTypeColumns.map(column => ({ ...column })),
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

export function getFilterTypeActions(record: IQueryColumn): IAction[] {
        let links: IAction[] = [];

links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/bfs/filter-type/add', displayText: 'Add New record' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['filterTypeId'], route:'/bfs/filter-type/view', displayText: 'View...' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['filterTypeId'], route:'/bfs/filter-type/edit', displayText: 'Edit...' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['filterTypeId'], route:'/bfs/filter-type/delete', displayText: 'Delete...' 
});

links.push({
actionSource:'0', actionType:'FrontendFunction', actionLocation:'ListRow',recordId: record['filterTypeId'], action: duplicateRecord, displayText: 'Duplicate Record', data: { postUrl: '/FilterType', onSuccessMethodName: 'getReport' } 
});
links.push({
actionSource:'0', actionType:'FrontendFunction', actionLocation:'ListRow',recordId: record['filterTypeId'], action: duplicateTree, displayText: 'Duplicate Tree', data: { postUrl: '/Operations/FilterType/DuplicateTree', onSuccessMethodName: 'getReport' } 
});

        return links;
    }
    //---------------------------------------------------------

