import { Component, OnInit, Output, Inject, EventEmitter } from '@angular/core';
import { Customer } from 'app/customers/models/customer.interface';
import { MD_DIALOG_DATA } from '@angular/material';
import { CustomerService } from 'app/customers/services/customer.service';

@Component({
    selector: 'customer-detail',
    templateUrl: 'customer-detail.component.html'
})

export class CustomerDetailComponent implements OnInit {

    customer: Customer;
    @Output() closeDialog = new EventEmitter();
    constructor(@Inject(MD_DIALOG_DATA) public data: any, public customerService: CustomerService) { }

    ngOnInit() {
        const id = +this.data;
        this.getCustomer(id);
    }

    getCustomer(id: number) {
        return this.customerService.getCustomer(id).subscribe(customer => this.customer = customer);
    }

    closeDialogEmit() {
        this.closeDialog.emit();
    }
}