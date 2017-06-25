import { Injectable, Inject } from '@angular/core';
import { Http, Headers, Response, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/filter';
import 'rxjs/add/observable/throw';
import 'rxjs/add/operator/map';
import 'rxjs/add/observable/of';

import { BaseHttpService } from 'app/services/baseHttp.service';
import { Customer } from 'app/customers/models/customer.interface';
import { CustomerPagingParameters } from 'app/customers/models/customer-paging-parameters.interface';

@Injectable()
export class CustomerService extends BaseHttpService {
    private customerApiUrl = `${this.apiUrl}customers`;

    constructor(private http: Http, @Inject('API_URL') private apiUrl: string) { super(); }

    getCustomers(params: CustomerPagingParameters): Observable<Customer[]> {
        return this.http.get(this.customerApiUrl)
            .map(super.extractData)
            .catch(super.handleError);
    }

    getCustomer(id: number): Observable<Customer>{
        const url = `${this.customerApiUrl}/${id}`;
        return this.http.get(url)
            .map(super.extractData)
            .catch(super.handleError);
    }

    saveCustomer(customer: Customer): Observable<Customer> {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        const options = new RequestOptions({ headers: headers });

        if (customer.id === 0){
            return this.createCustomer(customer, options);
        }
        return this.updateCustomer(customer, options);
    }

    createCustomer(customer:Customer, options: RequestOptions): Observable<Customer> {
        customer.id = undefined;
        return this.http.post(this.customerApiUrl, customer)
            .map(this.extractData)
            .catch(super.handleError);
    }

    updateCustomer(customer: Customer, options: RequestOptions): Observable<Customer> {
        const url = `${this.customerApiUrl}/${customer.id}`;
        return this.http.put(url, customer, options)
            .map(super.extractData)
            .catch(super.handleError);
    }

    deleteCustomer(id: number): Observable<Response> {
        const url = `${this.customerApiUrl}/${id}`;
        return this.http.delete(url);
    }
}