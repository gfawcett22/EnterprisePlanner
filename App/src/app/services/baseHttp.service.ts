import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Response } from '@angular/http';
import 'rxjs/add/observable/throw';

export class BaseHttpService {

    constructor() { }
    protected extractData(response: Response) {
        const body = response.json();
        return body || {};
    }
    protected handleError(error) {
        // send error to server for logging
        console.log(error);
        return Observable.throw(error.json().error || 'Server error');
    }

    protected getQueryFromObject(obj: any): string {
        console.log(obj);
        let returnStr = "";
        if (typeof (obj) === "object") {
            let count = 0;
            //loop through the object properties and add to string           
            for (let prop in obj) 
            {
                if (obj[prop] != undefined && obj[prop] !== "") {
                    if (count === 0) returnStr += '?';
                    else returnStr += '&'
                    returnStr += `${prop}=${obj[prop]}`;
                    ++count;
                } 
            }
        }
        return returnStr;
    }
}