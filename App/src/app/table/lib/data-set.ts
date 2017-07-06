import { Column } from "./column";
import { Row } from "./row";

export class DataSet {

    protected data: Array<any> = [];
    protected columns: Array<Column> = [];
    protected rows: Array<Row> = [];

    constructor(data: Array<any> = [], protected columnSettings: Object) {
        this.createColumns(columnSettings);
        this.setData(data);
    }

    getColumns(): Array<Column> {
        return this.columns;
    }

    getRows(): Array<Row> {
        return this.rows;
    }

    getFirstRow(): Row {
        return this.rows[0];
    }

    getLastRow(): Row {
        return this.rows[this.rows.length - 1];
    }

    findRowByData(data: any): Row {
        return this.rows.find((row: Row) => row.getData() === data);
    }

    setData(data: Array<any>) {
        this.data = data;
        this.createRows();
    }

    createColumns(settings: any) {
        for (const id in settings) {
            if (settings.hasOwnProperty(id)) {
                this.columns.push(new Column(id, settings[id], this));
            }
        }
    }
    createRows() {
        this.rows = [];
        this.data.forEach((el, index) => {
            this.rows.push(new Row(index, el, this));
        });
    }
}