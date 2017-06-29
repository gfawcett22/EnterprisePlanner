import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ITableSettings } from "./lib/interfaces/ITableSettings";
import { deepExtend } from "./lib/helpers";
import { Grid } from "./lib/grid";

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.css'],
})
export class TableComponent implements OnInit {

  @Input() settings: ITableSettings;

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
    showActionButtons: true,
    rows: []
  };

  constructor() {
  }

  ngOnInit() {
  }

  prepareSettings(): Object {
    return deepExtend({}, this.defaultSettings, this.settings);
  }
  
  initGrid() {
    this.grid = new Grid(this.prepareSettings());
  }
}
