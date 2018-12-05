import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { BehaviorSubject } from 'rxjs';


@Injectable()
export class NewsletterService 
{
    data: any;
    dataOnChanged: BehaviorSubject<any>;

    constructor(private _httpClient: HttpClient)
    {
       
        this.dataOnChanged = new BehaviorSubject({});
    }
   
    getSearchData(date): Promise<any[]>
    {
        return new Promise((resolve, reject) => {

            this._httpClient.get('https://newsapi.org/v2/everything?q=bitcoin&from='+date+'&to='+date+'&sortBy=publishedAt&apiKey=202f8b0c7f70442faa847a8035fb3753')
                .subscribe((data: any) => {
                    this.data = data;
                    this.dataOnChanged.next(this.data.articles);
                    resolve(this.data);
                }, reject);
        });
    }

    getNewsfeed(date) {
        return new Promise((resolve, reject) => {
            Promise.all([
                this.getSearchData(date)
            ]).then(
                () => {
                    resolve();
                },
                reject
            );
        });
    }
}
