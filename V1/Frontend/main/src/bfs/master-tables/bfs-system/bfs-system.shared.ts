
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/master-main/master.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const BfsSystemColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false },
{ fieldName: 'isMaster', displayName: 'Is BestFit Master System', sortName: 'IsMaster', width: '50px', isVisible:true },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:false },
{ fieldName: 'systemTemplateId', displayName: 'Template', sortName: 'SystemTemplate_Name', width: '50px', isVisible:true },
{ fieldName: 'basePortNumber', displayName: 'Base Port Number', sortName: 'BasePortNumber', width: '50px', isVisible:true },
{ fieldName: 'dbPrefix', displayName: 'DB Prefix', sortName: 'DbPrefix', width: '50px', isVisible:true },
{ fieldName: 'logo', displayName: 'Logo', sortName: 'Logo', width: '50px', isVisible:false },
{ fieldName: 'name', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:true },

];
//---------------------------------------------------------
export interface IBfsSystem {
    isDeleted?: boolean;
id?: string;
isMaster?: boolean;
notes?: string;
basePortNumber?: string;
dbPrefix?: string;
logo?: string;
name?: string;

    systemTemplateId?: number;

}
//---------------------------------------------------------
export function initBfsSystem(): IBfsSystem {
    let entity: IBfsSystem = {
        isDeleted: false,
id: '0',
isMaster: false,
notes: '',
basePortNumber: '',
dbPrefix: '',
logo: '',
name: '',

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
isMaster: [false],
notes: [''],
basePortNumber: [''],
dbPrefix: [''],
logo: [''],
name: [''],

    systemTemplateId: [0],

    };
} 
//---------------------------------------------------------
export interface IBfsSystemWithLookup extends IBfsSystem{

    systemTemplateName?: string;

}
//---------------------------------------------------------
export interface IBfsSystemRequest extends IEntityRequest<IBfsSystemFilter> {}

//---------------------------------------------------------
export interface IBfsSystemFilter {
    [key: string]: any;
    Id?: string;

    Logo?: string;
Name?: string;

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
            Id: undefined ,

            Logo: undefined ,
Name: undefined ,

            SystemTemplateId: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

export function getBfsSystemActions(component: any, record: IEntity): IAction[] {
        let links: IAction[] = [];

if (component.accessService.isActionAllowed('bfsSystem', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/mstr/bfs-system/add', displayText: 'Add New record'
});
}
if (component.accessService.isActionAllowed('bfsSystem', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/mstr/bfs-system/view', displayText: 'View...'
});
}
if (component.accessService.isActionAllowed('bfsSystem', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/mstr/bfs-system/edit', displayText: 'Edit...' 
});
}
if (component.accessService.isActionAllowed('bfsSystem', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/mstr/bfs-system/delete', displayText: 'Delete...' 
});
}

if (component.accessService.isActionAllowed('bfsSystem', ''))
{links.push({
actionSource:'System', actionType:'FrontendFunction', actionLocation:'ListRow',recordId: record['id'], action: operations.duplicateRecord, displayText: 'Duplicate Record', data: {recordId: record['id'], postUrl:'/BfsSystem', onSuccessMethodName: 'getReport' }
});
}

        return links;
    }
    //---------------------------------------------------------

