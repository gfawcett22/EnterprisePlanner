import { RouterModule } from '@angular/router';
import { OrderService } from './services/order.service';
import { TableModule } from '../table/table.module';
import { SharedModule } from '../shared/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OrderListComponent } from './views/order-list.component';
import { OrderEditComponent } from "app/orders/views/order-edit.component";

@NgModule({
  imports: [
    SharedModule,
    TableModule,
    RouterModule.forChild([
      {path: '', component: OrderListComponent},
      //{path: ':id', component: CustomerDetailComponent},
      {path: ':id/edit', component: OrderEditComponent}
    ]),
  ],
  declarations: [
    OrderListComponent,
    OrderEditComponent
  ],
  exports: [
    OrderListComponent
  ],
  providers: [
    OrderService
  ]
})
export class OrdersModule { }
