
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/infrastructure-main/infrastructure.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const BfsClientSystemColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:true },
{ fieldName: 'bfsClientId', displayName: 'Client Name', sortName: 'BfsClient', width: '50px', isVisible:true },
{ fieldName: 'bfsSystemId', displayName: 'BestFit System', sortName: 'BfsSystem', width: '50px', isVisible:true },

];
//---------------------------------------------------------
export interface IBfsClientSystem {
    isDeleted?: boolean;
id?: string;

    bfsClientId?: string;
bfsSystemId?: string;

}
//---------------------------------------------------------
export function initBfsClientSystem(): IBfsClientSystem {
    let entity: IBfsClientSystem = {
        isDeleted: false,
id: '0',

        bfsClientId: '0',
bfsSystemId: '0',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function bfsClientSystemUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],

    bfsClientId: ['0'],
bfsSystemId: ['0'],

    };
} 
//---------------------------------------------------------
export interface IBfsClientSystemWithLookup extends IBfsClientSystem{

    bfsClientName?: string;
bfsSystemName?: string;

}
//---------------------------------------------------------
export interface IBfsClientSystemRequest extends IEntityRequest<IBfsClientSystemFilter> {}

//---------------------------------------------------------
export interface IBfsClientSystemFilter {
    [key: string]: any;

    BfsClientId?: string;
BfsSystemId?: string;

}
//---------------------------------------------------------
export function initBfsClientSystemRequest(): IBfsClientSystemRequest {
    let request: IBfsClientSystemRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: BfsClientSystemColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {

            BfsClientId: undefined ,
BfsSystemId: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

export function getBfsClientSystemActions(component: any, record: IEntity): IAction[] {
        let links: IAction[] = [];

links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/bfs/bfs-client-system/add', displayText: 'Add New record'
});
links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/bfs/bfs-client-system/view', displayText: 'View...'
});
links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/bfs/bfs-client-system/edit', displayText: 'Edit...' 
});
links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/bfs/bfs-client-system/delete', displayText: 'Delete...' 
});
links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['bfsClientId'], route:'/bfs/bfs-client/view', displayText:'Go to BfsClient'
});
links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['bfsSystemId'], route:'/bfs/bfs-system/view', displayText:'Go to BfsSystem'
});

        return links;
    }
    //---------------------------------------------------------

//Template_Start_Code_DontOverwrite_1

//Template_End_Code_DontOverwrite_1

