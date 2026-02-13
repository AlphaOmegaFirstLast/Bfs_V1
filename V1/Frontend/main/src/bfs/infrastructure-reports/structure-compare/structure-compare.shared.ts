import { IEntityRequest, IQueryColumn, IAction } from "@bfs/_shared/interfaces";

// Output Columns of a Query  [used in entity Query]
export const StructureCompareColumns = [
    { fieldName: 'bfsComponent_DataTypeId', displayName: 'Data Type', sortName: 'DataType', width: '50px', isVisible:true },
{ fieldName: 'bfsComponent_DisplayName', displayName: 'Component Name', sortName: 'DisplayName', width: '50px', isVisible:true },

    { fieldName: 'countId', displayName: 'Fields Count Per Component', sortName: 'countId', width: '50px', isVisible:true },

];

//---------------------------------------------------------

export interface IStructureCompare {
    bfsComponent_DataTypeId?: number;
bfsComponent_DisplayName?: string;

}
//---------------------------------------------------------
export interface IStructureCompareWithLookup extends IStructureCompare{

    dataTypeName?: string;

    countId?:number;

}
//---------------------------------------------------------
export interface IStructureCompareFilter {
    [key: string]: any;

    DisplayName?: string;

    DataTypeId?: number;

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

            DisplayName: undefined ,

            DataTypeId: undefined ,

            countId: { from: undefined , to: undefined} ,

            }
    };
    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------
export function getStructureCompareActions(record: IQueryColumn): IAction[] {
        let links: IAction[] = [];

links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['dataTypeId'], route:'/bfs/data-type/view', displayText:'Go to DataType' 
});

        return links;
    }
    //---------------------------------------------------------

