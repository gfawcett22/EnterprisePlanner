import { TBodyModule } from './tbody/tbody.module';
import { SharedModule } from '../shared/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TableComponent } from "app/table/table.component";
import { TableFilterComponent } from './table-filter/table-filter.component';
import { THeadModule } from "app/table/thead/thead.module";

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    THeadModule,
    TBodyModule,
  ],
  declarations: [
    TableComponent,
  ],
  exports: [
    TableComponent
  ]
})
export class TableModule { }
