import {Component, Input, Output, EventEmitter} from '@angular/core';

import { Grid } from '../../lib/grid';
import { Column } from "../../lib/column";

@Component({
  selector: '[thead-filters-row]',
  template: `
    <th *ngFor="let column of grid.getColumns()" >
      <table-filter [source]="source"
                    [column]="column"
                    (filter)="filter.emit($event)">
      </table-filter>
    </th>
  `,
})
export class TheadFitlersRowComponent {

  @Input() grid: Grid;

  @Output() create = new EventEmitter<any>();
  @Output() filter = new EventEmitter<any>();

}
