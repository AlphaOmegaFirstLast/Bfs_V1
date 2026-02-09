import { IEntityRequest } from "@bfs/_shared/interfaces";

// Output Columns of a Query  [used in entity Query]
export const StructureReportColumns = [
    { fieldName: 'componentDataTypeId', displayName: 'Data Type', sortName: 'DataType', width: '50px', isVisible:true },
{ fieldName: 'componentDisplayName', displayName: 'Component Name', sortName: 'DisplayName', width: '50px', isVisible:true },

    { fieldName: 'countId', displayName: 'Fields Count', sortName: 'countId', width: '50px', isVisible:true },

];

//---------------------------------------------------------

export interface IStructureReport {
    componentDataTypeId?: number;
componentDisplayName?: string;

}
//---------------------------------------------------------
export interface IStructureReportWithLookup extends IStructureReport{

    dataTypeName?: string;

    countId?:number;

}
//---------------------------------------------------------
export interface IStructureReportFilter {
    [key: string]: any;

    componentDisplayName?: string;

    componentDataTypeId?: number;

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

            componentDisplayName: undefined ,

            componentDataTypeId: undefined ,

            }
    };
    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

