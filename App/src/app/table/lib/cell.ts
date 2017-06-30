import { Column } from "./column";
import { DataSet } from "./data-set";
import { Row } from "./row";

export class Cell {
    newValue: any;

    /**
     *
     */
    constructor(protected value: any, protected row: Row, protected column: Column, protected dataSet: DataSet) {}

    getColumn(): Column {
        return this.column;
    }

    getRow(): Row {
        return this.row;
    }

    getValue(): any {
        return this.value;
    }
    setValue(value: any): any {
        this.newValue = value;
    }

    getId(): string {
        return this.getColumn().id;
    }

    getTitle(): string {
        return this.getColumn().title;
    }
}