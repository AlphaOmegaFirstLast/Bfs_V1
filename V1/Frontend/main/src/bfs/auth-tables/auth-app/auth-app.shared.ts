
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/infrastructure-main/infrastructure.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const AuthAppColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:true },
{ fieldName: 'name', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:true },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:false },
{ fieldName: 'bfsSystemId', displayName: 'BestFit System', sortName: 'BfsSystem', width: '50px', isVisible:true },

];
//---------------------------------------------------------
export interface IAuthApp {
    isDeleted?: boolean;
id?: string;
name?: string;
notes?: string;

    bfsSystemId?: string;

}
//---------------------------------------------------------
export function initAuthApp(): IAuthApp {
    let entity: IAuthApp = {
        isDeleted: false,
id: '0',
name: '',
notes: '',

        bfsSystemId: '0',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function authAppUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],
name: [''],
notes: [''],

    bfsSystemId: ['0'],

    };
} 
//---------------------------------------------------------
export interface IAuthAppWithLookup extends IAuthApp{

    bfsSystemName?: string;

}
//---------------------------------------------------------
export interface IAuthAppRequest extends IEntityRequest<IAuthAppFilter> {}

//---------------------------------------------------------
export interface IAuthAppFilter {
    [key: string]: any;

    Name?: string;

    BfsSystemId?: string;

}
//---------------------------------------------------------
export function initAuthAppRequest(): IAuthAppRequest {
    let request: IAuthAppRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: AuthAppColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {

            Name: undefined ,

            BfsSystemId: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

export function getAuthAppActions(component: any, record: IEntity): IAction[] {
        let links: IAction[] = [];

links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/bfs/auth-app/add', displayText: 'Add New record'
});
links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/bfs/auth-app/view', displayText: 'View...'
});
links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/bfs/auth-app/edit', displayText: 'Edit...' 
});
links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/bfs/auth-app/delete', displayText: 'Delete...' 
});
links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['bfsSystemId'], route:'/bfs/bfs-system/view', displayText:'Go to BfsSystem'
});

        return links;
    }
    //---------------------------------------------------------

