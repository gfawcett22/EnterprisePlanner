import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OrderListComponent } from './views/order-list.component';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [
    OrderListComponent
  ],
  exports: [
    OrderListComponent
  ]
})
export class OrdersModule { }
