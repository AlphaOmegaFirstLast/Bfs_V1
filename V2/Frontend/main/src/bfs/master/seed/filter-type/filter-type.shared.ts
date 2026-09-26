import { FormBuilder } from "@angular/forms";
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
import { getFormControlValidation } from "@bfs/_shared/objectFields";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/master/main/master.operations';
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

// Output Columns of a Query  [used in entity Query]
export const FilterTypeColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'name', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:false, columnOrder:1 },

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
    isDeleted: [false,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
id: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
name: ['',getFormControlValidation('{"IsRequired":true,"MinLength":"3","MaxLength":"50","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
notes: ['',getFormControlValidation('{"IsRequired":false,"MinLength":"","MaxLength":"1000","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

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
    Id?: string;

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
            Id: undefined ,

            Name: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------
export function renderFilterType(record: IEntity, column: IQueryColumn): any {
        const value = record[column.fieldName as keyof IEntity];
        switch (column.fieldName) {

            default:
                return value;
        }
        return value;
    }
    //---------------------------------------------------------
export function getFilterTypeActions(component: any, record: IEntity): IAction[] {
        let links: IAction[] = [];

if (component.accessService.isActionAllowed('filterType', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/mstr/filter-type/add', displayText: 'Add New record'
});
}
if (component.accessService.isActionAllowed('filterType', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/mstr/filter-type/view', displayText: 'View...'
});
}
if (component.accessService.isActionAllowed('filterType', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/mstr/filter-type/edit', displayText: 'Edit...' 
});
}
if (component.accessService.isActionAllowed('filterType', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/mstr/filter-type/delete', displayText: 'Delete...' 
});
}

        return links;
    }
//---------------------------------------------------------
//Template_Start_Code_DontOverwrite_3
//Template_End_Code_DontOverwrite_3

//Template_Start_Code_DontOverwrite_4
//Template_End_Code_DontOverwrite_4

