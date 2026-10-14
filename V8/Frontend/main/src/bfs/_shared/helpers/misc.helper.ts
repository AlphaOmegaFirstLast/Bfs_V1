import { IQueryColumn } from "../interfaces";

    export function reOrderColumns(inputColumns: IQueryColumn[]): IQueryColumn[] {
        let resultColumns = inputColumns.sort((a, b) =>
            (a.columnOrder ?? 0) - (b.columnOrder ?? 0)
        );

        return resultColumns;
    }
    //---------------------------------------------------------