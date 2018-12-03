import { Injectable } from '@angular/core';
import { BehaviorSubject,Observable } from 'rxjs';
import { DataService } from "app/services";

@Injectable()
export class OrganisationService 
{
    users: any;
    getOrganisationRoute: string = "/api/organisation";
    onProductsChanged: BehaviorSubject<any>;

    /**
     * Constructor
     *
     * @param {HttpClient} _httpClient
     */
    constructor( private dataService: DataService)
    {
         // Set the defaults
         this.onProductsChanged = new BehaviorSubject({});
    }

    

    /**
     * Get Users
     *
     * 
     */
    getOrganisation(id,refresh: Boolean)
    {
        return this.dataService.getData(this.getOrganisationRoute+"/"+id, refresh);
    }
}
