
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/stockex-main/stockex.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const InvestorColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false },
{ fieldName: 'name', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:true },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:false },
{ fieldName: 'code', displayName: 'Code', sortName: 'Code', width: '50px', isVisible:false },
{ fieldName: 'email', displayName: 'Email', sortName: 'Email', width: '50px', isVisible:false },

];
//---------------------------------------------------------
export interface IInvestor {
    isDeleted?: boolean;
id?: string;
name?: string;
notes?: string;
code?: string;
email?: string;

}
//---------------------------------------------------------
export function initInvestor(): IInvestor {
    let entity: IInvestor = {
        isDeleted: false,
id: '0',
name: '',
notes: '',
code: '',
email: '',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function investorUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],
name: [''],
notes: [''],
code: [''],
email: [''],

    };
} 
//---------------------------------------------------------
export interface IInvestorWithLookup extends IInvestor{

}
//---------------------------------------------------------
export interface IInvestorRequest extends IEntityRequest<IInvestorFilter> {}

//---------------------------------------------------------
export interface IInvestorFilter {
    [key: string]: any;
    Id?: string;

    Name?: string;

}
//---------------------------------------------------------
export function initInvestorRequest(): IInvestorRequest {
    let request: IInvestorRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: InvestorColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {
            Id: undefined ,

            Name: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

export function getInvestorActions(component: any, record: IEntity): IAction[] {
        let links: IAction[] = [];

if (component.accessService.isActionAllowed('investor', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/stkx/investor/add', displayText: 'Add New record'
});
}
if (component.accessService.isActionAllowed('investor', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/investor/view', displayText: 'View...'
});
}
if (component.accessService.isActionAllowed('investor', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/investor/edit', displayText: 'Edit...' 
});
}
if (component.accessService.isActionAllowed('investor', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/investor/delete', displayText: 'Delete...' 
});
}

        return links;
    }
    //---------------------------------------------------------

