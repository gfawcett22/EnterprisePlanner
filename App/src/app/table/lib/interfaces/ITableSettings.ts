import { IColumn } from "./IColumn";

export interface ITableSettings {
    columns: Object;
    sortColumn: string;
    noResultsMessage?: string;
    showActionButtons: boolean;
}