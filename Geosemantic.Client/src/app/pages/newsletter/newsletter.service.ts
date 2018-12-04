import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable()
export class NewsletterService implements Resolve<any>
{
    data: any;
    dataOnChanged: BehaviorSubject<any>;

    /**
     * Constructor
     *
     * @param {HttpClient} _httpClient
     */
    constructor(
        private _httpClient: HttpClient
    )
    {
        // Set the defaults
        this.dataOnChanged = new BehaviorSubject({});
    }

    /**
     * Resolver
     *
     * @param {ActivatedRouteSnapshot} route
     * @param {RouterStateSnapshot} state
     * @returns {Observable<any> | Promise<any> | any}
     */
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<any> | Promise<any> | any
    {
        return new Promise((resolve, reject) => {
            Promise.all([
                this.getSearchData()
            ]).then(
                () => {
                    resolve();
                },
                reject
            );
        });
    }

    /**
     * Get search data
     */
    getSearchData(): Promise<any[]>
    {
        return new Promise((resolve, reject) => {

            this._httpClient.get('https://newsapi.org/v2/everything?q=bitcoin&from=2018-12-04&sortBy=publishedAt&apiKey=202f8b0c7f70442faa847a8035fb3753')
                .subscribe((data: any) => {
                    this.data = data;
                    this.dataOnChanged.next(this.data.articles);
                    resolve(this.data);
                }, reject);
        });
    }
}
