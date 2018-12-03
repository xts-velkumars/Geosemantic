import { Component, OnInit } from '@angular/core';
import { fuseAnimations } from '@fuse/animations';

@Component({
    selector     : 'chats-dashboard',
    templateUrl  : './chats.component.html',
    styleUrls    : ['./chats.component.scss'],
    animations   : fuseAnimations
})
export class ChatsComponent implements OnInit
{
   
    constructor()
    {
      
    }
    
    ngOnInit(): void
    {
       
    }

}


