
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/infrastructure-main/infrastructure.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const StrTransactionColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:true },
{ fieldName: 'quantity', displayName: 'Quantity', sortName: 'Quantity', width: '50px', isVisible:true },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:false },
{ fieldName: 'strStoreId', displayName: 'Store', sortName: 'StrStore', width: '50px', isVisible:true },
{ fieldName: 'strOperationId', displayName: 'Operation', sortName: 'StrOperation', width: '50px', isVisible:true },
{ fieldName: 'strProductId', displayName: 'Product', sortName: 'StrProduct', width: '50px', isVisible:true },

];
//---------------------------------------------------------
export interface IStrTransaction {
    isDeleted?: boolean;
id?: string;
quantity?: number;
notes?: string;

    strStoreId?: string;
strOperationId?: number;
strProductId?: string;

}
//---------------------------------------------------------
export function initStrTransaction(): IStrTransaction {
    let entity: IStrTransaction = {
        isDeleted: false,
id: '0',
quantity: 0,
notes: '',

        strStoreId: '0',
strOperationId: 0,
strProductId: '0',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function strTransactionUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],
quantity: [0],
notes: [''],

    strStoreId: ['0'],
strOperationId: [0],
strProductId: ['0'],

    };
} 
//---------------------------------------------------------
export interface IStrTransactionWithLookup extends IStrTransaction{

    strStoreName?: string;
strOperationName?: string;
strProductName?: string;

}
//---------------------------------------------------------
export interface IStrTransactionRequest extends IEntityRequest<IStrTransactionFilter> {}

//---------------------------------------------------------
export interface IStrTransactionFilter {
    [key: string]: any;

    StrStoreId?: string;
StrOperationId?: number;
StrProductId?: string;

    Quantity?: { from?: number ; to?: number} ;

}
//---------------------------------------------------------
export function initStrTransactionRequest(): IStrTransactionRequest {
    let request: IStrTransactionRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: StrTransactionColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {

            StrStoreId: undefined ,
StrOperationId: undefined ,
StrProductId: undefined ,

            Quantity: { from: undefined , to: undefined} ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

export function getStrTransactionActions(component: any, record: IEntity): IAction[] {
        let links: IAction[] = [];

        return links;
    }
    //---------------------------------------------------------

