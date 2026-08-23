
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/stockex-main/stockex.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const CalculationMethodColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false },
{ fieldName: 'name', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:true },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:false },

];
//---------------------------------------------------------
export interface ICalculationMethod {
    isDeleted?: boolean;
id?: string;
name?: string;
notes?: string;

}
//---------------------------------------------------------
export function initCalculationMethod(): ICalculationMethod {
    let entity: ICalculationMethod = {
        isDeleted: false,
id: '0',
name: '',
notes: '',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function calculationMethodUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],
name: [''],
notes: [''],

    };
} 
//---------------------------------------------------------
export interface ICalculationMethodWithLookup extends ICalculationMethod{

}
//---------------------------------------------------------
export interface ICalculationMethodRequest extends IEntityRequest<ICalculationMethodFilter> {}

//---------------------------------------------------------
export interface ICalculationMethodFilter {
    [key: string]: any;
    Id?: string;

    Name?: string;

}
//---------------------------------------------------------
export function initCalculationMethodRequest(): ICalculationMethodRequest {
    let request: ICalculationMethodRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: CalculationMethodColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {
            Id: undefined ,

            Name: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

export function getCalculationMethodActions(component: any, record: IEntity): IAction[] {
        let links: IAction[] = [];

if (component.accessService.isActionAllowed('calculationMethod', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/stkx/calculation-method/add', displayText: 'Add New record'
});
}
if (component.accessService.isActionAllowed('calculationMethod', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/calculation-method/view', displayText: 'View...'
});
}
if (component.accessService.isActionAllowed('calculationMethod', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/calculation-method/edit', displayText: 'Edit...' 
});
}
if (component.accessService.isActionAllowed('calculationMethod', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/calculation-method/delete', displayText: 'Delete...' 
});
}

        return links;
    }
    //---------------------------------------------------------

