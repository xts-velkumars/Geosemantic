import { Component, OnInit } from '@angular/core';
import { fuseAnimations } from '@fuse/animations';

@Component({
    selector     : 'reports-dashboard',
    templateUrl  : './reports.component.html',
    styleUrls    : ['./reports.component.scss'],
    animations   : fuseAnimations
})
export class ReportsComponent implements OnInit
{
   
    constructor()
    {
      
    }
    
    ngOnInit(): void
    {
       
    }

}


