import { Row } from '../../lib/row';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'table-edit-delete',
  template: `
    <div>
      <button md-raised-button (click)="this.edit.emit(row)">
              Edit
      </button>
      <button md-raised-button (click)="this.delete.emit(row)">
              Delete
      </button>
    </div>
  `,
  styles: []
})
export class TableEditDeleteComponent implements OnInit {

  @Input() row: Row;

  @Output() edit = new EventEmitter<any>();
  @Output() delete = new EventEmitter<any>();

  constructor() { }

  ngOnInit() {
  }

}
