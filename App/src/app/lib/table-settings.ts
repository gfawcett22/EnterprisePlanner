import { Column } from "app/lib/column";

export interface TableSettings {
    columns: Column[];
    sortable: boolean;
    sortColumn: string;
    noResultsMessage?: string;
    showActionButtons: boolean;
    rows: any[];
}