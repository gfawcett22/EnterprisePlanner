import {
    ChangeDetectionStrategy,
    Component,
    EventEmitter,
    Input,
    OnChanges,
    Output,
    SimpleChange
} from '@angular/core';
import { ITableSettings } from "./lib/interfaces/ITableSettings";
import { deepExtend } from "./lib/helpers";
import { Grid } from "./lib/grid";

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.css'],
})
export class TableComponent implements OnChanges {

  @Input() settings: ITableSettings;
  @Input() rows: any[];

  @Output() edit = new EventEmitter<any>();
  @Output() delete = new EventEmitter<any>();
  @Output() create = new EventEmitter<any>();
  @Output() filter = new EventEmitter<any>();
  @Output() sort = new EventEmitter<any>();

  tableClass: string;

  grid: Grid;
  defaultSettings: ITableSettings = {
    columns: [],
    sortColumn: '',
    showActionButtons: true
  };

  constructor() {
  }

  ngOnChanges(changes: { [propertyName: string]: SimpleChange }) {
    //this.initGrid();
    console.log(changes);
    if (this.grid) {
      if (changes['settings']) {
        this.grid.setSettings(this.prepareSettings(), this.rows);
      } 
      else if(changes['rows']){
        this.grid.setSettings(null, this.rows);
      }
    } else {
      this.initGrid();
    }   
  }

  prepareSettings(): ITableSettings {
    return deepExtend({}, this.defaultSettings, this.settings);
  }
  
  initGrid() {
    this.grid = new Grid(this.prepareSettings(), this.rows);
  }
}
