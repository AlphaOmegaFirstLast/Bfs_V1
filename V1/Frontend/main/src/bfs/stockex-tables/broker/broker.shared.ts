
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/stockex-main/stockex.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const BrokerColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible: false },
    { fieldName: 'name', displayName: 'Name', sortName: 'Name', width: '50px', isVisible: true },
    { fieldName: 'notes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible: false },
    { fieldName: 'code', displayName: 'Code', sortName: 'Code', width: '50px', isVisible: false },
    { fieldName: 'email', displayName: 'Email', sortName: 'Email', width: '50px', isVisible: false },
    { fieldName: 'tradingRoomId', displayName: 'Trading Room', sortName: 'TradingRoom_Name', width: '50px', isVisible: true },

];
//---------------------------------------------------------
export interface IBroker {
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
        isDeleted: [false],
        id: ['0'],
        name: [''],
        notes: [''],
        code: [''],
        email: [''],
        tradingRoomId: ['0'],
    };
}
//---------------------------------------------------------
export interface IBrokerWithLookup extends IBroker {
    tradingRoomName?: string;
}
//---------------------------------------------------------
export interface IBrokerRequest extends IEntityRequest<IBrokerFilter> { }

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
            Id: undefined,

            Name: undefined,

            TradingRoomId: undefined,

        }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

export function getBrokerActions(component: any, record: IEntity): IAction[] {
    let links: IAction[] = [];

    if (component.accessService.isActionAllowed('broker', '')) {
        links.push({
            actionSource: 'System', actionType: 'FrontendLink', actionLocation: 'ListHeader', recordId: 0, route: '/stkx/broker/add', displayText: 'Add New record'
        });
    }
    if (component.accessService.isActionAllowed('broker', '')) {
        links.push({
            actionSource: 'System', actionType: 'FrontendLink', actionLocation: 'ListRow', recordId: record['id'], route: '/stkx/broker/view', displayText: 'View...'
        });
    }
    if (component.accessService.isActionAllowed('broker', '')) {
        links.push({
            actionSource: 'System', actionType: 'FrontendLink', actionLocation: 'ListRow', recordId: record['id'], route: '/stkx/broker/edit', displayText: 'Edit...'
        });
    }
    if (component.accessService.isActionAllowed('broker', '')) {
        links.push({
            actionSource: 'System', actionType: 'FrontendLink', actionLocation: 'ListRow', recordId: record['id'], route: '/stkx/broker/delete', displayText: 'Delete...'
        });
    }
    if (component.accessService.isActionAllowed('broker', '')) {
        links.push({
            actionSource: 'System', actionType: 'FrontendLink', actionLocation: 'ListRow', recordId: record['tradingRoomId'], route: '/stkx/trading-room/view', displayText: 'Go to TradingRoom'
        });
    }

    return links;
}
//---------------------------------------------------------

