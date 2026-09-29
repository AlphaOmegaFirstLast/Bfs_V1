import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, ValidationErrors, FormBuilder } from "@angular/forms";

import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
import { getFormControlValidation } from "@bfs/_shared/objectFields";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/stockex/main/stockex.operations';
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

// Output Columns of a Query  [used in entity Query]
export const TransactionTypeColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'name', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'effectTypeId', displayName: 'Effect Type', sortName: 'EffectType_Name', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'stockEntityTypeId', displayName: 'Applicable To Entity', sortName: 'StockEntityType_Name', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'calculationMethodId', displayName: 'Calculation Method', sortName: 'CalculationMethod_Name', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'sourceTypeId', displayName: 'Source Type', sortName: 'SourceType_Name', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'stockFieldTypeId', displayName: 'Applicable To Field', sortName: 'StockFieldType_Name', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'nextTransactionTypeId', displayName: 'Next Transaction Type', sortName: 'NextTransactionType_Name', width: '50px', isVisible:true, columnOrder:1 },

];
//---------------------------------------------------------
export interface ITransactionType extends IEntity{
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
    isDeleted: [false,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
id: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
name: ['',getFormControlValidation('{"IsRequired":true,"MinLength":"3","MaxLength":"50","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
notes: ['',getFormControlValidation('{"IsRequired":false,"MinLength":"","MaxLength":"1000","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

    effectTypeId: [0,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
stockEntityTypeId: [0,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
calculationMethodId: [0,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
sourceTypeId: [0,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
stockFieldTypeId: [0,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
nextTransactionTypeId: [0,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

    };
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
export function renderTransactionType(record: ITransactionType, column: IQueryColumn): any {
        const value = record[column.fieldName as keyof ITransactionType];
        switch (column.fieldName) {
            case 'effectTypeId':
                return record['effectTypeName']?.toString();
case 'stockEntityTypeId':
                return record['stockEntityTypeName']?.toString();
case 'calculationMethodId':
                return record['calculationMethodName']?.toString();
case 'sourceTypeId':
                return record['sourceTypeName']?.toString();
case 'stockFieldTypeId':
                return record['stockFieldTypeName']?.toString();
case 'nextTransactionTypeId':
                return record['nextTransactionTypeName']?.toString();

            default:
                return value;
        }
        return value;
    }
    //---------------------------------------------------------
export function getTransactionTypeActions(component: any, record: ITransactionType): IAction[] {
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
export function isVisible(entity: ITransactionType, validationForm: UntypedFormGroup, fieldName: string): boolean {
    //check using const control = this.validationForm.get(fieldName);
    // or this.entity[fieldName]
    switch (fieldName) {
        case 'exampleId':
           //example=>  return (entity['transactionTypeId'] == 2 || entity['transactionTypeId'] == 3) ? true : false;
           return true;
    }
    return true;
}
//---------------------------------------------------------
export function isEnabled(entity: IEntity, validationForm: UntypedFormGroup, fieldName: string): boolean {
    //check using const control = this.validationForm.get(fieldName);
    // or this.entity[fieldName]
    switch (fieldName) {
        case 'exampleId':
           //example=>  return (entity['transactionTypeId'] == 2 || entity['transactionTypeId'] == 3) ? true : false;
           return true;
    }
    return true;
}

//Template_Start_Code_DontOverwrite_3
//Template_End_Code_DontOverwrite_3

