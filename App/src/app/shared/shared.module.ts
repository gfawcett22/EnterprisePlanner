import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {MdToolbarModule, MdButtonModule, MdDialogModule, MdInputModule} from '@angular/material';

@NgModule({
  imports: [ CommonModule ],
  exports : [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MdToolbarModule,
    MdButtonModule,
    MdDialogModule,
    MdInputModule
  ],
  declarations: [ ],
})
export class SharedModule { }