
import { IEntityRequest, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import { deleteTree, duplicateRecord, duplicateTree } from '@bfs/infrastructure-main/infrastructure.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const [ComponentNameCapital]Columns = [

];
//---------------------------------------------------------
export interface I[ComponentNameCapital] {

}
//---------------------------------------------------------
export function init[ComponentNameCapital](): I[ComponentNameCapital] {
    let entity: I[ComponentNameCapital] = {

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function [ComponentNameSmall]UntypedFormGroup(formBuilder: FormBuilder): any {
    return {

    };
} 
//---------------------------------------------------------
export interface I[ComponentNameCapital]WithLookup extends I[ComponentNameCapital]{

}
//---------------------------------------------------------
export interface I[ComponentNameCapital]Request extends IEntityRequest<I[ComponentNameCapital]Filter> {}

//---------------------------------------------------------
export interface I[ComponentNameCapital]Filter {
    [key: string]: any;

}
//---------------------------------------------------------
export function init[ComponentNameCapital]Request(): I[ComponentNameCapital]Request {
    let request: I[ComponentNameCapital]Request = {
        pageIndex: 1,
        pageSize: 5,
        columns: [ComponentNameCapital]Columns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

export function get[ComponentNameCapital]Actions(record: IQueryColumn): IAction[] {
        let links: IAction[] = [];

        return links;
    }
    //---------------------------------------------------------

