import { Component, Input, OnInit } from '@angular/core';
import { Cell } from "../lib/cell";

@Component({
  selector: 'table-row',
  styles: [],
  template: `
    <tr>
      <th *ngFor="let cell of cells">
        <table-cell [cell]="cell"></table-cell>
      </th>
    <tr>
  `   
})
export class TableRowComponent implements OnInit {

  @Input() cells: Cell[];
  constructor() { }

  ngOnInit() {
  }

}
