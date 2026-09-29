import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, ValidationErrors, FormBuilder } from "@angular/forms";

import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
import { getFormControlValidation } from "@bfs/_shared/objectFields";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/stockex/main/stockex.operations';
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

// Output Columns of a Query  [used in entity Query]
export const CurrentPriceColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'stockShareId', displayName: 'Stock Share', sortName: 'StockShare_Name', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'transactionDate', displayName: 'Transaction Date', sortName: 'TransactionDate', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'price', displayName: 'Price', sortName: 'Price', width: '50px', isVisible:false, columnOrder:1 },

];
//---------------------------------------------------------
export interface ICurrentPrice extends IEntity{
    isDeleted?: boolean;
id?: string;
name?: string;
notes?: string;
transactionDate?: Date | null;
price?: number;

    stockShareId?: string;

}
//---------------------------------------------------------
export function initCurrentPrice(): ICurrentPrice {
    let entity: ICurrentPrice = {
        isDeleted: false,
id: '0',
name: '',
notes: '',
transactionDate: new Date(0),
price: 0,

        stockShareId: '0',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function currentPriceUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
id: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
name: ['',getFormControlValidation('{"IsRequired":false,"MinLength":"0","MaxLength":"0","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
notes: ['',getFormControlValidation('{"IsRequired":false,"MinLength":"","MaxLength":"0","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
transactionDate: [new Date(0),getFormControlValidation('{"IsRequired":false,"MinLength":"","MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
price: [0,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

    stockShareId: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

    };
} 
//---------------------------------------------------------
export interface ICurrentPriceRequest extends IEntityRequest<ICurrentPriceFilter> {}

//---------------------------------------------------------
export interface ICurrentPriceFilter {
    [key: string]: any;
    Id?: string;

    Name?: string;

    StockShareId?: string;

    TransactionDate?: { from?: Date | null ; to?: Date | null} ;
Price?: { from?: number ; to?: number} ;

}
//---------------------------------------------------------
export function initCurrentPriceRequest(): ICurrentPriceRequest {
    let request: ICurrentPriceRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: CurrentPriceColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {
            Id: undefined ,

            Name: undefined ,

            StockShareId: undefined ,

            TransactionDate: { from: undefined , to: undefined} ,
Price: { from: undefined , to: undefined} ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------
export function renderCurrentPrice(record: ICurrentPrice, column: IQueryColumn): any {
        const value = record[column.fieldName as keyof ICurrentPrice];
        switch (column.fieldName) {
            case 'stockShareId':
                return record['stockShareName']?.toString();

            default:
                return value;
        }
        return value;
    }
    //---------------------------------------------------------
export function getCurrentPriceActions(component: any, record: ICurrentPrice): IAction[] {
        let links: IAction[] = [];

if (component.accessService.isActionAllowed('currentPrice', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/stkx/current-price/add', displayText: 'Add New record'
});
}
if (component.accessService.isActionAllowed('currentPrice', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/current-price/view', displayText: 'View...'
});
}
if (component.accessService.isActionAllowed('currentPrice', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/current-price/edit', displayText: 'Edit...' 
});
}
if (component.accessService.isActionAllowed('currentPrice', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/current-price/delete', displayText: 'Delete...' 
});
}
if (component.accessService.isActionAllowed('currentPrice', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['stockShareId'], route:'/stkx/stock-share/view', displayText:'Go to StockShare'
});
}

        return links;
    }
//---------------------------------------------------------
export function isVisible(entity: ICurrentPrice, validationForm: UntypedFormGroup, fieldName: string): boolean {
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

