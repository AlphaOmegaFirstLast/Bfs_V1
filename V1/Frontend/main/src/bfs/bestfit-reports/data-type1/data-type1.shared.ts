import { IEntityRequest } from "@bfs/_shared/interfaces";

// Output Columns of a Query  [used in entity Query]
export const DataType1Columns = [
    { fieldName: 'dataTypeId', displayName: 'Id', sortName: 'Id', width: '50px', isVisible:true },
{ fieldName: 'dataTypeName', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:true },
{ fieldName: 'dataTypeNotes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:true },

];

//---------------------------------------------------------

export interface IDataType1 {
    dataTypeId?: number;
dataTypeName?: string;
dataTypeNotes?: string;

}
//---------------------------------------------------------
export interface IDataType1WithLookup extends IDataType1{

}
//---------------------------------------------------------
export interface IDataType1Filter {
    [key: string]: any;

    dataTypeNotes?: string;

    dataTypeId?: { from?: number ; to?: number} ;

}
//---------------------------------------------------------

export interface IDataType1Request extends IEntityRequest<IDataType1Filter> {}

//---------------------------------------------------------
export function initDataType1Request(): IDataType1Request {
    let request: IDataType1Request = {
        pageIndex: 1,
        pageSize: 5,
        columns: DataType1Columns.map(column => ({ ...column })),
        group: '',
        sortOption: {},
        filter: {

            dataTypeNotes: undefined ,

            dataTypeId: { from: undefined , to: undefined} ,

            }
    };
    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

