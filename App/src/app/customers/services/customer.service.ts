import { Injectable, Inject } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

@Injectable()
export class CustomerService {
    constructor(private http: Http, @Inject('CUSTOMER_API_URL') private customerApiUrl: string) { }

    
}