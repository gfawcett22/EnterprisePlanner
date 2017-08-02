import { CustomerPagingResult } from '../models/customer-paging-result.interface';
import { Row } from '../../table/lib/row';
import { CustomerPagingParameters } from '../models/customer-paging-parameters.interface';
import { CustomerService } from '../services/customer.service';
import { Customer } from '../models/customer.interface';
import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { MdDialog } from '@angular/material';
import { CustomerEditComponent } from 'app/customers/views/customer-edit.component';
import { CustomerDetailComponent } from 'app/customers/views/customer-detail.component';
import { ITableSettings } from "app/table/lib/interfaces/ITableSettings";

@Component({
    selector: 'customer-list',
    templateUrl: './customer-list.component.html',
    styles: [
        `
        .mat-raised-button{
            color: black;
        }
        `
    ],
    changeDetection: ChangeDetectionStrategy.OnPush
})

export class CustomerListComponent implements OnInit {
    customers: Customer[] = [];
    totalCustomerCount: number = 0;
    pagingParameters: CustomerPagingParameters = {
        name: '',
        business: '',
        address: '',
        pageNumber: 1,
        pageSize: 25
    };

    settings: ITableSettings = {
        columns: {
            name: {title: 'Name', sortable: true, sortDirection: 'asc', filterable: true, currentSort: true},
            address: {title: 'Address', sortable: true, sortDirection: 'asc', filterable: true, currentSort: false},
            business: {title: 'Business', sortable: true, sortDirection: 'asc', filterable: true, currentSort: false}
        },
        sortColumn: 'Name',
        showActionButtons: true,
        noResultsMessage: 'No Results'
    };

    constructor(private customerService: CustomerService, public dialog: MdDialog, private cdr: ChangeDetectorRef) { }

    ngOnInit() {
        this.getCustomers();
        this.dialog.afterAllClosed.subscribe(() => this.onDialogClose());
    }

    getCustomers() {        
        this.customerService.getCustomers(this.pagingParameters)
            .subscribe(c => this.onCustomersReceived(c) , err => console.log(err));
    }

    onCustomersReceived(customerPagingResult: CustomerPagingResult) {
        this.customers = customerPagingResult.data || [];
        this.totalCustomerCount = customerPagingResult.totalResultCount;
        this.cdr.markForCheck();
    }

    openEditDialog(row: Row) {
        this.dialog.open(
            CustomerEditComponent,
            {
                data: row.getData().id,
                height: '50%',
                width: '60%'
            }
        );
    }

    openDetailDialog(row: Row) {
        this.dialog.open(
            CustomerDetailComponent,
            {
                data: row.getData().id
            }
        );
    }

    openCreateDialog() {
        this.dialog.open(
            CustomerEditComponent,
            {
                data: 0,
                height: '50%',
                width: '60%'
            }
        );
    }

    onDialogClose() {
        this.getCustomers();
    }

    filter($event): void {
        console.log($event);
        if($event) {
            Object.assign(this.pagingParameters, $event);
            this.getCustomers();
        }
    }

    sort($event): void {
        console.log($event);
    }

}