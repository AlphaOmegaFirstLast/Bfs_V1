import { IQueryColumn } from "../interfaces";

    export function reOrderColumns(inputColumns: IQueryColumn[]): IQueryColumn[] {
        let resultColumns = inputColumns.sort((a, b) =>
            (a.columnOrder ?? 0) - (b.columnOrder ?? 0)
        );

        return resultColumns;
    }
    //---------------------------------------------------------
    function normalizeObjectKeysToLowerFirstLetter<T extends object>(value: T): T {
        // easier code
        // let lowercaseKey = key.charAt(0).toLowerCase() + key.slice(1); // convert first letter to lowercase

    if (!value || typeof value !== 'object' || Array.isArray(value)) {
        return value;
    }

    const result = {} as Record<string, unknown>;
    Object.entries(value as Record<string, unknown>).forEach(([key, val]) => {
        const normalizedKey = key.charAt(0).toLowerCase() + key.slice(1);
        result[normalizedKey] = val;
    });

    return result as T;
}
//---------------------------------------------------------