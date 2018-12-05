import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import {formatDate} from '@angular/common';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { NewsletterService } from '../../services/newsletter.service';

@Component({
    selector     : 'newsletter',
    templateUrl  : './newsletter.component.html',
    styleUrls    : ['./newsletter.component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class NewsletterComponent implements OnInit, OnDestroy
{
    searchItems: any;
   filteredItems: any;
   date: string;
    // Private
    private _unsubscribeAll: Subject<any>;

    
    constructor(private newsletterService: NewsletterService)
    {
        
        this._unsubscribeAll = new Subject();
    }

   
    ngOnInit(): void
    {
        this.onToday();
    }

    
    ngOnDestroy(): void
    {
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
    }
    assignCopy(){
        this.filteredItems = Object.assign([], this.searchItems);
     }
     filterItem(value){
        if(!value) this.assignCopy(); //when nothing has typed
         this.filteredItems = Object.assign([], this.searchItems).filter(
             item => item.source.name.toLowerCase().indexOf(value.toLowerCase()) > -1
         );
     }

     onDayago(){
       var dayago = new Date();
       dayago.setDate(dayago.getDate() - 1);
       this.date=formatDate(dayago, 'yyyy-MM-dd', 'en');
       this.newsletterService.getNewsfeed(this.date);
       this.newsletterService.dataOnChanged
       .pipe(takeUntil(this._unsubscribeAll))
       .subscribe(searchItems => {
           this.searchItems = searchItems;
           this.assignCopy();
       });
    }
    onToday(){
       this.date=formatDate(new Date(), 'yyyy-MM-dd', 'en');
       this.newsletterService.getNewsfeed(this.date);
       this.newsletterService.dataOnChanged
       .pipe(takeUntil(this._unsubscribeAll))
       .subscribe(searchItems => {
           this.searchItems = searchItems;
           this.assignCopy();
       });

    }
     
}
