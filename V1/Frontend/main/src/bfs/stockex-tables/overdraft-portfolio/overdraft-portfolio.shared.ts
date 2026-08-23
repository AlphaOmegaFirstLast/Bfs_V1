
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/stockex-main/stockex.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const OverdraftPortfolioColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false },
{ fieldName: 'name', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:true },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:false },
{ fieldName: 'ssPortfolioId', displayName: 'StockShare Portfolio', sortName: 'SsPortfolio_Name', width: '50px', isVisible:true },
{ fieldName: 'overdraftValue', displayName: 'Overdraft Value', sortName: 'OverdraftValue', width: '50px', isVisible:false },

];
//---------------------------------------------------------
export interface IOverdraftPortfolio {
    isDeleted?: boolean;
id?: string;
name?: string;
notes?: string;
overdraftValue?: number;

    ssPortfolioId?: string;

}
//---------------------------------------------------------
export function initOverdraftPortfolio(): IOverdraftPortfolio {
    let entity: IOverdraftPortfolio = {
        isDeleted: false,
id: '0',
name: '',
notes: '',
overdraftValue: 0,

        ssPortfolioId: '0',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function overdraftPortfolioUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],
name: [''],
notes: [''],
overdraftValue: [0],

    ssPortfolioId: ['0'],

    };
} 
//---------------------------------------------------------
export interface IOverdraftPortfolioWithLookup extends IOverdraftPortfolio{

    ssPortfolioName?: string;

}
//---------------------------------------------------------
export interface IOverdraftPortfolioRequest extends IEntityRequest<IOverdraftPortfolioFilter> {}

//---------------------------------------------------------
export interface IOverdraftPortfolioFilter {
    [key: string]: any;
    Id?: string;

    Name?: string;

    SsPortfolioId?: string;

    OverdraftValue?: { from?: number ; to?: number} ;

}
//---------------------------------------------------------
export function initOverdraftPortfolioRequest(): IOverdraftPortfolioRequest {
    let request: IOverdraftPortfolioRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: OverdraftPortfolioColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {
            Id: undefined ,

            Name: undefined ,

            SsPortfolioId: undefined ,

            OverdraftValue: { from: undefined , to: undefined} ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

export function getOverdraftPortfolioActions(component: any, record: IEntity): IAction[] {
        let links: IAction[] = [];

if (component.accessService.isActionAllowed('overdraftPortfolio', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/stkx/overdraft-portfolio/add', displayText: 'Add New record'
});
}
if (component.accessService.isActionAllowed('overdraftPortfolio', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/overdraft-portfolio/view', displayText: 'View...'
});
}
if (component.accessService.isActionAllowed('overdraftPortfolio', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/overdraft-portfolio/edit', displayText: 'Edit...' 
});
}
if (component.accessService.isActionAllowed('overdraftPortfolio', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/overdraft-portfolio/delete', displayText: 'Delete...' 
});
}
if (component.accessService.isActionAllowed('overdraftPortfolio', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['ssPortfolioId'], route:'/stkx/ss-portfolio/view', displayText:'Go to SsPortfolio'
});
}

        return links;
    }
    //---------------------------------------------------------

