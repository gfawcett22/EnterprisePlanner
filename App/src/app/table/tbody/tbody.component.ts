import { Component, EventEmitter, OnChanges, Output, Input } from '@angular/core';
import { Grid } from "../lib/grid";

@Component({
  selector: '[table-body]',
  templateUrl: './tbody.component.html',
  styles: [
    `
    .center {
      text-align: center;
    }
    `
  ]
})
export class TBodyComponent implements OnChanges {
  @Input() grid: Grid;

  @Output() edit = new EventEmitter<any>();
  @Output() delete = new EventEmitter<any>();

  noResultsMessage: string;
  showActionButtons: boolean;

  constructor() { }

  ngOnChanges() {
    this.noResultsMessage = this.grid.getSetting('noResultsMessage');
    this.showActionButtons = this.grid.getSetting('showActionButtons');
  }

}
