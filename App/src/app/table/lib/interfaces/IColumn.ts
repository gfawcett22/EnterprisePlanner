export interface IColumn {
    key: string;
    title: string;
    filterable?: boolean;
    sortable?: boolean;
    currentSort: boolean;
    sortDirection: string;
}