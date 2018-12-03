import { Injectable } from '@angular/core';
import { DataService } from "app/services";

@Injectable()
export class QuestionsService 
{
    getFormsRoute: string = "/api/forms";
    
    constructor( private dataService: DataService)
    {
    }

    
    getForms(orgId,refresh)
    {
        return this.dataService.getData(this.getFormsRoute+"/"+orgId, refresh);
    }
}
