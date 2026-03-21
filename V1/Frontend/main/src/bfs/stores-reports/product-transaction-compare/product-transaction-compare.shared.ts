import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
import * as operations from '@bfs/infrastructure-main/infrastructure.operations';

// Output Columns of a Query  [used in entity Query]
export const ProductTransactionCompareColumns = [
    { fieldName: 'strProduct_Name', displayName: 'Product Name', sortName: 'Name', width: '50px', isVisible:true },

    { fieldName: 'sumQuantity', displayName: 'Sum of Quantity', sortName: 'sumQuantity', width: '50px', isVisible:true },

];

//---------------------------------------------------------

export interface IProductTransactionCompare {
    strProduct_Name?: string;

}
//---------------------------------------------------------
export interface IProductTransactionCompareWithLookup extends IProductTransactionCompare{

    sumQuantity?:number;

}
//---------------------------------------------------------
export interface IProductTransactionCompareFilter {
    [key: string]: any;

    Quantity?: string;
Name?: string;

}
//---------------------------------------------------------

export interface IProductTransactionCompareRequest extends IEntityRequest<IProductTransactionCompareFilter> {}

//---------------------------------------------------------
export function initProductTransactionCompareRequest(): IProductTransactionCompareRequest {
    let request: IProductTransactionCompareRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: ProductTransactionCompareColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {},
        filter: {

            Quantity: undefined ,
Name: undefined ,

            }
    };
    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------
export function getProductTransactionCompareActions(component:any, record: IEntity): IAction[] {
        let links: IAction[] = [];

        return links;
    }
    //---------------------------------------------------------

//Template_Start_Code_DontOverwrite_1

//Template_End_Code_DontOverwrite_1

