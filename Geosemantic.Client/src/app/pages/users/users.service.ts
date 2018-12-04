import { Injectable } from '@angular/core';
import { BehaviorSubject,Observable } from 'rxjs';
import { DataService } from "app/services";

@Injectable()
export class UsersService 
{
    users: any;
    getUserRoute: string = "/api/users";    
    
    constructor( private dataService: DataService)
    {
    }
    getUsers(refresh)
    {
        return this.dataService.getData(this.getUserRoute, refresh);
    }

    
}
