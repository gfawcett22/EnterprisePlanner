import { CustomerPagingParameters } from './models/customer-paging-parameters.interface';
import { Injectable, Inject } from '@angular/core';
import { Http } from "@angular/http";
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

import { BaseHttpService } from '../services/baseHttp.service';
import { Customer } from './models/customer.interface';

@Injectable()
export class CustomerService extends BaseHttpService {
    constructor(private http: Http, @Inject('API_URL') private apiUrl: string) { super(); }

    getCustomers(pagingParams: CustomerPagingParameters): Observable<Customer[]> {
        const url = this.apiUrl + 'customers' + super.getQueryFromObject(pagingParams);
        console.log(url);
        return this.http.get(url)
            .map(super.extractData)
            .catch(super.handleError);
    }

    getCustomer(id: number): Observable<Customer> {
        const url = this.apiUrl + 'customers/' + id.toString();
        return this.http.get(url)
            .map(super.extractData)
            .catch(super.handleError);
    }
}