import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
import * as operations from '@bfs/master/main/master.operations';

// Output Columns of a Query  [used in entity Query]
export const StructureCompareColumns = [
    { fieldName: 'bfsComponent_DataTypeId', displayName: 'Data Type', sortName: 'DataType_Name', width: '50px', isVisible:true },
{ fieldName: 'bfsComponent_DisplayName', displayName: 'Component Name', sortName: 'BfsComponent_DisplayName', width: '50px', isVisible:true },

    { fieldName: 'countId', displayName: 'Fields Count Per Component', sortName: 'countId', width: '50px', isVisible:true },

];

//---------------------------------------------------------

export interface IStructureCompare {
    [key: string]: any;
    bfsComponent_DataTypeId?: number;
bfsComponent_DisplayName?: string;

}
//---------------------------------------------------------
export interface IStructureCompareFilter {
    [key: string]: any;

    BfsComponent_DisplayName?: string;

    BfsComponent_DataTypeId?: number;

    countId?: { from?: string ; to?: string} ;

}
//---------------------------------------------------------

export interface IStructureCompareRequest extends IEntityRequest<IStructureCompareFilter> {}

//---------------------------------------------------------
export function initStructureCompareRequest(): IStructureCompareRequest {
    let request: IStructureCompareRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: StructureCompareColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {},
        filter: {

            BfsComponent_DisplayName: undefined ,

            BfsComponent_DataTypeId: undefined ,

            countId: { from: undefined , to: undefined} ,

            }
    };
    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------
export function renderStructureCompare(record: IEntity, column: IQueryColumn): any {
        const value = record[column.fieldName as keyof IEntity];
        switch (column.fieldName) {
            case 'bfsComponent_DataTypeId':
                return record['dataTypeName']?.toString();

            case 'countId':
                return record['countId']?.toString();

            default:
                return value;
        }
        return value;
    }
    //---------------------------------------------------------
export function getStructureCompareActions(component:any, record: IEntity): IAction[] {
        let links: IAction[] = [];

        return links;
    }
    //---------------------------------------------------------

