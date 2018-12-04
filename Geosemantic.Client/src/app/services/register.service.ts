import { Injectable } from '@angular/core';
import { DataService } from "./data.service";


@Injectable()
export class RegisterService {

  constructor(
    private dataService: DataService) { }


    newuserregister(userdetails) {
      debugger;
      return this.dataService.post('/api/user', userdetails);
  }
}
