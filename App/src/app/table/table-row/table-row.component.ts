import { Component, Input, OnInit } from '@angular/core';
import { Cell } from "app/lib/cell";

@Component({
  selector: 'table-row',
  styles: [],
  template: `
    <tr>
      <td *ngFor="let cell of cells">
        <table-cell [cell]="cell"></table-cell>
      </td>
    <tr>
  `   
})
export class TableRowComponent implements OnInit {

  @Input() cells: Cell[];
  constructor() { }

  ngOnInit() {
  }

}
