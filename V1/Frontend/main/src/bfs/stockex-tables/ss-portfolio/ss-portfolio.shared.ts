
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
import { getFormControlValidation } from "@bfs/_shared/objectFields";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/stockex-main/stockex.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const SsPortfolioColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible: false },
    { fieldName: 'name', displayName: 'Name', sortName: 'Name', width: '50px', isVisible: true },
    { fieldName: 'notes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible: false },
    { fieldName: 'brokerId', displayName: 'Broker', sortName: 'Broker_Name', width: '50px', isVisible: true },
    { fieldName: 'investorId', displayName: 'Investor', sortName: 'Investor_Name', width: '50px', isVisible: true },

];
//---------------------------------------------------------
export interface ISsPortfolio {
    isDeleted?: boolean;
    id?: string;
    name?: string;
    notes?: string;

    brokerId?: string;
    brokerName?: string;
    investorId?: string;
    investorName?: string;

}
//---------------------------------------------------------
export function initSsPortfolio(): ISsPortfolio {
    let entity: ISsPortfolio = {
        isDeleted: false,
        id: '0',
        name: '',
        notes: '',

        brokerId: '0',
        brokerName: '',
        investorId: '0',
        investorName: '',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function ssPortfolioUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
        isDeleted: [false, getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
        id: ['0', getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
        name: ['', getFormControlValidation('{"IsRequired":true,"MinLength":"3","MaxLength":"50","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
        notes: ['', getFormControlValidation('{"IsRequired":false,"MinLength":"","MaxLength":"1000","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

        brokerId: ['0', getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
        brokerName: [''],
        investorId: ['0', getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
        investorName: [''],

    };
}
//---------------------------------------------------------
export interface ISsPortfolioWithLookup extends ISsPortfolio {

    brokerName?: string;
    investorName?: string;

}
//---------------------------------------------------------
export interface ISsPortfolioRequest extends IEntityRequest<ISsPortfolioFilter> { }

//---------------------------------------------------------
export interface ISsPortfolioFilter {
    [key: string]: any;
    Id?: string;

    Name?: string;

    BrokerId?: string;
    InvestorId?: string;

}
//---------------------------------------------------------
export function initSsPortfolioRequest(): ISsPortfolioRequest {
    let request: ISsPortfolioRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: SsPortfolioColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
        },
        filter: {
            Id: undefined,

            Name: undefined,

            BrokerId: undefined,
            InvestorId: undefined,

        }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

export function getSsPortfolioActions(component: any, record: IEntity): IAction[] {
    let links: IAction[] = [];

    if (component.accessService.isActionAllowed('ssPortfolio', '')) {
        links.push({
            actionSource: 'System', actionType: 'FrontendLink', actionLocation: 'ListHeader', recordId: 0, route: '/stkx/ss-portfolio/add', displayText: 'Add New record'
        });
    }
    if (component.accessService.isActionAllowed('ssPortfolio', '')) {
        links.push({
            actionSource: 'System', actionType: 'FrontendLink', actionLocation: 'ListRow', recordId: record['id'], route: '/stkx/ss-portfolio/view', displayText: 'View...'
        });
    }
    if (component.accessService.isActionAllowed('ssPortfolio', '')) {
        links.push({
            actionSource: 'System', actionType: 'FrontendLink', actionLocation: 'ListRow', recordId: record['id'], route: '/stkx/ss-portfolio/edit', displayText: 'Edit...'
        });
    }
    if (component.accessService.isActionAllowed('ssPortfolio', '')) {
        links.push({
            actionSource: 'System', actionType: 'FrontendLink', actionLocation: 'ListRow', recordId: record['id'], route: '/stkx/ss-portfolio/delete', displayText: 'Delete...'
        });
    }

    return links;
}
//---------------------------------------------------------

