
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/stockex-main/stockex.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const BrokerColumns = [
    { fieldName: 'id', displayName: 'Id', sortName: 'Id', width: '50px', isVisible:true },
{ fieldName: 'isDeleted', displayName: 'IsDeleted', sortName: 'IsDeleted', width: '50px', isVisible:true },
{ fieldName: 'code', displayName: 'Code', sortName: 'Code', width: '50px', isVisible:true },
{ fieldName: 'name', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:true },
{ fieldName: 'tradingRoomId', displayName: 'Trading Room', sortName: 'TradingRoom_Name', width: '50px', isVisible:true },

];
//---------------------------------------------------------
export interface IBroker {
    id?: string;
isDeleted?: boolean;
code?: string;
name?: string;

    tradingRoomId?: string;

}
//---------------------------------------------------------
export function initBroker(): IBroker {
    let entity: IBroker = {
        id: '0',
isDeleted: false,
code: '',
name: '',

        tradingRoomId: '0',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function brokerUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    id: ['0'],
isDeleted: [false],
code: [''],
name: [''],

    tradingRoomId: ['0'],

    };
} 
//---------------------------------------------------------
export interface IBrokerWithLookup extends IBroker{

    tradingRoomName?: string;

}
//---------------------------------------------------------
export interface IBrokerRequest extends IEntityRequest<IBrokerFilter> {}

//---------------------------------------------------------
export interface IBrokerFilter {
    [key: string]: any;

    Code?: string;
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

            Code: undefined ,
Name: undefined ,

            TradingRoomId: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

export function getBrokerActions(component: any, record: IEntity): IAction[] {
        let links: IAction[] = [];

        return links;
    }
    //---------------------------------------------------------

