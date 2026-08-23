
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/stockex-main/stockex.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const TransactionTypeColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false },
{ fieldName: 'name', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:true },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:false },
{ fieldName: 'effectTypeId', displayName: 'Effect Type', sortName: 'EffectType_Name', width: '50px', isVisible:true },
{ fieldName: 'stockEntityTypeId', displayName: 'Applicable To Entity', sortName: 'StockEntityType_Name', width: '50px', isVisible:true },
{ fieldName: 'calculationMethodId', displayName: 'Calculation Method', sortName: 'CalculationMethod_Name', width: '50px', isVisible:true },
{ fieldName: 'sourceTypeId', displayName: 'Source Type', sortName: 'SourceType_Name', width: '50px', isVisible:true },
{ fieldName: 'stockFieldTypeId', displayName: 'Applicable To Field', sortName: 'StockFieldType_Name', width: '50px', isVisible:true },
{ fieldName: 'nextTransactionTypeId', displayName: 'Next Transaction Type', sortName: 'NextTransactionType_Name', width: '50px', isVisible:true },

];
//---------------------------------------------------------
export interface ITransactionType {
    isDeleted?: boolean;
id?: string;
name?: string;
notes?: string;

    effectTypeId?: number;
stockEntityTypeId?: number;
calculationMethodId?: number;
sourceTypeId?: number;
stockFieldTypeId?: number;
nextTransactionTypeId?: number;

}
//---------------------------------------------------------
export function initTransactionType(): ITransactionType {
    let entity: ITransactionType = {
        isDeleted: false,
id: '0',
name: '',
notes: '',

        effectTypeId: 0,
stockEntityTypeId: 0,
calculationMethodId: 0,
sourceTypeId: 0,
stockFieldTypeId: 0,
nextTransactionTypeId: 0,

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function transactionTypeUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],
name: [''],
notes: [''],

    effectTypeId: [0],
stockEntityTypeId: [0],
calculationMethodId: [0],
sourceTypeId: [0],
stockFieldTypeId: [0],
nextTransactionTypeId: [0],

    };
} 
//---------------------------------------------------------
export interface ITransactionTypeWithLookup extends ITransactionType{

    effectTypeName?: string;
stockEntityTypeName?: string;
calculationMethodName?: string;
sourceTypeName?: string;
stockFieldTypeName?: string;
nextTransactionTypeName?: string;

}
//---------------------------------------------------------
export interface ITransactionTypeRequest extends IEntityRequest<ITransactionTypeFilter> {}

//---------------------------------------------------------
export interface ITransactionTypeFilter {
    [key: string]: any;
    Id?: string;

    Name?: string;

    EffectTypeId?: number;
StockEntityTypeId?: number;
CalculationMethodId?: number;
SourceTypeId?: number;
StockFieldTypeId?: number;
NextTransactionTypeId?: number;

}
//---------------------------------------------------------
export function initTransactionTypeRequest(): ITransactionTypeRequest {
    let request: ITransactionTypeRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: TransactionTypeColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {
            Id: undefined ,

            Name: undefined ,

            EffectTypeId: undefined ,
StockEntityTypeId: undefined ,
CalculationMethodId: undefined ,
SourceTypeId: undefined ,
StockFieldTypeId: undefined ,
NextTransactionTypeId: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

export function getTransactionTypeActions(component: any, record: IEntity): IAction[] {
        let links: IAction[] = [];

if (component.accessService.isActionAllowed('transactionType', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/stkx/transaction-type/add', displayText: 'Add New record'
});
}
if (component.accessService.isActionAllowed('transactionType', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/transaction-type/view', displayText: 'View...'
});
}
if (component.accessService.isActionAllowed('transactionType', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/transaction-type/edit', displayText: 'Edit...' 
});
}
if (component.accessService.isActionAllowed('transactionType', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/transaction-type/delete', displayText: 'Delete...' 
});
}
if (component.accessService.isActionAllowed('transactionType', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['effectTypeId'], route:'/stkx/effect-type/view', displayText:'Go to EffectType'
});
}
if (component.accessService.isActionAllowed('transactionType', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['stockEntityTypeId'], route:'/stkx/stock-entity-type/view', displayText:'Go to StockEntityType'
});
}
if (component.accessService.isActionAllowed('transactionType', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['calculationMethodId'], route:'/stkx/calculation-method/view', displayText:'Go to CalculationMethod'
});
}
if (component.accessService.isActionAllowed('transactionType', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['sourceTypeId'], route:'/stkx/source-type/view', displayText:'Go to SourceType'
});
}
if (component.accessService.isActionAllowed('transactionType', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['stockFieldTypeId'], route:'/stkx/stock-field-type/view', displayText:'Go to StockFieldType'
});
}
if (component.accessService.isActionAllowed('transactionType', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['nextTransactionTypeId'], route:'/stkx/next-transaction-type/view', displayText:'Go to NextTransactionType'
});
}

        return links;
    }
    //---------------------------------------------------------

