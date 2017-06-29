import { ITableSettings } from './interfaces/ITableSettings';

import { DataSet } from "./data-set";
import { getDeepFromObject, Deferred } from "./helpers";
import { Column } from "./column";
import { Row } from "./row";

export class Grid {
    settings: ITableSettings;
    dataSet: DataSet;

    constructor(settings: any) {
        this.setSettings(settings);
    }

    setSettings(settings: ITableSettings) {
        this.settings = settings;
        this.dataSet = new DataSet([], this.getSetting('columns'));
    }

    getSetting(name: string, defaultValue?: any): any {
        return getDeepFromObject(this.settings, name, defaultValue);
    }

    getColumns(): Column[] {
        return this.dataSet.getColumns();
    }

    getRows(): Row[] {
        return this.dataSet.getRows();
    }
    
    getFirstRow(): Row {
        return this.dataSet.getFirstRow();
    }

    getLastRow(): Row {
        return this.dataSet.getLastRow();
    }
}