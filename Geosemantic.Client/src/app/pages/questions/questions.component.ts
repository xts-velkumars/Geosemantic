import { Component, OnInit } from '@angular/core';
import { fuseAnimations } from '@fuse/animations';

@Component({
    selector     : 'questions-dashboard',
    templateUrl  : './questions.component.html',
    styleUrls    : ['./questions.component.scss'],
    animations   : fuseAnimations
})
export class QuestionsComponent implements OnInit
{
   
    constructor()
    {
      
    }
    
    ngOnInit(): void
    {
       
    }

}


