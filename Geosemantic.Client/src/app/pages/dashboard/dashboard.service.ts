import { Injectable } from '@angular/core';
import { HttpClient ,HttpHeaders} from '@angular/common/http';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { AnalyticsDashboardDb } from 'app/pages/dashboard/dashboard-analytics';
import { environment } from '../../../environments/environment';

@Injectable()
export class DashboardService implements Resolve<any>
{
    widgets: any;
    private baseUrl = environment.apiBaseUrl;
    getDashboardRoute: string = "/api/leads";
    
    /**
     * Constructor
     *
     * @param {HttpClient} _httpClient
     */
    constructor(
        private _httpClient: HttpClient
    )
    {
        
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
                this.getWidgets()
            ]).then(
                () => {
                    resolve();
                },
                reject
            );
        });
    }


    /**
     * Get widgets
     *
     * @returns {Promise<any>}
     */
    getWidgets(): Promise<any>
    {
        this.widgets = AnalyticsDashboardDb.widgets;
        return null;
        // let headers = new HttpHeaders();
        // headers = headers.set('Content-Type', 'application/json; charset=utf-8');
        // return new Promise((resolve, reject) => {
        //     this._httpClient.get(this.baseUrl + this.getDashboardRoute,{headers})
        //         .subscribe((response: any) => {
        //             this.widgets = AnalyticsDashboardDb.widgets;
        //             resolve(response);
        //         }, reject);
        // });
    }
    
}
