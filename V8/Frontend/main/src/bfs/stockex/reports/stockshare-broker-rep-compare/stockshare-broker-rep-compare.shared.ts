import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
import { reOrderColumns } from "@bfs/_shared/helpers/misc.helper"
import * as operations from '@bfs/stockex/main/stockex.operations';

// Output Columns of a Query  [used in entity Query]
export const columns = [
    { fieldName: 'stockshare_Id', displayName: 'ID', sortName: 'Stockshare_Id', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'stockshare_Name', displayName: 'Name', sortName: 'Stockshare_Name', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'stockShare_TradingRoomId', displayName: 'TradingRoomId', sortName: 'TradingRoomName', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'tradingRoom_BrokerId', displayName: 'BrokerId', sortName: 'BrokerName', width: '50px', isVisible:true, columnOrder:1 },

];
export const StockshareBrokerRepCompareColumns  = reOrderColumns(columns);
//---------------------------------------------------------
export interface IStockshareBrokerRepCompare {
    [key: string]: any;
    stockshare_Id?: string;
stockshare_Name?: string;
stockShare_TradingRoomId?: string;
tradingRoom_BrokerId?: string;

}
//---------------------------------------------------------
export interface IStockshareBrokerRepCompareFilter {
    [key: string]: any;
    Stockshare_Id?: string;

    Stockshare_Name?: string;

    StockShare_TradingRoomId?: string;
TradingRoom_BrokerId?: string;

}
//---------------------------------------------------------

export interface IStockshareBrokerRepCompareRequest extends IEntityRequest<IStockshareBrokerRepCompareFilter> {}

//---------------------------------------------------------
export function initStockshareBrokerRepCompareRequest(): IStockshareBrokerRepCompareRequest {
    let request: IStockshareBrokerRepCompareRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: StockshareBrokerRepCompareColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {},
        filter: {
            Stockshare_Id: undefined ,

            Stockshare_Name: undefined ,

            StockShare_TradingRoomId: undefined ,
TradingRoom_BrokerId: undefined ,

            }
    };
    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------
export function renderStockshareBrokerRepCompare(record: IEntity, column: IQueryColumn): any {
        const value = record[column.fieldName as keyof IEntity];
        switch (column.fieldName) {
            case 'stockShare_TradingRoomId':
                return record['tradingRoomName']?.toString();
case 'tradingRoom_BrokerId':
                return record['brokerName']?.toString();

            default:
                return value;
        }
        return value;
    }
    //---------------------------------------------------------
export function getStockshareBrokerRepCompareActions(component:any, record: IEntity): IAction[] {
        let links: IAction[] = [];

        return links;
    }
    //---------------------------------------------------------

