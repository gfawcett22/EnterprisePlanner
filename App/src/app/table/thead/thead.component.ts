import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Grid } from "../lib/grid";

@Component({
  selector: 'table-head',
  templateUrl: './thead.component.html',
  styles: []
})
export class THeadComponent implements OnInit {

  @Input() grid: Grid;

  @Output() sort = new EventEmitter<any>();
  @Output() create = new EventEmitter<any>();
  @Output() filter = new EventEmitter<any>();

  constructor() { }

  ngOnInit() {
    console.log(this.grid);
  }

}
