import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, ValidationErrors, FormBuilder } from "@angular/forms";

import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
import { getFormControlValidation } from "@bfs/_shared/objectFields";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/auth/main/auth.operations';
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

// Output Columns of a Query  [used in entity Query]
export const AppColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'name', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'bfsSystemId', displayName: 'BestFit System', sortName: 'BfsSystem_Name', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'logo', displayName: 'Logo', sortName: 'Logo', width: '50px', isVisible:false, columnOrder:1 },

];
//---------------------------------------------------------
export interface IApp extends IEntity{
    isDeleted?: boolean;
id?: string;
name?: string;
notes?: string;
logo?: string;

    bfsSystemId?: string;

}
//---------------------------------------------------------
export function initApp(): IApp {
    let entity: IApp = {
        isDeleted: false,
id: '0',
name: '',
notes: '',
logo: '',

        bfsSystemId: '0',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function appUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
id: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
name: ['',getFormControlValidation('{"IsRequired":true,"MinLength":"3","MaxLength":"50","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
notes: ['',getFormControlValidation('{"IsRequired":false,"MinLength":"","MaxLength":"1000","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
logo: ['',getFormControlValidation('{"IsRequired":true,"MinLength":"3","MaxLength":"300","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

    bfsSystemId: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

    };
} 
//---------------------------------------------------------
export interface IAppRequest extends IEntityRequest<IAppFilter> {}

//---------------------------------------------------------
export interface IAppFilter {
    [key: string]: any;
    Id?: string;

    Name?: string;
Logo?: string;

    BfsSystemId?: string;

}
//---------------------------------------------------------
export function initAppRequest(): IAppRequest {
    let request: IAppRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: AppColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {
            Id: undefined ,

            Name: undefined ,
Logo: undefined ,

            BfsSystemId: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------
export function renderApp(record: IApp, column: IQueryColumn): any {
        const value = record[column.fieldName as keyof IApp];
        switch (column.fieldName) {
            case 'bfsSystemId':
                return record['bfsSystemName']?.toString();

            default:
                return value;
        }
        return value;
    }
    //---------------------------------------------------------
export function getAppActions(component: any, record: IApp): IAction[] {
        let links: IAction[] = [];

if (component.accessService.isActionAllowed('app', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/ath/app/add', displayText: 'Add New record'
});
}
if (component.accessService.isActionAllowed('app', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/ath/app/view', displayText: 'View...'
});
}
if (component.accessService.isActionAllowed('app', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/ath/app/edit', displayText: 'Edit...' 
});
}
if (component.accessService.isActionAllowed('app', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/ath/app/delete', displayText: 'Delete...' 
});
}
if (component.accessService.isActionAllowed('app', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['bfsSystemId'], route:'/ath/bfs-system/view', displayText:'Go to BfsSystem'
});
}

        return links;
    }
//---------------------------------------------------------
export function isVisible(entity: IApp, validationForm: UntypedFormGroup, fieldName: string): boolean {
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

