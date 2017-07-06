import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Grid } from "../../lib/grid";

@Component({
  selector: '[thead-titles-row]',
  template: `
    <th *ngIf="grid.getSetting('showActionButtons')">
    </th>
    <th *ngFor="let column of grid.getColumns()"
      [style.width]="column.width" >
      <column-title [column]="column" (sort)="sort.emit($event)"></column-title>
    </th>
  `
})
export class TheadTitlesRowComponent implements OnInit {

  @Input() grid: Grid;

  @Output() sort = new EventEmitter<any>();
  @Output() create = new EventEmitter<any>();
  @Output() selectAllRows = new EventEmitter<any>();

  constructor() { }

  ngOnInit() {
  }

}
