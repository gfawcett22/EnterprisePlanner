import { Injectable, Inject } from '@angular/core';
import { Http, Headers, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/filter';
import 'rxjs/add/observable/throw';
import 'rxjs/add/operator/map';
import 'rxjs/add/observable/of';

import { BaseHttpService } from "app/services/baseHttp.service";
import { Customer } from "app/customers/models/customer.interface";

@Injectable()
export class CustomerService extends BaseHttpService {
    constructor(private http: Http, @Inject('API_URL') private customerApiUrl: string) { super(); }

    getCustomers(): Observable<Customer[]> {
        return this.http.get(this.customerApiUrl)
            .map(super.extractData)
            .catch(super.handleError);
    }
    getCustomer(id: number): Observable<Customer>{
        const url = this.customerApiUrl + '/' + id;
        return this.http.get(url)
            .map(super.extractData)
            .catch(super.handleError)
    }
    createCustomer(customer:Customer): Observable<Customer>{
        customer.id = undefined;
        return this.http.post(this.customerApiUrl, customer)
            .map(this.extractData)
            .catch(super.handleError);
    }
    deleteCustomer(id: number): Observable<Response> {
        return this.http.delete(this.customerApiUrl + '/' + id);
    }
}