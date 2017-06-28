import { TableSettings } from './table-settings';

import { DataSet } from "app/lib/data-set";
import { getDeepFromObject, Deferred } from "app/lib/helpers";
import { Column } from "app/lib/column";
import { Row } from "app/lib/row";

export class Grid {
    settings: TableSettings;
    dataSet: DataSet;

    constructor(settings: any) {
        this.setSettings(settings);
    }

    setSettings(settings: TableSettings) {
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