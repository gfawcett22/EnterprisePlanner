import { OrderEditComponent } from './order-edit.component';
import { Row } from '../../table/lib/row';
import { OrderPagingResult } from '../models/order-paging-result.interface';
import { OrderService } from '../services/order.service';
import { OrderPagingParameters } from '../models/order-paging-parameters.interface';
import { Order } from '../models/order.interface';
import { ITableSettings } from '../../table/lib/interfaces/ITableSettings';
import { Component, OnInit } from '@angular/core';
import { MdDialog } from "@angular/material";

@Component({
  selector: 'order-list',
  templateUrl: './order-list.component.html',
  styles: []
})
export class OrderListComponent implements OnInit {
    orders: Order[] = [];
    totalOrderCount: number = 0;
    pagingParameters: OrderPagingParameters = {
        id: 0,
        customerId: 0,
        customerName: '',
        datePlaced: null,
        pageNumber: 1,
        pageSize: 25
    };

  settings: ITableSettings = {
        columns: {
            id: {title: 'ID', sortable: true, sortDirection: 'asc', filterable: true, currentSort: true},
            customerId: {title: 'Customer ID', sortable: true, sortDirection: 'asc', filterable: true, currentSort: false},
            customerName: {title: 'Customer Name', sortable: true, sortDirection: 'asc', filterable: true, currentSort: false},
            datePlaced: {title: 'Date Placed', sortable: true, sortDirection: 'asc', filterable: true, currentSort: false}
        },
        sortColumn: 'Name',
        showActionButtons: true,
        noResultsMessage: 'No Results'
    };
    
  constructor(private orderService: OrderService, public dialog: MdDialog) { }

  ngOnInit() {
        this.getOrders();
        this.dialog.afterAllClosed.subscribe(() => this.onDialogClose());
    }

    getOrders() {        
        this.orderService.getOrders(this.pagingParameters)
            .subscribe(c => this.onOrdersReceived(c) , err => console.log(err));
    }

    onOrdersReceived(orderPagingResult: OrderPagingResult) {
        this.orders = orderPagingResult.data || [];
        this.totalOrderCount = orderPagingResult.totalResultCount;
    }

    openEditDialog(row: Row) {
        this.dialog.open(
            OrderEditComponent,
            {
                data: row.getData().id,
                height: '50%',
                width: '60%'
            }
        );
    }

    openDetailDialog(row: Row) {
        // this.dialog.open(
        //     OrderDetailComponent,
        //     {
        //         data: row.getData().id
        //     }
        // );
    }

    openCreateDialog() {
        this.dialog.open(
            OrderEditComponent,
            {
                data: 0,
                height: '50%',
                width: '60%'
            }
        );
    }

    onDialogClose() {
        this.getOrders();
    }

    filter($event): void {
        console.log($event);
        if($event) {
            Object.assign(this.pagingParameters, $event);
            this.getOrders();
        }
    }

    sort($event): void {
        console.log($event);
    }


}
