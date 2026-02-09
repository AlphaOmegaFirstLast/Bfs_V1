import { IEntityRequest } from "@bfs/_shared/interfaces";

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const ChartElementColumns = [
    { fieldName: 'isDeleted', displayName: 'IsDeleted', sortName:'IsDeleted', width: '50px', isVisible:false },
{ fieldName: 'id', displayName: 'ID', sortName:'Id', width: '50px', isVisible:false },
{ fieldName: 'name', displayName: 'Name', sortName:'Name', width: '50px', isVisible:true },
{ fieldName: 'notes', displayName: 'Notes', sortName:'Notes', width: '50px', isVisible:true },

];
//---------------------------------------------------------
// Fields of an Entity [used in Entity form]
export function chartElementUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],
name: [''],
notes: [''],

    };
} 
//---------------------------------------------------------

export interface IChartElement {
    isDeleted?: boolean;
id?: string;
name?: string;
notes?: string;

}
//---------------------------------------------------------
export interface IChartElementWithLookup extends IChartElement{

}
//---------------------------------------------------------

export function initChartElement(): IChartElement {
    let entity: IChartElement = {
        isDeleted: false,
id: '0',
name: '',
notes: '',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------
export interface IChartElementRequest extends IEntityRequest<IChartElementFilter> {}

//---------------------------------------------------------
export interface IChartElementFilter {
    [key: string]: any;

    Name?: string;

}
//---------------------------------------------------------
export function initChartElementRequest(): IChartElementRequest {
    let request: IChartElementRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: ChartElementColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {

            Name: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

