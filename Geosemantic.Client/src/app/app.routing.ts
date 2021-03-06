import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';



export const appRoutes: Routes = [
    {
        path: 'dashboard',
        loadChildren: './pages/dashboard/dashboard.module#DashboardModule'
    }, {
        path: 'users',
        loadChildren: './pages/users/users.module#UsersModule'
    }, {
        path: 'register',
        loadChildren: './pages/register/register.module#RegisterModule'
    }, {
        path: 'login',
        loadChildren: './pages/login/login.module#LoginModule'
    }, {
        path: 'newsfeed',
        loadChildren: './pages/newsletter/newsletter.module#NewsletterModule'
    }, {
        path: 'awaitingapproval',
        loadChildren: './pages/awaitingapproval/awaitingapproval.module#AwaitingApprovalModule'
    }, {
        path: 'underreview',
        loadChildren: './pages/underreview/underreview.module#UnderReviewModule'
    }, {
        path: '**',
        redirectTo: 'dashboard'
    }
];


@NgModule({
    imports: [RouterModule.forRoot(appRoutes)],
    exports: [RouterModule],

})
export class AppRoutingModule { }
