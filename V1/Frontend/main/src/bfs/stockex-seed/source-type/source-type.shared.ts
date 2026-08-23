
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/stockex-main/stockex.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const SourceTypeColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false },
{ fieldName: 'name', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:true },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:false },

];
//---------------------------------------------------------
export interface ISourceType {
    isDeleted?: boolean;
id?: string;
name?: string;
notes?: string;

}
//---------------------------------------------------------
export function initSourceType(): ISourceType {
    let entity: ISourceType = {
        isDeleted: false,
id: '0',
name: '',
notes: '',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function sourceTypeUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],
name: [''],
notes: [''],

    };
} 
//---------------------------------------------------------
export interface ISourceTypeWithLookup extends ISourceType{

}
//---------------------------------------------------------
export interface ISourceTypeRequest extends IEntityRequest<ISourceTypeFilter> {}

//---------------------------------------------------------
export interface ISourceTypeFilter {
    [key: string]: any;
    Id?: string;

    Name?: string;

}
//---------------------------------------------------------
export function initSourceTypeRequest(): ISourceTypeRequest {
    let request: ISourceTypeRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: SourceTypeColumns.map(column => ({ ...column })),
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

export function getSourceTypeActions(component: any, record: IEntity): IAction[] {
        let links: IAction[] = [];

if (component.accessService.isActionAllowed('sourceType', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/stkx/source-type/add', displayText: 'Add New record'
});
}
if (component.accessService.isActionAllowed('sourceType', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/source-type/view', displayText: 'View...'
});
}
if (component.accessService.isActionAllowed('sourceType', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/source-type/edit', displayText: 'Edit...' 
});
}
if (component.accessService.isActionAllowed('sourceType', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/source-type/delete', displayText: 'Delete...' 
});
}

        return links;
    }
    //---------------------------------------------------------

