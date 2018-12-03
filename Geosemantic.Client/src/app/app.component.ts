import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { DOCUMENT } from '@angular/common';
import { Platform } from '@angular/cdk/platform';
import { FuseNavigationService } from '@fuse/components/navigation/navigation.service';
import { FuseSidebarService } from '@fuse/components/sidebar/sidebar.service';
import { FuseSplashScreenService } from '@fuse/services/splash-screen.service';
import { FuseConfigService } from '@fuse/services/config.service';
import { SpinnerComponent } from "./shared/component/spinnercomponent/spinner.component";
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { Spinkit } from 'ng-http-loader';

@Component({
    selector   : 'app',
    templateUrl: './app.component.html',
    styleUrls  : ['./app.component.scss']
})
export class AppComponent implements OnInit, OnDestroy
{
    fuseConfig: any;
    private unsubscribeAll: Subject<any>;
    spinnerComponent = SpinnerComponent;
    public spinkit = Spinkit;

    constructor(
        @Inject(DOCUMENT) private document: any,
        private fuseConfigService: FuseConfigService,
        private fuseNavigationService: FuseNavigationService,
        private fuseSidebarService: FuseSidebarService,
        private fuseSplashScreenService: FuseSplashScreenService,
        private platform: Platform
    )
    {
       
        // Add is-mobile class to the body if the platform is mobile
        if ( this.platform.ANDROID || this.platform.IOS )
        {
            this.document.body.classList.add('is-mobile');
        }
        this.unsubscribeAll = new Subject();
    }

    
    ngOnInit(): void
    {
        this.fuseConfigService.config
            .pipe(takeUntil(this.unsubscribeAll))
            .subscribe((config) => {

                this.fuseConfig = config;

                // Boxed
                if ( this.fuseConfig.layout.width === 'boxed' )
                {
                    this.document.body.classList.add('boxed');
                }
                else
                {
                    this.document.body.classList.remove('boxed');
                }

                // Color theme - Use normal for loop for IE11 compatibility
                for ( let i = 0; i < this.document.body.classList.length; i++ )
                {
                    const className = this.document.body.classList[i];

                    if ( className.startsWith('theme-') )
                    {
                        this.document.body.classList.remove(className);
                    }
                }

                this.document.body.classList.add(this.fuseConfig.colorTheme);
            });
    }

    ngOnDestroy(): void
    {
        this.unsubscribeAll.next();
        this.unsubscribeAll.complete();
    }

    
    toggleSidebarOpen(key): void
    {
        this.fuseSidebarService.getSidebar(key).toggleOpen();
    }
}
