import { FormBuilder } from "@angular/forms";
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
import { getFormControlValidation } from "@bfs/_shared/objectFields";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/master-main/master.operations';

// Output Columns of a Query  [used in entity Query]
export const BfsManualCodeColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'bfsSystemId', displayName: 'BestFit System', sortName: 'BfsSystem_Name', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'bfsComponentId', displayName: 'Component', sortName: 'BfsComponent_Name', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'name', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:true, columnOrder:3 },
{ fieldName: 'fileName', displayName: 'FileName', sortName: 'FileName', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'startTemplate', displayName: 'Start Template', sortName: 'StartTemplate', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'endTemplate', displayName: 'End Template', sortName: 'EndTemplate', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'code', displayName: 'Code', sortName: 'Code', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:false, columnOrder:1 },

];
//---------------------------------------------------------
export interface IBfsManualCode {
    isDeleted?: boolean;
id?: string;
name?: string;
fileName?: string;
startTemplate?: string;
endTemplate?: string;
code?: string;
notes?: string;

    bfsSystemId?: string;
bfsComponentId?: string;

}
//---------------------------------------------------------
export function initBfsManualCode(): IBfsManualCode {
    let entity: IBfsManualCode = {
        isDeleted: false,
id: '0',
name: '',
fileName: '',
startTemplate: '',
endTemplate: '',
code: '',
notes: '',

        bfsSystemId: '0',
bfsComponentId: '0',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function bfsManualCodeUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
id: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
name: ['',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
fileName: ['',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
startTemplate: ['',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
endTemplate: ['',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
code: ['',getFormControlValidation('{"IsRequired":false,"MinLength":"","MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
notes: ['',getFormControlValidation('{"IsRequired":false,"MinLength":"","MaxLength":"500","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

    bfsSystemId: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
bfsComponentId: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

    };
} 
//---------------------------------------------------------
export interface IBfsManualCodeWithLookup extends IBfsManualCode{

    bfsSystemName?: string;
bfsComponentName?: string;

}
//---------------------------------------------------------
export interface IBfsManualCodeRequest extends IEntityRequest<IBfsManualCodeFilter> {}

//---------------------------------------------------------
export interface IBfsManualCodeFilter {
    [key: string]: any;
    Id?: string;

    Name?: string;
FileName?: string;

    BfsSystemId?: string;
BfsComponentId?: string;

}
//---------------------------------------------------------
export function initBfsManualCodeRequest(): IBfsManualCodeRequest {
    let request: IBfsManualCodeRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: BfsManualCodeColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {
            Id: undefined ,

            Name: undefined ,
FileName: undefined ,

            BfsSystemId: undefined ,
BfsComponentId: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------
export function renderBfsManualCode(record: IEntity, column: IQueryColumn): any {
        const value = record[column.fieldName as keyof IEntity];
        switch (column.fieldName) {
            case 'bfsSystemId':
                return record['bfsSystemName']?.toString();
case 'bfsComponentId':
                return record['bfsComponentName']?.toString();

            default:
                return value;
        }
        return value;
    }
    //---------------------------------------------------------
export function getBfsManualCodeActions(component: any, record: IEntity): IAction[] {
        let links: IAction[] = [];

if (component.accessService.isActionAllowed('bfsManualCode', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/mstr/bfs-manual-code/add', displayText: 'Add New record'
});
}
if (component.accessService.isActionAllowed('bfsManualCode', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/mstr/bfs-manual-code/view', displayText: 'View...'
});
}
if (component.accessService.isActionAllowed('bfsManualCode', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/mstr/bfs-manual-code/edit', displayText: 'Edit...' 
});
}
if (component.accessService.isActionAllowed('bfsManualCode', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/mstr/bfs-manual-code/delete', displayText: 'Delete...' 
});
}
if (component.accessService.isActionAllowed('bfsManualCode', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['bfsSystemId'], route:'/mstr/bfs-system/view', displayText:'Go to BfsSystem'
});
}
if (component.accessService.isActionAllowed('bfsManualCode', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['bfsComponentId'], route:'/mstr/bfs-component/view', displayText:'Go to BfsComponent'
});
}

        return links;
    }
    //---------------------------------------------------------

