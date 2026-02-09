import { IEntityRequest } from "@bfs/_shared/interfaces";

import { ICustomField, initCustomFields } from "@bfs/_shared/customFields";

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const ClientColumns = [
    { fieldName: 'isDeleted', displayName: 'IsDeleted', sortName:'IsDeleted', width: '50px', isVisible:false },
{ fieldName: 'id', displayName: 'ID', sortName:'Id', width: '50px', isVisible:false },
{ fieldName: 'name', displayName: 'Name', sortName:'Name', width: '50px', isVisible:true },
{ fieldName: 'notes', displayName: 'Notes', sortName:'Notes', width: '50px', isVisible:true },
{ fieldName: 'dbConnection', displayName: 'Database Connection', sortName:'DbConnection', width: '50px', isVisible:true },

    { fieldName: 'customFields', displayName: 'Custom Fields', sortName:'CustomFields', width: '50px', isVisible:true },

];
//---------------------------------------------------------
// Fields of an Entity [used in Entity form]
export function clientUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],
name: [''],
notes: [''],
dbConnection: [''],

    customFields: formBuilder.array([]),

    };
} 
//---------------------------------------------------------

export interface IClient {
    isDeleted?: boolean;
id?: string;
name?: string;
notes?: string;
dbConnection?: string;

    customFields?: ICustomField[];

}
//---------------------------------------------------------
export interface IClientWithLookup extends IClient{

}
//---------------------------------------------------------

export function initClient(): IClient {
    let entity: IClient = {
        isDeleted: false,
id: '0',
name: '',
notes: '',
dbConnection: '',

        customFields: initCustomFields(),

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------
export interface IClientRequest extends IEntityRequest<IClientFilter> {}

//---------------------------------------------------------
export interface IClientFilter {
    [key: string]: any;

    clientName?: string;

}
//---------------------------------------------------------
export function initClientRequest(): IClientRequest {
    let request: IClientRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: ClientColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {

            clientName: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

