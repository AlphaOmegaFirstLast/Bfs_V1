import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, ValidationErrors, FormBuilder } from "@angular/forms";

import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
import { getFormControlValidation } from "@bfs/_shared/objectFields";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/stockex/main/stockex.operations';
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

// Output Columns of a Query  [used in entity Query]
export const CreditInterestColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'interestPercent', displayName: 'Interest Percent', sortName: 'InterestPercent', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'paymentModeId', displayName: 'Payment Mode', sortName: 'PaymentMode_Name', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'brokerAgreementId', displayName: 'Broker Agreement', sortName: 'BrokerAgreement_Name', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:true, columnOrder:1 },

];
//---------------------------------------------------------
export interface ICreditInterest extends IEntity{
    id?: string;
name?: string;
interestPercent?: number;
notes?: string;
isDeleted?: boolean;

    paymentModeId?: string;
brokerAgreementId?: string;

}
//---------------------------------------------------------
export function initCreditInterest(): ICreditInterest {
    let entity: ICreditInterest = {
        id: '0',
name: '',
interestPercent: 0,
notes: '',
isDeleted: false,

        paymentModeId: '0',
brokerAgreementId: '0',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function creditInterestUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    id: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
name: ['',getFormControlValidation('{"IsRequired":false,"MinLength":"0","MaxLength":"0","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
interestPercent: [0,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
notes: ['',getFormControlValidation('{"IsRequired":false,"MinLength":"0","MaxLength":"0","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
isDeleted: [false,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

    paymentModeId: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
brokerAgreementId: ['0',getFormControlValidation('{"IsRequired":true,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

    };
} 
//---------------------------------------------------------
export interface ICreditInterestRequest extends IEntityRequest<ICreditInterestFilter> {}

//---------------------------------------------------------
export interface ICreditInterestFilter {
    [key: string]: any;
    Id?: string;

    Name?: string;
Notes?: string;

    PaymentModeId?: string;
BrokerAgreementId?: string;

    InterestPercent?: { from?: number ; to?: number} ;

}
//---------------------------------------------------------
export function initCreditInterestRequest(): ICreditInterestRequest {
    let request: ICreditInterestRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: CreditInterestColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {
            Id: undefined ,

            Name: undefined ,
Notes: undefined ,

            PaymentModeId: undefined ,
BrokerAgreementId: undefined ,

            InterestPercent: { from: undefined , to: undefined} ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------
export function renderCreditInterest(record: ICreditInterest, column: IQueryColumn): any {
        const value = record[column.fieldName as keyof ICreditInterest];
        switch (column.fieldName) {
            case 'paymentModeId':
                return record['paymentModeName']?.toString();
case 'brokerAgreementId':
                return record['brokerAgreementName']?.toString();

            default:
                return value;
        }
        return value;
    }
    //---------------------------------------------------------
export function getCreditInterestActions(component: any, record: ICreditInterest): IAction[] {
        let links: IAction[] = [];

        return links;
    }
//---------------------------------------------------------
export function isVisible(entity: ICreditInterest, validationForm: UntypedFormGroup, fieldName: string): boolean {
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

