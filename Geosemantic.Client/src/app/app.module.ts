import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule ,HTTP_INTERCEPTORS} from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule, Routes } from '@angular/router';
import { MatMomentDateModule } from '@angular/material-moment-adapter';
import { MatButtonModule, MatIconModule } from '@angular/material';
import { TranslateModule } from '@ngx-translate/core';
import { NgHttpLoaderModule } from 'ng-http-loader';
import { ToastrModule } from 'ngx-toastr';
import 'hammerjs';

import { FuseModule } from '@fuse/fuse.module';
import { FuseSharedModule } from '@fuse/shared.module';
import { FuseProgressBarModule, FuseSidebarModule } from '@fuse/components';

import { fuseConfig } from 'app/fuse-config';

import { AppComponent } from 'app/app.component';
import { LayoutModule } from 'app/layout/layout.module';
import { SharedModule } from "./shared/shared.module";
// Import routing module
import { AppRoutingModule } from './app.routing';
import { AuthenticationService,UserSessionService,AlertService,HttpInterceptorService,DataService,UserPageSessionService,NavigationService,OrganisationPageSessionService,PageService} from 'app/services'
import { AuthGuard } from './guards';
import {OrganisationService} from '../app/pages/organisation/organisation.service';
import { ConfirmationModalComponent } from './shared/component/modalcomponent/confirmationmodal.component';
import { GenericMessageModalComponent } from './shared/component/modalcomponent/genericmessagemodal.component';
import { SpinnerComponent } from 'app/shared/component/spinnercomponent/spinner.component';
import { RoleModule } from './pages/role/role.module';
import { RoleComponent } from './pages/role/role.component';

const appRoutes: Routes = [
    {
        path      : '**',
        redirectTo: 'sample'
    }
];

@NgModule({
    declarations: [
        AppComponent
    ],
    imports     : [
        BrowserModule,
        BrowserAnimationsModule,
        HttpClientModule,
        AppRoutingModule,
        NgHttpLoaderModule,
        RouterModule.forRoot(appRoutes),

        TranslateModule.forRoot(),

        // Material moment date module
        MatMomentDateModule,

        // Material
        MatButtonModule,
        MatIconModule,

        // Fuse modules
        FuseModule.forRoot(fuseConfig),
        FuseProgressBarModule,
        FuseSharedModule,
        FuseSidebarModule,
        // App modules
        LayoutModule,
        SharedModule,
        RoleModule,
        // Toast modules
        ToastrModule.forRoot({
            timeOut: 5000,
            positionClass: 'toast-top-right',
            preventDuplicates: true,
          })
    ],
    providers: [AuthenticationService,
        AuthGuard,
        AlertService,
        UserSessionService,
        DataService,
        PageService,
        OrganisationPageSessionService,
        OrganisationService,
        NavigationService,
        UserPageSessionService,{
        provide: HTTP_INTERCEPTORS,
        useClass: HttpInterceptorService,
        multi: true
      }],
    bootstrap   : [
        AppComponent
    ],
    entryComponents: [
        RoleComponent,
        SpinnerComponent,
        ConfirmationModalComponent,
        GenericMessageModalComponent]
})
export class AppModule
{
}
