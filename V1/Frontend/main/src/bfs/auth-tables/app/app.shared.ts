
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/auth-main/auth.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const AppColumns = [
    { fieldName: 'app_Id', displayName: 'ID', sortName: 'App_Id', width: '50px', isVisible:false },
{ fieldName: 'app_Name', displayName: 'Name', sortName: 'App_Name', width: '50px', isVisible:true },
{ fieldName: 'app_Notes', displayName: 'Notes', sortName: 'App_Notes', width: '50px', isVisible:false },
{ fieldName: 'app_BfsSystemId', displayName: 'BestFit System', sortName: 'BfsSystem_Name', width: '50px', isVisible:true },
{ fieldName: 'app_Logo', displayName: 'Logo', sortName: 'App_Logo', width: '50px', isVisible:false },

];
//---------------------------------------------------------
export interface IApp {
    isDeleted?: boolean;
id?: string;
name?: string;
notes?: string;
logo?: string;

    bfsSystemId?: string;

}
//---------------------------------------------------------
export function initApp(): IApp {
    let entity: IApp = {
        isDeleted: false,
id: '0',
name: '',
notes: '',
logo: '',

        bfsSystemId: '0',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function appUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],
name: [''],
notes: [''],
logo: [''],

    bfsSystemId: ['0'],

    };
} 
//---------------------------------------------------------
export interface IAppWithLookup extends IApp{

    bfsSystemName?: string;

}
//---------------------------------------------------------
export interface IAppRequest extends IEntityRequest<IAppFilter> {}

//---------------------------------------------------------
export interface IAppFilter {
    [key: string]: any;
    Id?: string;

    Name?: string;
Logo?: string;

    BfsSystemId?: string;

}
//---------------------------------------------------------
export function initAppRequest(): IAppRequest {
    let request: IAppRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: AppColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {
            Id: undefined ,

            Name: undefined ,
Logo: undefined ,

            BfsSystemId: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

export function getAppActions(component: any, record: IEntity): IAction[] {
        let links: IAction[] = [];

if (component.accessService.isActionAllowed('app', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/ath/app/add', displayText: 'Add New record'
});
}
if (component.accessService.isActionAllowed('app', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/ath/app/view', displayText: 'View...'
});
}
if (component.accessService.isActionAllowed('app', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/ath/app/edit', displayText: 'Edit...' 
});
}
if (component.accessService.isActionAllowed('app', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/ath/app/delete', displayText: 'Delete...' 
});
}
if (component.accessService.isActionAllowed('app', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['bfsSystemId'], route:'/ath/bfs-system/view', displayText:'Go to BfsSystem'
});
}

        return links;
    }
    //---------------------------------------------------------

