import { Component, Input, Output, EventEmitter } from '@angular/core';

import { Column } from '../../lib/column';

@Component({
  selector: 'column-title',
  template: `
    <div>
      <table-title [column]="column" (sort)="sort.emit($event)"></table-title>
    </div>
  `,
})
export class ColumnTitleComponent {

  @Input() column: Column;

  @Output() sort = new EventEmitter<any>();

}
