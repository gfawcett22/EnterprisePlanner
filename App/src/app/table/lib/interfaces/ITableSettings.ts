import { IColumn } from "./IColumn";

export interface ITableSettings {
    columns: IColumn[];
    sortColumn: string;
    noResultsMessage?: string;
    showActionButtons: boolean;
    rows: any[];
}