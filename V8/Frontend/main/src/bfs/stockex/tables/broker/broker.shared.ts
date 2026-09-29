import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, ValidationErrors, FormBuilder } from "@angular/forms";

import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
import { getFormControlValidation } from "@bfs/_shared/objectFields";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/stockex/main/stockex.operations';
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

// Output Columns of a Query  [used in entity Query]
export const BrokerColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'name', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'code', displayName: 'Code', sortName: 'Code', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'email', displayName: 'Email', sortName: 'Email', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'tradingRoomId', displayName: 'Trading Room', sortName: 'TradingRoom_Name', width: '50px', isVisible:true, columnOrder:1 },

];
//---------------------------------------------------------
export interface IBroker extends IEntity{
    isDeleted?: boolean;
id?: string;
name?: string;
notes?: string;
code?: string;
email?: string;

    tradingRoomId?: string;

}
//---------------------------------------------------------
export function initBroker(): IBroker {
    let entity: IBroker = {
        isDeleted: false,
id: '0',
name: '',
notes: '',
code: '',
email: '',

        tradingRoomId: '0',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function brokerUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
id: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
name: ['',getFormControlValidation('{"IsRequired":true,"MinLength":"3","MaxLength":"50","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
notes: ['',getFormControlValidation('{"IsRequired":false,"MinLength":"","MaxLength":"1000","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
code: ['',getFormControlValidation('{"IsRequired":true,"MinLength":"","MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
email: ['',getFormControlValidation('{"IsRequired":false,"MinLength":"1","MaxLength":"100","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

    tradingRoomId: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

    };
} 
//---------------------------------------------------------
export interface IBrokerRequest extends IEntityRequest<IBrokerFilter> {}

//---------------------------------------------------------
export interface IBrokerFilter {
    [key: string]: any;
    Id?: string;

    Name?: string;

    TradingRoomId?: string;

}
//---------------------------------------------------------
export function initBrokerRequest(): IBrokerRequest {
    let request: IBrokerRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: BrokerColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {
            Id: undefined ,

            Name: undefined ,

            TradingRoomId: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------
export function renderBroker(record: IBroker, column: IQueryColumn): any {
        const value = record[column.fieldName as keyof IBroker];
        switch (column.fieldName) {
            case 'tradingRoomId':
                return record['tradingRoomName']?.toString();

            default:
                return value;
        }
        return value;
    }
    //---------------------------------------------------------
export function getBrokerActions(component: any, record: IBroker): IAction[] {
        let links: IAction[] = [];

if (component.accessService.isActionAllowed('broker', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/stkx/broker/add', displayText: 'Add New record'
});
}
if (component.accessService.isActionAllowed('broker', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/broker/view', displayText: 'View...'
});
}
if (component.accessService.isActionAllowed('broker', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/broker/edit', displayText: 'Edit...' 
});
}
if (component.accessService.isActionAllowed('broker', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/broker/delete', displayText: 'Delete...' 
});
}
if (component.accessService.isActionAllowed('broker', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['tradingRoomId'], route:'/stkx/trading-room/view', displayText:'Go to TradingRoom'
});
}

        return links;
    }
//---------------------------------------------------------
export function isVisible(entity: IBroker, validationForm: UntypedFormGroup, fieldName: string): boolean {
    //check using const control = this.validationForm.get(fieldName);
    // or this.entity[fieldName]
    switch (fieldName) {
        case 'exampleId':
           //example=>  return (entity['transactionTypeId'] == 2 || entity['transactionTypeId'] == 3) ? true : false;
           return true;
    }
    return true;
}
//---------------------------------------------------------
export function isEnabled(entity: IEntity, validationForm: UntypedFormGroup, fieldName: string): boolean {
    //check using const control = this.validationForm.get(fieldName);
    // or this.entity[fieldName]
    switch (fieldName) {
        case 'exampleId':
           //example=>  return (entity['transactionTypeId'] == 2 || entity['transactionTypeId'] == 3) ? true : false;
           return true;
    }
    return true;
}

//Template_Start_Code_DontOverwrite_3
//Template_End_Code_DontOverwrite_3

