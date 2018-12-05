import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import * as _ from 'lodash';
import { AuthenticationService,UserSessionService,NavigationService} from 'app/services';
import { FuseSidebarService } from '@fuse/components/sidebar/sidebar.service';


@Component({
    selector     : 'toolbar',
    templateUrl  : './toolbar.component.html',
    styleUrls    : ['./toolbar.component.scss'],
    encapsulation: ViewEncapsulation.None
})

export class ToolbarComponent implements OnInit, OnDestroy
{
    userName:any;
    constructor(
        private authService: AuthenticationService,
        private fuseSidebarService: FuseSidebarService,
        private userSessionService:UserSessionService,
        private navigationService:NavigationService
    )
    {
        this.userName= userSessionService.userFullName();
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------

    /**
     * On init
     */
    ngOnInit(): void
    {
    }

    ngOnDestroy(): void
    {
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Public methods
    // -----------------------------------------------------------------------------------------------------

    /**
     * Toggle sidebar open
     *
     * @param key
     */
    toggleSidebarOpen(key): void
    {
        this.fuseSidebarService.getSidebar(key).toggleOpen();
    }

    onLogout() {
        this.authService.logOut();
        this.navigationService.goToLogin();
      }

     

      
}
