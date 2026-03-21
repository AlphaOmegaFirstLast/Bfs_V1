
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/infrastructure-main/infrastructure.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const StrCurrencyColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:true },
{ fieldName: 'name', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:true },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:false },

];
//---------------------------------------------------------
export interface IStrCurrency {
    isDeleted?: boolean;
id?: string;
name?: string;
notes?: string;

}
//---------------------------------------------------------
export function initStrCurrency(): IStrCurrency {
    let entity: IStrCurrency = {
        isDeleted: false,
id: '0',
name: '',
notes: '',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function strCurrencyUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],
name: [''],
notes: [''],

    };
} 
//---------------------------------------------------------
export interface IStrCurrencyWithLookup extends IStrCurrency{

}
//---------------------------------------------------------
export interface IStrCurrencyRequest extends IEntityRequest<IStrCurrencyFilter> {}

//---------------------------------------------------------
export interface IStrCurrencyFilter {
    [key: string]: any;

    Name?: string;

}
//---------------------------------------------------------
export function initStrCurrencyRequest(): IStrCurrencyRequest {
    let request: IStrCurrencyRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: StrCurrencyColumns.map(column => ({ ...column })),
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

export function getStrCurrencyActions(component: any, record: IEntity): IAction[] {
        let links: IAction[] = [];

links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/str/str-currency/add', displayText: 'Add New record'
});
links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/str/str-currency/view', displayText: 'View...'
});
links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/str/str-currency/edit', displayText: 'Edit...' 
});
links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/str/str-currency/delete', displayText: 'Delete...' 
});

        return links;
    }
    //---------------------------------------------------------

