import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {MdToolbarModule} from '@angular/material';

@NgModule({
  imports: [ CommonModule ],
  exports : [
    CommonModule,
    FormsModule,
    ReactiveFormsModule
  ],
  declarations: [ ],
})
export class SharedModule { }