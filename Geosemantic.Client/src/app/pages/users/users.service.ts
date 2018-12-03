import { Injectable } from '@angular/core';
import { BehaviorSubject,Observable } from 'rxjs';
import { DataService } from "app/services";

@Injectable()
export class UsersService 
{
    users: any;
    getUserRoute: string = "/api/users";
    getRoleRoute: string = "/api/roles";
    
    constructor( private dataService: DataService)
    {
    }
    getUsers(orgId,refresh)
    {
        return this.dataService.getData(this.getUserRoute+"/"+orgId, refresh);
    }

    getRoles(orgId,refresh)
    {
        return this.dataService.getData(this.getRoleRoute+"/"+orgId, refresh);
    }
}
