import { SharedModule } from '../shared/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TableRowComponent } from './table-row/table-row.component';
import { TableCellComponent } from './table-cell/table-cell.component';
import { TableComponent } from "app/table/table.component";
import { TbodyComponent } from './tbody/tbody.component';
import { TableEditDeleteComponent } from './tbody/table-edit-delete/table-edit-delete.component';
import { TheadComponent } from './thead/thead.component';
import { TableFilterComponent } from './table-filter/table-filter.component';
import { TheadTitlesRowComponent } from './thead/rows/thead-titles-row.component';
import { ColumnTitleComponent } from './thead/cells/column-title.component';
import { TitleComponent } from './thead/cells/title/table-title.component';

@NgModule({
  imports: [
    CommonModule,
    SharedModule
  ],
  declarations: [
    TableComponent,
    TableRowComponent,
    TableCellComponent,
    TbodyComponent,
    TableEditDeleteComponent,
    TheadComponent,
    TableFilterComponent,
    TheadTitlesRowComponent,
    ColumnTitleComponent,
    TitleComponent
  ],
  exports: [
    TableComponent
  ]
})
export class TableModule { }
