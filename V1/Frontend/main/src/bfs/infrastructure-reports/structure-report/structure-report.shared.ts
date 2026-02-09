import { IEntityRequest } from "@bfs/_shared/interfaces";

// Output Columns of a Query  [used in entity Query]
export const StructureReportColumns = [
    { fieldName: 'bfsComponentDataTypeId', displayName: 'Data Type', sortName: 'DataType', width: '50px', isVisible:true },
{ fieldName: 'bfsComponentDisplayName', displayName: 'Component Name', sortName: 'DisplayName', width: '50px', isVisible:true },

    { fieldName: 'countId', displayName: 'Fields Count Per Component', sortName: 'countId', width: '50px', isVisible:true },

];

//---------------------------------------------------------

export interface IStructureReport {
    bfsComponentDataTypeId?: number;
bfsComponentDisplayName?: string;

}
//---------------------------------------------------------
export interface IStructureReportWithLookup extends IStructureReport{

    dataTypeName?: string;

    countId?:number;

}
//---------------------------------------------------------
export interface IStructureReportFilter {
    [key: string]: any;

    DisplayName?: string;

    DataTypeId?: number;

    countId?: { from?: string ; to?: string} ;

}
//---------------------------------------------------------

export interface IStructureReportRequest extends IEntityRequest<IStructureReportFilter> {}

//---------------------------------------------------------
export function initStructureReportRequest(): IStructureReportRequest {
    let request: IStructureReportRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: StructureReportColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {},
        filter: {

            DisplayName: undefined ,

            DataTypeId: undefined ,

            countId: { from: undefined , to: undefined} ,

            }
    };
    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

