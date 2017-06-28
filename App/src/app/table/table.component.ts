import { ChangeDetectionStrategy, Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { TableSettings } from "app/lib/table-settings";
import { deepExtend } from "app/lib/helpers";
import { Grid } from "app/lib/grid";

@Component({
  selector: 'table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class TableComponent implements OnInit {

  @Input() settings: TableSettings;

  @Output() edit = new EventEmitter<any>();
  @Output() delete = new EventEmitter<any>();
  @Output() create = new EventEmitter<any>();
  @Output() filter = new EventEmitter<any>();
  @Output() sort = new EventEmitter<any>();

  tableClass: string;

  grid: Grid;
  defaultSettings: TableSettings = {
    columns: [],
    sortable: false,
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
