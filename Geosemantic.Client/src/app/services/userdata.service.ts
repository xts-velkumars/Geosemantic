import { Injectable } from '@angular/core';
import { DataService } from "./data.service";


@Injectable()
export class UserService {

    users: any;
    getUserRoute: string = "/api/users";

    constructor(private dataService: DataService) {
    }


    getUsers(refresh) {
        return this.dataService.getData(this.getUserRoute, refresh);
    }


    deleteUser(id) {
        return this.dataService.delete('/api/user/' + id + '/delete');
    }

    approved(user) {
        return this.dataService.post('/api/user/approved', user);
    }

    saveUser(user) {
        return this.dataService.post('/api/users', user).map(response => {
            this.dataService.clearRouteCache(this.getUserRoute);
            return response;
        });
    }

    getRoles(orgId, refresh: Boolean) {
        return this.dataService.getData('/api/roleslookup', refresh);
    }


}
