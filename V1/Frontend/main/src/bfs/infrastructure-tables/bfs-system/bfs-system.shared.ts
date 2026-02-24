
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import { deleteTree, duplicateRecord, duplicateTree } from '@bfs/infrastructure-main/infrastructure.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const BfsSystemColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:true },
{ fieldName: 'name', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:true },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:false },
{ fieldName: 'bfsClientId', displayName: 'BestFit Client', sortName: 'BfsClient', width: '50px', isVisible:true },
{ fieldName: 'systemTemplateId', displayName: 'Template', sortName: 'SystemTemplate', width: '50px', isVisible:true },
{ fieldName: 'basePortNumber', displayName: 'Base Port Number', sortName: 'BasePortNumber', width: '50px', isVisible:true },
{ fieldName: 'dbPrefix', displayName: 'DB Prefix', sortName: 'DbPrefix', width: '50px', isVisible:true },

];
//---------------------------------------------------------
export interface IBfsSystem {
    isDeleted?: boolean;
id?: string;
name?: string;
notes?: string;
basePortNumber?: string;
dbPrefix?: string;

    bfsClientId?: string;
systemTemplateId?: number;

}
//---------------------------------------------------------
export function initBfsSystem(): IBfsSystem {
    let entity: IBfsSystem = {
        isDeleted: false,
id: '0',
name: '',
notes: '',
basePortNumber: '',
dbPrefix: '',

        bfsClientId: '0',
systemTemplateId: 0,

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function bfsSystemUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],
name: [''],
notes: [''],
basePortNumber: [''],
dbPrefix: [''],

    bfsClientId: ['0'],
systemTemplateId: [0],

    };
} 
//---------------------------------------------------------
export interface IBfsSystemWithLookup extends IBfsSystem{

    bfsClientName?: string;
systemTemplateName?: string;

}
//---------------------------------------------------------
export interface IBfsSystemRequest extends IEntityRequest<IBfsSystemFilter> {}

//---------------------------------------------------------
export interface IBfsSystemFilter {
    [key: string]: any;

    Name?: string;

    BfsClientId?: string;
SystemTemplateId?: number;

}
//---------------------------------------------------------
export function initBfsSystemRequest(): IBfsSystemRequest {
    let request: IBfsSystemRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: BfsSystemColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {

            Name: undefined ,

            BfsClientId: undefined ,
SystemTemplateId: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

export function getBfsSystemActions(component: any, record: IEntity): IAction[] {
        let links: IAction[] = [];

links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/bfs/bfs-system/add', displayText: 'Add New record'
});
links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/bfs/bfs-system/view', displayText: 'View...'
});
links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/bfs/bfs-system/edit', displayText: 'Edit...' 
});
links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/bfs/bfs-system/delete', displayText: 'Delete...' 
});

links.push({
actionSource:'System', actionType:'FrontendFunction', actionLocation:'ListRow',recordId: record['id'], action: duplicateRecord, displayText: 'Duplicate Record', data: {recordId: record['id'], postUrl:'/BfsSystem', onSuccessMethodName: 'getReport' }
});

        return links;
    }
    //---------------------------------------------------------

