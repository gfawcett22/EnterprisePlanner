import { CustomerPagingParameters } from '../models/customer-paging-parameters.interface';
import { CustomerService } from '../services/customer.service';
import { Customer } from '../models/customer.interface';
import { Component, OnInit } from '@angular/core';
import { MdDialog } from '@angular/material';
import { CustomerEditComponent } from 'app/customers/views/customer-edit.component';
import { CustomerDetailComponent } from 'app/customers/views/customer-detail.component';
import { ITableSettings } from "app/lib/interfaces/ITableSettings";

@Component({
    selector: 'customer-list',
    templateUrl: './customer-list.component.html',
    styles: [
        `
        .mat-raised-button{
            color: black;
        }
        `
    ]
})

export class CustomerListComponent implements OnInit {
    customers: Customer[];
    nameFilter = '';
    addressFilter = '';
    businessFilter = '';
    pageNumberFilter = 1;
    pageSizeFilter = 25;

    settings: ITableSettings = {
        columns: [
            {title: 'Name', sortable: true, sortDirection: 'asc', filterable: true, currentSort: false},
            {title: 'Address', sortable: true, sortDirection: 'asc', filterable: true, currentSort: false},
            {title: 'Business', sortable: true, sortDirection: 'asc', filterable: true, currentSort: false}
        ],
        sortColumn: 'Name',
        showActionButtons: true,
        rows: this.customers
    };

    constructor(private customerService: CustomerService, public dialog: MdDialog) { }

    ngOnInit() {
        this.getCustomers();
    }

    getCustomers() {
        const params: CustomerPagingParameters = {
            name: this.nameFilter,
            address: this.addressFilter,
            business: this.businessFilter,
            pageNumber: this.pageNumberFilter,
            pageSize: this.pageSizeFilter
        };
        this.customerService.getCustomers(params)
            .subscribe(customers => this.customers = customers, err => console.log(err));
    }

    openEditDialog(id: number) {
        debugger;
        this.dialog.open(
            CustomerEditComponent,
            {
                data: id,
                height: '50%',
                width: '60%'
            }
        );
    }

    openDetailDialog(id: number) {
        debugger;
        this.dialog.open(
            CustomerDetailComponent,
            {
                data: id
            }
        );
    }

}