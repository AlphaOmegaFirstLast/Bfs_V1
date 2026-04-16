
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/stores-main/stores.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const TransactionColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false },
{ fieldName: 'quantity', displayName: 'Quantity', sortName: 'QuantityName', width: '50px', isVisible:true },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'NotesName', width: '50px', isVisible:false },
{ fieldName: 'storeId', displayName: 'Store', sortName: 'StoreName', width: '50px', isVisible:true },
{ fieldName: 'operationId', displayName: 'Operation', sortName: 'OperationName', width: '50px', isVisible:true },
{ fieldName: 'productId', displayName: 'Product', sortName: 'ProductName', width: '50px', isVisible:true },

];
//---------------------------------------------------------
export interface ITransaction {
    isDeleted?: boolean;
id?: string;
quantity?: number;
notes?: string;

    storeId?: string;
operationId?: number;
productId?: string;

}
//---------------------------------------------------------
export function initTransaction(): ITransaction {
    let entity: ITransaction = {
        isDeleted: false,
id: '0',
quantity: 0,
notes: '',

        storeId: '0',
operationId: 0,
productId: '0',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function transactionUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],
quantity: [0],
notes: [''],

    storeId: ['0'],
operationId: [0],
productId: ['0'],

    };
} 
//---------------------------------------------------------
export interface ITransactionWithLookup extends ITransaction{

    storeName?: string;
operationName?: string;
productName?: string;

}
//---------------------------------------------------------
export interface ITransactionRequest extends IEntityRequest<ITransactionFilter> {}

//---------------------------------------------------------
export interface ITransactionFilter {
    [key: string]: any;

    StoreId?: string;
OperationId?: number;
ProductId?: string;

    Quantity?: { from?: number ; to?: number} ;

}
//---------------------------------------------------------
export function initTransactionRequest(): ITransactionRequest {
    let request: ITransactionRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: TransactionColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {

            StoreId: undefined ,
OperationId: undefined ,
ProductId: undefined ,

            Quantity: { from: undefined , to: undefined} ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

export function getTransactionActions(component: any, record: IEntity): IAction[] {
        let links: IAction[] = [];

        return links;
    }
    //---------------------------------------------------------

