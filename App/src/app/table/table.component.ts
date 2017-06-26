import { TableSettings } from './models/table-settings';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.css']
})
export class TableComponent implements OnInit {

  @Input() settings: TableSettings;

  @Output() edit = new EventEmitter<any>();
  @Output() delete = new EventEmitter<any>();
  @Output() create = new EventEmitter<any>();

  tableClass: string;
  
  defaultSettings: TableSettings = {
    columns: [],
    sortable: false,
    sortColumn: -1 
  };

  constructor() { }

  ngOnInit() {
  }

  editItem(id: number) {
    this.edit.emit(id);
  }

  deleteItem(id: number) {
    this.delete.emit(id);
  }

  createItem() {
    this.create.emit();
  }

}
