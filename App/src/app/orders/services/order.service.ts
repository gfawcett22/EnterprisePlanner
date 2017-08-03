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
import { Order } from 'app/orders/models/order.interface';
import { OrderPagingParameters } from 'app/orders/models/order-paging-parameters.interface';
import { OrderPagingResult } from '../models/order-paging-result.interface';

@Injectable()
export class OrderService extends BaseHttpService {
    private orderApiUrl = `${this.apiUrl}orders`;

    constructor(private http: Http, @Inject('API_URL') private apiUrl: string) { super(); }

    getOrders(params: OrderPagingParameters): Observable<OrderPagingResult> {
        const url = this.orderApiUrl + super.getQueryFromObject(params);
        return this.http.get(url)
            .map(super.extractData)
            .catch(super.handleError);
    }

    getOrder(id: number): Observable<Order>{
        if (id === 0) {
            return Observable.of(this.initializeOrder());
        };
        const url = `${this.orderApiUrl}/${id}`;
        return this.http.get(url)
            .map(super.extractData)
            //.do(order => console.log(order))
            .catch(super.handleError);
    }

    saveOrder(order: Order): Observable<Order> {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        const options = new RequestOptions({ headers: headers });
        console.log(order);
        if (order.id === 0){
            return this.createOrder(order, options);
        }
        return this.updateOrder(order, options);
    }

    createOrder(order:Order, options: RequestOptions): Observable<Order> {
        order.id = undefined;
        console.log(this.orderApiUrl);
        return this.http.post(this.orderApiUrl, order)
            .map(this.extractData)
            .catch(super.handleError);
    }

    updateOrder(order: Order, options: RequestOptions): Observable<Order> {
        const url = `${this.orderApiUrl}/${order.id}`;
        return this.http.put(url, order, options)
            .map(super.extractData)
            .catch(super.handleError);
    }

    deleteOrder(id: number): Observable<Response> {
        const url = `${this.orderApiUrl}/${id}`;
        return this.http.delete(url);
    }

    initializeOrder(): Order {
        return {
            id: 0,
            customerId: null,
            customerName: '',
            datePlaced: null
        };
    }
}