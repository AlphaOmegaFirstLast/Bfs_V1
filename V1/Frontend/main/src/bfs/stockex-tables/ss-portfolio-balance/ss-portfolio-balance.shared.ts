
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
import { getFormControlValidation } from "@bfs/_shared/objectFields";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/stockex-main/stockex.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const SsPortfolioBalanceColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'name', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'ssPortfolioId', displayName: ' Portfolio', sortName: 'SsPortfolio_Name', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'balance', displayName: 'Balance', sortName: 'Balance', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'currencyId', displayName: 'Currency', sortName: 'Currency_Name', width: '50px', isVisible:true, columnOrder:1 },

];
//---------------------------------------------------------
export interface ISsPortfolioBalance {
    isDeleted?: boolean;
id?: string;
name?: string;
notes?: string;
balance?: number;

    ssPortfolioId?: string;
currencyId?: string;

}
//---------------------------------------------------------
export function initSsPortfolioBalance(): ISsPortfolioBalance {
    let entity: ISsPortfolioBalance = {
        isDeleted: false,
id: '0',
name: '',
notes: '',
balance: 0,

        ssPortfolioId: '0',
currencyId: '0',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function ssPortfolioBalanceUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
id: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
name: ['',getFormControlValidation('{"IsRequired":true,"MinLength":"3","MaxLength":"50","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
notes: ['',getFormControlValidation('{"IsRequired":false,"MinLength":"","MaxLength":"1000","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
balance: [0,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

    ssPortfolioId: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
currencyId: ['0',getFormControlValidation('{"IsRequired":true,"MinLength":"","MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

    };
} 
//---------------------------------------------------------
export interface ISsPortfolioBalanceWithLookup extends ISsPortfolioBalance{

    ssPortfolioName?: string;
currencyName?: string;

}
//---------------------------------------------------------
export interface ISsPortfolioBalanceRequest extends IEntityRequest<ISsPortfolioBalanceFilter> {}

//---------------------------------------------------------
export interface ISsPortfolioBalanceFilter {
    [key: string]: any;
    Id?: string;

    Name?: string;

    SsPortfolioId?: string;
CurrencyId?: string;

    Balance?: { from?: number ; to?: number} ;

}
//---------------------------------------------------------
export function initSsPortfolioBalanceRequest(): ISsPortfolioBalanceRequest {
    let request: ISsPortfolioBalanceRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: SsPortfolioBalanceColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {
            Id: undefined ,

            Name: undefined ,

            SsPortfolioId: undefined ,
CurrencyId: undefined ,

            Balance: { from: undefined , to: undefined} ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

export function getSsPortfolioBalanceActions(component: any, record: IEntity): IAction[] {
        let links: IAction[] = [];

if (component.accessService.isActionAllowed('ssPortfolioBalance', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/stkx/ss-portfolio-balance/add', displayText: 'Add New record'
});
}
if (component.accessService.isActionAllowed('ssPortfolioBalance', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/ss-portfolio-balance/view', displayText: 'View...'
});
}
if (component.accessService.isActionAllowed('ssPortfolioBalance', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/ss-portfolio-balance/edit', displayText: 'Edit...' 
});
}
if (component.accessService.isActionAllowed('ssPortfolioBalance', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/ss-portfolio-balance/delete', displayText: 'Delete...' 
});
}
if (component.accessService.isActionAllowed('ssPortfolioBalance', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['ssPortfolioId'], route:'/stkx/ss-portfolio/view', displayText:'Go to SsPortfolio'
});
}
if (component.accessService.isActionAllowed('ssPortfolioBalance', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['currencyId'], route:'/stkx/currency/view', displayText:'Go to Currency'
});
}

        return links;
    }
    //---------------------------------------------------------

