import { DataSet } from "./data-set";

export class Column {

    key: string = '';
    title: string = '';
    width: string = '';
    isSortable: boolean = false;
    isFilterable: boolean = false;
    sortDirection: string = '';
    defaultSortDirection: string = '';

    constructor(public id: string, protected settings: any, protected dataSet: DataSet) {
        this.process();
     }

    protected process() {
        this.title = this.settings['title'];
        this.width = this.settings['width'];
        this.isFilterable = typeof this.settings['filter'] === 'undefined' ? true : !!this.settings['filter'];
        this.defaultSortDirection = ['asc', 'desc']
            .indexOf(this.settings['sortDirection']) !== -1 ? this.settings['sortDirection'] : '';
        this.isSortable = typeof this.settings['sort'] === 'undefined' ? true : !!this.settings['sort'];
        this.sortDirection = this.prepareSortDirection();
    }

    prepareSortDirection(): string {
        return this.settings['sort'] === 'desc' ? 'desc' : 'asc';
    }
}