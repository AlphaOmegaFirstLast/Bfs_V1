
import { IEntityRequest, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import { deleteTree, duplicateRecord, duplicateTree } from '@bfs/infrastructure-main/infrastructure.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const ChartElementColumns = [
    { fieldName: 'chartElementId', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:true },
{ fieldName: 'chartElementName', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:true },
{ fieldName: 'chartElementNotes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:true },

];
//---------------------------------------------------------
export interface IChartElement {
    isDeleted?: boolean;
id?: string;
name?: string;
notes?: string;

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
export interface IChartElementWithLookup extends IChartElement{

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

export function getChartElementActions(record: IQueryColumn): IAction[] {
        let links: IAction[] = [];

links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/bfs/chart-element/add', displayText: 'Add New record' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['chartElementId'], route:'/bfs/chart-element/view', displayText: 'View...' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['chartElementId'], route:'/bfs/chart-element/edit', displayText: 'Edit...' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['chartElementId'], route:'/bfs/chart-element/delete', displayText: 'Delete...' 
});

links.push({
actionSource:'0', actionType:'FrontendFunction', actionLocation:'ListRow',recordId: record['chartElementId'], action: duplicateRecord, displayText: 'Duplicate Record', data: { postUrl: '/ChartElement', onSuccessMethodName: 'getReport' } 
});
links.push({
actionSource:'0', actionType:'FrontendFunction', actionLocation:'ListRow',recordId: record['chartElementId'], action: duplicateTree, displayText: 'Duplicate Tree', data: { postUrl: '/Operations/ChartElement/DuplicateTree', onSuccessMethodName: 'getReport' } 
});

        return links;
    }
    //---------------------------------------------------------

