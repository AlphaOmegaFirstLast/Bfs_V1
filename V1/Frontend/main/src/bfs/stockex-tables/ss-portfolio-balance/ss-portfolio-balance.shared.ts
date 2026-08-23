
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/stockex-main/stockex.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const SsPortfolioBalanceColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false },
{ fieldName: 'name', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:false },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:false },
{ fieldName: 'ssPortfolioId', displayName: ' Portfolio', sortName: 'SsPortfolio_Name', width: '50px', isVisible:true },
{ fieldName: 'balance', displayName: 'Balance', sortName: 'Balance', width: '50px', isVisible:true },

];
//---------------------------------------------------------
export interface ISsPortfolioBalance {
    isDeleted?: boolean;
id?: string;
name?: string;
notes?: string;
balance?: number;

    ssPortfolioId?: string;

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

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function ssPortfolioBalanceUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],
name: [''],
notes: [''],
balance: [0],

    ssPortfolioId: ['0'],

    };
} 
//---------------------------------------------------------
export interface ISsPortfolioBalanceWithLookup extends ISsPortfolioBalance{

    ssPortfolioName?: string;

}
//---------------------------------------------------------
export interface ISsPortfolioBalanceRequest extends IEntityRequest<ISsPortfolioBalanceFilter> {}

//---------------------------------------------------------
export interface ISsPortfolioBalanceFilter {
    [key: string]: any;
    Id?: string;

    Name?: string;

    SsPortfolioId?: string;

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

        return links;
    }
    //---------------------------------------------------------

