import { Component, Input, Output, EventEmitter, SimpleChanges } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';

import { Column } from '../../../lib/column';

@Component({
  selector: 'table-title',
  template: `
    <a href="#" *ngIf="column.isSortable"
                (click)="_sort($event, column)"                
                [ngClass]="currentDirection">
      {{ column.title }}
    </a>
    <span *ngIf="!column.isSortable">{{ column.title }}</span>
  `,
})
export class TitleComponent {

  currentDirection = '';
  @Input() column: Column;
  @Output() sort = new EventEmitter<any>();

  _sort(event: any) {
    event.preventDefault();
    this.changeSortDirection();
    
    this.sort.emit(this.column);
  }

  changeSortDirection(): string {
    if (this.currentDirection) {
      const newDirection = this.currentDirection === 'asc' ? 'desc' : 'asc';
      this.currentDirection = newDirection;
    } else {
      this.currentDirection = this.column.sortDirection;
    }
    return this.currentDirection;
  }
}
