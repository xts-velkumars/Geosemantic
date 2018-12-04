import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { NewsletterService } from './newsletter.service';


@Component({
    selector     : 'newsletter',
    templateUrl  : './newsletter.component.html',
    styleUrls    : ['./newsletter.component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class NewsletterComponent implements OnInit, OnDestroy
{
    searchItems: any;

    // Private
    private _unsubscribeAll: Subject<any>;

    /**
     * Constructor
     *
     * @param {SearchModernService} _searchModernService
     */
    constructor(
        private newsletterService: NewsletterService
    )
    {
        // Set the private defaults
        this._unsubscribeAll = new Subject();
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------

    /**
     * On init
     */
    ngOnInit(): void
    {
        this.newsletterService.dataOnChanged
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe(searchItems => {
                this.searchItems = searchItems;
            });
    }

    /**
     * On destroy
     */
    ngOnDestroy(): void
    {
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
    }
}
