import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MdToolbarModule, MdButtonModule, MdDialogModule, MdInputModule } from '@angular/material';

import { TableRowComponent } from './table-row/table-row.component';
import { TableCellComponent } from './table-cell/table-cell.component';
import { TableComponent } from "app/table/table.component";
import { TbodyComponent } from './tbody/tbody.component';
import { TableEditDeleteComponent } from './tbody/table-edit-delete/table-edit-delete.component';
import { TheadComponent } from './thead/thead.component';

@NgModule({
  imports: [
    CommonModule,
    MdToolbarModule,
    MdButtonModule,
    MdDialogModule,
    MdInputModule
  ],
  declarations: [
    TableComponent,
    TableRowComponent,
    TableCellComponent,
    TbodyComponent,
    TableEditDeleteComponent,
    TheadComponent]
})
export class TableModule { }
