
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
import { getFormControlValidation } from "@bfs/_shared/objectFields";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/master-main/master.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const BusinessActionColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false },
{ fieldName: 'shortName', displayName: 'Short Name', sortName: 'ShortName', width: '50px', isVisible:true },
{ fieldName: 'actionTypeId', displayName: 'Action Type', sortName: 'ActionType_Name', width: '50px', isVisible:true },
{ fieldName: 'writerTypeId', displayName: 'Writer Type', sortName: 'WriterType_Name', width: '50px', isVisible:true },
{ fieldName: 'matchProperty', displayName: 'Writer Matching Property', sortName: 'MatchProperty', width: '50px', isVisible:true },
{ fieldName: 'matchValues', displayName: 'Writer Matching Values', sortName: 'MatchValues', width: '50px', isVisible:true },
{ fieldName: 'actionTemplate', displayName: 'Action Template', sortName: 'ActionTemplate', width: '50px', isVisible:false },
{ fieldName: 'name', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:true },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:false },

];
//---------------------------------------------------------
export interface IBusinessAction {
    isDeleted?: boolean;
id?: string;
shortName?: string;
matchProperty?: string;
matchValues?: string;
actionTemplate?: string;
name?: string;
notes?: string;

    actionTypeId?: number;
writerTypeId?: number;

}
//---------------------------------------------------------
export function initBusinessAction(): IBusinessAction {
    let entity: IBusinessAction = {
        isDeleted: false,
id: '0',
shortName: '',
matchProperty: '',
matchValues: '',
actionTemplate: '',
name: '',
notes: '',

        actionTypeId: 0,
writerTypeId: 0,

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function businessActionUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
id: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
shortName: ['',getFormControlValidation('{"IsRequired":true,"MinLength":"1","MaxLength":"3","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
matchProperty: ['',getFormControlValidation('{"IsRequired":true,"MinLength":"3","MaxLength":"1000","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
matchValues: ['',getFormControlValidation('{"IsRequired":true,"MinLength":"3","MaxLength":"1000","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
actionTemplate: ['',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
name: ['',getFormControlValidation('{"IsRequired":true,"MinLength":"3","MaxLength":"50","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
notes: ['',getFormControlValidation('{"IsRequired":false,"MinLength":"","MaxLength":"1000","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

    actionTypeId: [0,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
writerTypeId: [0,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

    };
} 
//---------------------------------------------------------
export interface IBusinessActionWithLookup extends IBusinessAction{

    actionTypeName?: string;
writerTypeName?: string;

}
//---------------------------------------------------------
export interface IBusinessActionRequest extends IEntityRequest<IBusinessActionFilter> {}

//---------------------------------------------------------
export interface IBusinessActionFilter {
    [key: string]: any;
    Id?: string;

    ShortName?: string;
MatchProperty?: string;
MatchValues?: string;
Name?: string;

    ActionTypeId?: number;
WriterTypeId?: number;

}
//---------------------------------------------------------
export function initBusinessActionRequest(): IBusinessActionRequest {
    let request: IBusinessActionRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: BusinessActionColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {
            Id: undefined ,

            ShortName: undefined ,
MatchProperty: undefined ,
MatchValues: undefined ,
Name: undefined ,

            ActionTypeId: undefined ,
WriterTypeId: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

export function getBusinessActionActions(component: any, record: IEntity): IAction[] {
        let links: IAction[] = [];

if (component.accessService.isActionAllowed('businessAction', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/mstr/business-action/add', displayText: 'Add New record'
});
}
if (component.accessService.isActionAllowed('businessAction', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/mstr/business-action/view', displayText: 'View...'
});
}
if (component.accessService.isActionAllowed('businessAction', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/mstr/business-action/edit', displayText: 'Edit...' 
});
}
if (component.accessService.isActionAllowed('businessAction', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/mstr/business-action/delete', displayText: 'Delete...' 
});
}
if (component.accessService.isActionAllowed('businessAction', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['actionTypeId'], route:'/mstr/action-type/view', displayText:'Go to ActionType'
});
}
if (component.accessService.isActionAllowed('businessAction', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['writerTypeId'], route:'/mstr/writer-type/view', displayText:'Go to WriterType'
});
}

if (component.accessService.isActionAllowed('businessAction', ''))
{links.push({
actionSource:'System', actionType:'FrontendFunction', actionLocation:'ListRow',recordId: record['id'], action: operations.duplicateRecord, displayText: 'Duplicate Record', data: {recordId: record['id'], postUrl:'/BusinessAction', onSuccessMethodName: 'getReport' }
});
}

        return links;
    }
    //---------------------------------------------------------

