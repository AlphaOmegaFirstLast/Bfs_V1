
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/stores-main/stores.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const StoreColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false },
{ fieldName: 'name', displayName: 'Name', sortName: 'NameName', width: '50px', isVisible:true },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'NotesName', width: '50px', isVisible:false },

];
//---------------------------------------------------------
export interface IStore {
    isDeleted?: boolean;
id?: string;
name?: string;
notes?: string;

}
//---------------------------------------------------------
export function initStore(): IStore {
    let entity: IStore = {
        isDeleted: false,
id: '0',
name: '',
notes: '',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function storeUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],
name: [''],
notes: [''],

    };
} 
//---------------------------------------------------------
export interface IStoreWithLookup extends IStore{

}
//---------------------------------------------------------
export interface IStoreRequest extends IEntityRequest<IStoreFilter> {}

//---------------------------------------------------------
export interface IStoreFilter {
    [key: string]: any;

    Name?: string;

}
//---------------------------------------------------------
export function initStoreRequest(): IStoreRequest {
    let request: IStoreRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: StoreColumns.map(column => ({ ...column })),
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

export function getStoreActions(component: any, record: IEntity): IAction[] {
        let links: IAction[] = [];

if (component.accessService.isActionAllowed('store', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/str/store/add', displayText: 'Add New record'
});
}
if (component.accessService.isActionAllowed('store', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/str/store/view', displayText: 'View...'
});
}
if (component.accessService.isActionAllowed('store', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/str/store/edit', displayText: 'Edit...' 
});
}
if (component.accessService.isActionAllowed('store', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/str/store/delete', displayText: 'Delete...' 
});
}

        return links;
    }
    //---------------------------------------------------------

