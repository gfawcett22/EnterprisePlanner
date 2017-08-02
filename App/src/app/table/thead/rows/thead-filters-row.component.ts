import { FilterObject } from '../../lib/interfaces/FilterObject';
import { ChangeDetectionStrategy, Component, EventEmitter, Input, Output } from '@angular/core';

import { Grid } from '../../lib/grid';
import { Column } from "../../lib/column";

@Component({
  selector: '[thead-filters-row]',
  template: `
    <th *ngIf="grid.getSetting('showActionButtons')">
      <button md-raised-button (click)="create.emit(null)">Create New</button>
    </th>
    <th *ngFor="let column of grid.getColumns()" >
      <table-filter [column]="column"
                    (filter)="updateFilters($event)">
      </table-filter>
    </th>
  `,
})
export class TheadFitlersRowComponent {

  @Input() grid: Grid;

  @Output() create = new EventEmitter<any>();
  @Output() filter = new EventEmitter<any>();

  private filterObject: Object = {};

  updateFilters($event: Object): void {
    if ($event) {
      Object.assign(this.filterObject, $event);
      this.filter.emit(this.filterObject);
    }
  }
}
