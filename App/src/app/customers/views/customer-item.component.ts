import { Component, OnInit, Input } from '@angular/core';
import { Customer } from "app/customers/models/customer.interface";

@Component({
  selector: 'customer-item',
  template: `
    <p>
      customer-item Works!
    </p>
  `,
  styles: []
})
export class CustomerItemComponent implements OnInit {
  @Input() customer: Customer;
  constructor() { }

  ngOnInit() {
  }

}
