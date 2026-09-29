import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, ValidationErrors, FormBuilder } from "@angular/forms";

import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
import { getFormControlValidation } from "@bfs/_shared/objectFields";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/stockex/main/stockex.operations';
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

// Output Columns of a Query  [used in entity Query]
export const StockShareColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'name', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'tradingRoomId', displayName: 'Trading Room', sortName: 'TradingRoom_Name', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'currencyId', displayName: 'Currency', sortName: 'Currency_Name', width: '50px', isVisible:true, columnOrder:1 },

];
//---------------------------------------------------------
export interface IStockShare extends IEntity{
    isDeleted?: boolean;
id?: string;
name?: string;
notes?: string;

    tradingRoomId?: string;
currencyId?: string;

}
//---------------------------------------------------------
export function initStockShare(): IStockShare {
    let entity: IStockShare = {
        isDeleted: false,
id: '0',
name: '',
notes: '',

        tradingRoomId: '0',
currencyId: '0',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function stockShareUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
id: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
name: ['',getFormControlValidation('{"IsRequired":true,"MinLength":"3","MaxLength":"50","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
notes: ['',getFormControlValidation('{"IsRequired":false,"MinLength":"","MaxLength":"1000","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

    tradingRoomId: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
currencyId: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

    };
} 
//---------------------------------------------------------
export interface IStockShareRequest extends IEntityRequest<IStockShareFilter> {}

//---------------------------------------------------------
export interface IStockShareFilter {
    [key: string]: any;
    Id?: string;

    Name?: string;

    TradingRoomId?: string;
CurrencyId?: string;

}
//---------------------------------------------------------
export function initStockShareRequest(): IStockShareRequest {
    let request: IStockShareRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: StockShareColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {
            Id: undefined ,

            Name: undefined ,

            TradingRoomId: undefined ,
CurrencyId: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------
export function renderStockShare(record: IStockShare, column: IQueryColumn): any {
        const value = record[column.fieldName as keyof IStockShare];
        switch (column.fieldName) {
            case 'tradingRoomId':
                return record['tradingRoomName']?.toString();
case 'currencyId':
                return record['currencyName']?.toString();

            default:
                return value;
        }
        return value;
    }
    //---------------------------------------------------------
export function getStockShareActions(component: any, record: IStockShare): IAction[] {
        let links: IAction[] = [];

if (component.accessService.isActionAllowed('stockShare', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/stkx/stock-share/add', displayText: 'Add New record'
});
}
if (component.accessService.isActionAllowed('stockShare', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/stock-share/view', displayText: 'View...'
});
}
if (component.accessService.isActionAllowed('stockShare', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/stock-share/edit', displayText: 'Edit...' 
});
}
if (component.accessService.isActionAllowed('stockShare', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/stock-share/delete', displayText: 'Delete...' 
});
}
if (component.accessService.isActionAllowed('stockShare', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['tradingRoomId'], route:'/stkx/trading-room/view', displayText:'Go to TradingRoom'
});
}
if (component.accessService.isActionAllowed('stockShare', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['currencyId'], route:'/stkx/currency/view', displayText:'Go to Currency'
});
}

        return links;
    }
//---------------------------------------------------------
export function isVisible(entity: IStockShare, validationForm: UntypedFormGroup, fieldName: string): boolean {
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

