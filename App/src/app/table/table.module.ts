import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {MdToolbarModule, MdButtonModule, MdDialogModule, MdInputModule} from '@angular/material';
import { TableComponent } from './table.component';

@NgModule({
  imports: [
    CommonModule,
    MdToolbarModule, 
    MdButtonModule, 
    MdDialogModule, 
    MdInputModule
  ],
  declarations: [TableComponent]
})
export class TableModule { }
