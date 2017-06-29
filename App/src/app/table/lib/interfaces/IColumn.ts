export interface IColumn {
    title: string;
    filterable?: boolean;
    sortable?: boolean;
    currentSort: boolean;
    sortDirection: string;
}