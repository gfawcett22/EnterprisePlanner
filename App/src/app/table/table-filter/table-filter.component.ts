import { FilterObject } from '../lib/interfaces/FilterObject';
import { FormControl } from '@angular/forms';
import {
    ChangeDetectionStrategy,
    Component,
    EventEmitter,
    Input,
    OnChanges,
    OnInit,
    Output,
    SimpleChanges
} from '@angular/core';
import { Column } from "../lib/column";

import 'rxjs/add/operator/distinctUntilChanged';
import 'rxjs/add/operator/debounceTime';
import 'rxjs/add/operator/skip';

@Component({
  selector: 'table-filter',
  template: `
    <md-input-container>
      <input [(ngModel)]="query"
            mdInput
            [ngClass]="inputClass"
            [formControl]="inputControl"
            placeholder="{{ column.title }}" />
    </md-input-container>
  `,
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class TableFilterComponent implements OnInit, OnChanges {

  delay: number = 300;
  query: string;
  @Input() inputClass: string;
  @Input() column: Column;
  @Output() filter = new EventEmitter<any>();

  inputControl = new FormControl();

  constructor() { }

  ngOnInit() {
    this.inputControl.valueChanges
      .skip(1)
      .distinctUntilChanged()
      .debounceTime(this.delay)
      .subscribe((value: string) => this.setFilter());
  }

  ngOnChanges(changes: SimpleChanges) {
    //console.log('table filter changes', changes);
  }

  setFilter() {
    let returnObj = {};
    returnObj[this.column.id] = this.query;
    this.filter.emit(returnObj);
  }
}
