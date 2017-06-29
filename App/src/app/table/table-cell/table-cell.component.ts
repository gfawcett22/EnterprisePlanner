import { Component, OnInit, Input } from '@angular/core';
import { Cell } from "../lib/cell";

@Component({
  selector: 'table-cell',
  template: `
    <div>{{ cell.getValue() }}</div>
  `,
  styles: []
})
export class TableCellComponent implements OnInit {

  @Input() cell: Cell;
  constructor() { }

  ngOnInit() {
  }

}
