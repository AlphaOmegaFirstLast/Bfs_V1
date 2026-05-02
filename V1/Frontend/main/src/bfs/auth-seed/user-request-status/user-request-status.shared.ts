
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/auth-main/auth.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const UserRequestStatusColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false },
{ fieldName: 'name', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:true },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:false },

];
//---------------------------------------------------------
export interface IUserRequestStatus {
    isDeleted?: boolean;
id?: string;
name?: string;
notes?: string;

}
//---------------------------------------------------------
export function initUserRequestStatus(): IUserRequestStatus {
    let entity: IUserRequestStatus = {
        isDeleted: false,
id: '0',
name: '',
notes: '',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function userRequestStatusUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],
name: [''],
notes: [''],

    };
} 
//---------------------------------------------------------
export interface IUserRequestStatusWithLookup extends IUserRequestStatus{

}
//---------------------------------------------------------
export interface IUserRequestStatusRequest extends IEntityRequest<IUserRequestStatusFilter> {}

//---------------------------------------------------------
export interface IUserRequestStatusFilter {
    [key: string]: any;
    Id?: string;

    Name?: string;

}
//---------------------------------------------------------
export function initUserRequestStatusRequest(): IUserRequestStatusRequest {
    let request: IUserRequestStatusRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: UserRequestStatusColumns.map(column => ({ ...column })),
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

export function getUserRequestStatusActions(component: any, record: IEntity): IAction[] {
        let links: IAction[] = [];

if (component.accessService.isActionAllowed('userRequestStatus', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/ath/user-request-status/add', displayText: 'Add New record'
});
}
if (component.accessService.isActionAllowed('userRequestStatus', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/ath/user-request-status/view', displayText: 'View...'
});
}
if (component.accessService.isActionAllowed('userRequestStatus', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/ath/user-request-status/edit', displayText: 'Edit...' 
});
}
if (component.accessService.isActionAllowed('userRequestStatus', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/ath/user-request-status/delete', displayText: 'Delete...' 
});
}

        return links;
    }
    //---------------------------------------------------------

