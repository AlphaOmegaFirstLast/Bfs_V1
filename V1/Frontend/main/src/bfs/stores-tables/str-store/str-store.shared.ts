
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/infrastructure-main/infrastructure.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const StrStoreColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:true },
{ fieldName: 'name', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:true },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:false },

];
//---------------------------------------------------------
export interface IStrStore {
    isDeleted?: boolean;
id?: string;
name?: string;
notes?: string;

}
//---------------------------------------------------------
export function initStrStore(): IStrStore {
    let entity: IStrStore = {
        isDeleted: false,
id: '0',
name: '',
notes: '',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function strStoreUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],
name: [''],
notes: [''],

    };
} 
//---------------------------------------------------------
export interface IStrStoreWithLookup extends IStrStore{

}
//---------------------------------------------------------
export interface IStrStoreRequest extends IEntityRequest<IStrStoreFilter> {}

//---------------------------------------------------------
export interface IStrStoreFilter {
    [key: string]: any;

    Name?: string;

}
//---------------------------------------------------------
export function initStrStoreRequest(): IStrStoreRequest {
    let request: IStrStoreRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: StrStoreColumns.map(column => ({ ...column })),
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

export function getStrStoreActions(component: any, record: IEntity): IAction[] {
        let links: IAction[] = [];

links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/str/str-store/add', displayText: 'Add New record'
});
links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/str/str-store/view', displayText: 'View...'
});
links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/str/str-store/edit', displayText: 'Edit...' 
});
links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/str/str-store/delete', displayText: 'Delete...' 
});

        return links;
    }
    //---------------------------------------------------------

