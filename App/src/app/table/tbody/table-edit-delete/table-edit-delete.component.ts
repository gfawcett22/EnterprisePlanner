import { Component, OnInit, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'table-edit-delete',
  template: `
    <div>
      <button md-raised-button (click)="this.edit.emit($event)">
              Edit
      </button>
      <button md-raised-button (click)="this.delete.emit($event)">
              Delete
      </button>
    </div>
  `,
  styles: []
})
export class TableEditDeleteComponent implements OnInit {

  @Output() edit = new EventEmitter<any>();
  @Output() delete = new EventEmitter<any>();
  constructor() { }

  ngOnInit() {
  }

}
