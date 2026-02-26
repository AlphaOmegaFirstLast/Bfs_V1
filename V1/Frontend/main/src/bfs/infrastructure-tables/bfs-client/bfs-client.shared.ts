
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/infrastructure-main/infrastructure.operations';

import { ICustomField, initCustomFields } from "@bfs/_shared/customFields";

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const BfsClientColumns = [
    { fieldName: 'dbConnection', displayName: 'Database Connection', sortName: 'DbConnection', width: '50px', isVisible:false },
{ fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:true },
{ fieldName: 'name', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:true },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:false },
{ fieldName: 'customFields', displayName: 'Custom Fields', sortName: 'CustomFields', width: '50px', isVisible:false },

];
//---------------------------------------------------------
export interface IBfsClient {
    dbConnection?: string;
isDeleted?: boolean;
id?: string;
name?: string;
notes?: string;

    customFields?: ICustomField[];

}
//---------------------------------------------------------
export function initBfsClient(): IBfsClient {
    let entity: IBfsClient = {
        dbConnection: '',
isDeleted: false,
id: '0',
name: '',
notes: '',

        customFields: initCustomFields(),

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function bfsClientUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    dbConnection: [''],
isDeleted: [false],
id: ['0'],
name: [''],
notes: [''],

    customFields: formBuilder.array([]),

    };
} 
//---------------------------------------------------------
export interface IBfsClientWithLookup extends IBfsClient{

}
//---------------------------------------------------------
export interface IBfsClientRequest extends IEntityRequest<IBfsClientFilter> {}

//---------------------------------------------------------
export interface IBfsClientFilter {
    [key: string]: any;

    Name?: string;

}
//---------------------------------------------------------
export function initBfsClientRequest(): IBfsClientRequest {
    let request: IBfsClientRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: BfsClientColumns.map(column => ({ ...column })),
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

export function getBfsClientActions(component: any, record: IEntity): IAction[] {
        let links: IAction[] = [];

links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/bfs/bfs-client/add', displayText: 'Add New record'
});
links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/bfs/bfs-client/view', displayText: 'View...'
});
links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/bfs/bfs-client/edit', displayText: 'Edit...' 
});
links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/bfs/bfs-client/delete', displayText: 'Delete...' 
});

links.push({
actionSource:'System', actionType:'FrontendFunction', actionLocation:'ListRow',recordId: record['id'], action: operations.duplicateRecord, displayText: 'Duplicate Record', data: {recordId: record['id'], postUrl:'/BfsClient', onSuccessMethodName: 'getReport' }
});

        return links;
    }
    //---------------------------------------------------------

//Template_Start_Code_DontOverwrite_1

//Template_End_Code_DontOverwrite_1
//Template_Start_Code_DontOverwrite_2

//Template_End_Code_DontOverwrite_2
//Template_Start_Code_DontOverwrite_3

//Template_End_Code_DontOverwrite_3

