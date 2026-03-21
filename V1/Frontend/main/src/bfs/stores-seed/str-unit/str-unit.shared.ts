
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/infrastructure-main/infrastructure.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const StrUnitColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:true },
{ fieldName: 'name', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:true },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:false },

];
//---------------------------------------------------------
export interface IStrUnit {
    isDeleted?: boolean;
id?: string;
name?: string;
notes?: string;

}
//---------------------------------------------------------
export function initStrUnit(): IStrUnit {
    let entity: IStrUnit = {
        isDeleted: false,
id: '0',
name: '',
notes: '',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function strUnitUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],
name: [''],
notes: [''],

    };
} 
//---------------------------------------------------------
export interface IStrUnitWithLookup extends IStrUnit{

}
//---------------------------------------------------------
export interface IStrUnitRequest extends IEntityRequest<IStrUnitFilter> {}

//---------------------------------------------------------
export interface IStrUnitFilter {
    [key: string]: any;

    Name?: string;

}
//---------------------------------------------------------
export function initStrUnitRequest(): IStrUnitRequest {
    let request: IStrUnitRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: StrUnitColumns.map(column => ({ ...column })),
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

export function getStrUnitActions(component: any, record: IEntity): IAction[] {
        let links: IAction[] = [];

links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/str/str-unit/add', displayText: 'Add New record'
});
links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/str/str-unit/view', displayText: 'View...'
});
links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/str/str-unit/edit', displayText: 'Edit...' 
});
links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/str/str-unit/delete', displayText: 'Delete...' 
});

        return links;
    }
    //---------------------------------------------------------

