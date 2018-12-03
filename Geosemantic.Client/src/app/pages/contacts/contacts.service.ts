import { Injectable } from '@angular/core';
import { BehaviorSubject,Observable } from 'rxjs';
import { DataService } from "app/services";

@Injectable()
export class ContactsService 
{
    contacts: any;
    getUserRoute: string = "/api/users";
    
    constructor( private dataService: DataService)
    {
    }

    
    getUsers(orgId,refresh)
    {
        return this.dataService.getData(this.getUserRoute+"/"+orgId, refresh);
    }
}
