import { Column } from "app/table/models/column";

export interface TableSettings {
    columns: Column[];
    sortable: boolean;
    sortColumn: number;
    noResultsMessage?: string;
}