import { Cell } from "app/lib/cell";
import { Column } from "app/lib/column";
import { DataSet } from "app/lib/data-set";

export class Row {
    cells: Array<Cell> = [];

    constructor(public index: number, protected data: any, protected _dataSet: DataSet) {
        this.process();
    }

    setData(data: any): any {
        this.data = data;
        this.process();
    }

    getNewData(): any {
        const values = Object.assign({}, this.data);
        this.getCells().forEach((cell) => values[cell.getColumn().id] = cell.newValue);
        return values;
    }

    getCell(column: Column): Cell {
        return this.cells.find(el => el.getColumn() === column);
    }

    getCells() {
        return this.cells;
    }

    getData(): any {
        return this.data;
    }
    process() {
        this.cells = [];
        this._dataSet.getColumns().forEach((column: Column) => {
            const cell = this.createCell(column);
            this.cells.push(cell);
        });
    }

    createCell(column: Column): Cell {
        const defValue = (column as any).settings.defaultValue ? (column as any).settings.defaultValue : '';
        const value = typeof this.data[column.id] === 'undefined' ? defValue : this.data[column.id];
        return new Cell(value, this, column, this._dataSet);
    }

}