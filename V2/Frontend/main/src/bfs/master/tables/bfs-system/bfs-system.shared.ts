import { FormBuilder } from "@angular/forms";
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
import { getFormControlValidation } from "@bfs/_shared/objectFields";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/master/main/master.operations';
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

// Output Columns of a Query  [used in entity Query]
export const BfsSystemColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'isMaster', displayName: 'Is BestFit Master System', sortName: 'IsMaster', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'systemTemplateId', displayName: 'Template', sortName: 'SystemTemplate_Name', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'basePortNumber', displayName: 'Base Port Number', sortName: 'BasePortNumber', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'dbPrefix', displayName: 'DB Prefix', sortName: 'DbPrefix', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'logo', displayName: 'Logo', sortName: 'Logo', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'name', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:true, columnOrder:1 },

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
    isDeleted: [false,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
id: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
isMaster: [false,getFormControlValidation('{"IsRequired":false,"MinLength":"","MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
notes: ['',getFormControlValidation('{"IsRequired":false,"MinLength":"","MaxLength":"500","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
basePortNumber: ['',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
dbPrefix: ['',getFormControlValidation('{"IsRequired":false,"MinLength":"2","MaxLength":"50","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
logo: ['',getFormControlValidation('{"IsRequired":true,"MinLength":"3","MaxLength":"300","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
name: ['',getFormControlValidation('{"IsRequired":true,"MinLength":"3","MaxLength":"50","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

    systemTemplateId: [0,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

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
export function renderBfsSystem(record: IEntity, column: IQueryColumn): any {
        const value = record[column.fieldName as keyof IEntity];
        switch (column.fieldName) {
            case 'systemTemplateId':
                return record['systemTemplateName']?.toString();

            default:
                return value;
        }
        return value;
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
//Template_Start_Code_DontOverwrite_3
//Template_End_Code_DontOverwrite_3

//Template_Start_Code_DontOverwrite_4
//Template_End_Code_DontOverwrite_4

