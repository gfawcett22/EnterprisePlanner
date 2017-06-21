import { CustomerPagingParameters } from './models/customer-paging-parameters.interface';
import { CustomerService } from './customer.service';
import { Customer } from './models/customer.interface';
import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'customer-list',
    template: 
    `
    <customer-item *ngFor="let customer of customers" [customer]="customer"></customer-item>
    `
})

export class CustomerListComponent implements OnInit {
    customers: Customer[];
    nameFilter: string = "";
    addressFilter: string = "";
    businessFilter: string = "";
    pageNumberFilter: number = 1;
    pageSizeFilter: number = 25;

    constructor(private customerService:CustomerService) { }

    ngOnInit() { 
        this.getCustomers();
    }

    getCustomers() {
        let params: CustomerPagingParameters = {
            name: this.nameFilter,
            address: this.addressFilter,
            business: this.businessFilter,
            pageNumber: this.pageNumberFilter,
            pageSize: this.pageSizeFilter
        }
        this.customerService.getCustomers(params)
            .subscribe(customers => this.customers = customers, err => console.log(err));
    }

}