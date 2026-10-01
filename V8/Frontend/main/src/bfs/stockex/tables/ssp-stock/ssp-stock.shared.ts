import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, ValidationErrors, FormBuilder } from "@angular/forms";

import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
import { getFormControlValidation } from "@bfs/_shared/objectFields";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/stockex/main/stockex.operations';
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

// Output Columns of a Query  [used in entity Query]
export const SspStockColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'name', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'ssPortfolioId', displayName: 'StockShare Portfolio', sortName: 'SsPortfolio_Name', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'quantity', displayName: 'Quantity', sortName: 'Quantity', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'stockShareId', displayName: 'StockShare ', sortName: 'StockShare_Name', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'averageCost', displayName: 'Average Cost', sortName: 'AverageCost', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'currencyId', displayName: 'Currency', sortName: 'Currency_Name', width: '50px', isVisible:true, columnOrder:1 },

];
//---------------------------------------------------------
export interface ISspStock extends IEntity{
    isDeleted?: boolean;
id?: string;
name?: string;
notes?: string;
quantity?: number;
averageCost?: number;

    ssPortfolioId?: string;
stockShareId?: string;
currencyId?: string;

}
//---------------------------------------------------------
export function initSspStock(): ISspStock {
    let entity: ISspStock = {
        isDeleted: false,
id: '0',
name: '',
notes: '',
quantity: 0,
averageCost: 0,

        ssPortfolioId: '0',
stockShareId: '0',
currencyId: '0',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function sspStockUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
id: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
name: ['',getFormControlValidation('{"IsRequired":true,"MinLength":"3","MaxLength":"50","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
notes: ['',getFormControlValidation('{"IsRequired":false,"MinLength":"","MaxLength":"1000","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
quantity: [0,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
averageCost: [0,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

    ssPortfolioId: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
stockShareId: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
currencyId: ['0',getFormControlValidation('{"IsRequired":true,"MinLength":null,"MaxLength":null,"MinValue":"1","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

    };
} 
//---------------------------------------------------------
export interface ISspStockRequest extends IEntityRequest<ISspStockFilter> {}

//---------------------------------------------------------
export interface ISspStockFilter {
    [key: string]: any;
    Id?: string;

    Name?: string;

    SsPortfolioId?: string;
StockShareId?: string;
CurrencyId?: string;

    Quantity?: { from?: number ; to?: number} ;
AverageCost?: { from?: number ; to?: number} ;

}
//---------------------------------------------------------
export function initSspStockRequest(): ISspStockRequest {
    let request: ISspStockRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: SspStockColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {
            Id: undefined ,

            Name: undefined ,

            SsPortfolioId: undefined ,
StockShareId: undefined ,
CurrencyId: undefined ,

            Quantity: { from: undefined , to: undefined} ,
AverageCost: { from: undefined , to: undefined} ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------
export function renderSspStock(record: ISspStock, column: IQueryColumn): any {
        const value = record[column.fieldName as keyof ISspStock];
        switch (column.fieldName) {
            case 'ssPortfolioId':
                return record['ssPortfolioName']?.toString();
case 'stockShareId':
                return record['stockShareName']?.toString();
case 'currencyId':
                return record['currencyName']?.toString();

            default:
                return value;
        }
        return value;
    }
    //---------------------------------------------------------
export function getSspStockActions(component: any, record: ISspStock): IAction[] {
        let links: IAction[] = [];

if (component.accessService.isActionAllowed('sspStock', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/stkx/ssp-stock/add', displayText: 'Add New record'
});
}
if (component.accessService.isActionAllowed('sspStock', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/ssp-stock/view', displayText: 'View...'
});
}
if (component.accessService.isActionAllowed('sspStock', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/ssp-stock/edit', displayText: 'Edit...' 
});
}
if (component.accessService.isActionAllowed('sspStock', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/ssp-stock/delete', displayText: 'Delete...' 
});
}
if (component.accessService.isActionAllowed('sspStock', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['ssPortfolioId'], route:'/stkx/ss-portfolio/view', displayText:'Go to SsPortfolio'
});
}
if (component.accessService.isActionAllowed('sspStock', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['stockShareId'], route:'/stkx/stock-share/view', displayText:'Go to StockShare'
});
}
if (component.accessService.isActionAllowed('sspStock', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['currencyId'], route:'/stkx/currency/view', displayText:'Go to Currency'
});
}

        return links;
    }
//---------------------------------------------------------
export function isVisible(entity: ISspStock, validationForm: UntypedFormGroup, fieldName: string): boolean {
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

