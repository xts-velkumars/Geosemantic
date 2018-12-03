import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';



export const appRoutes: Routes = [
    {
        path        : 'dashboard',
        loadChildren: './pages/dashboard/dashboard.module#DashboardModule'
    },{
        path        : 'users',
        loadChildren: './pages/users/users.module#UsersModule'
    },{
        path        : 'user/:id',
        loadChildren: './pages/user/user.module#UserModule'
    },{
        path        : 'role/:id',
        loadChildren: './pages/role/role.module#RoleModule'
    },{
        path        : 'contacts',
        loadChildren: './pages/contacts/contacts.module#ContactsModule'
    },{
        path        : 'forms',
        loadChildren: './pages/forms/forms.module#FormsModule'
    },{
        path        : 'questions',
        loadChildren: './pages/questions/questions.module#QuestionsModule'
    },{
        path        : 'chats',
        loadChildren: './pages/chats/chats.module#ChatsModule'
    },{
        path        : 'reports',
        loadChildren: './pages/reports/reports.module#ReportsModule'
    },{
        path        : 'setting',
        loadChildren: './pages/settings/settings.module#SettingsModule'
    },{
        path        : 'register',
        loadChildren: './pages/register/register.module#RegisterModule'
    },{
        path        : 'login',
        loadChildren: './pages/login/login.module#LoginModule'
    },{
        path        : 'forgot-password',
        loadChildren: './pages/forgot-password/forgot-password.module#ForgotPasswordModule'
    },{
        path        : 'organisation',
        loadChildren: './pages/organisation/organisation.module#OrganisationModule'
    },{
        path      : '**',
        redirectTo: 'dashboard'
    }
];


@NgModule({
  imports: [RouterModule.forRoot(appRoutes)],
  exports: [RouterModule],

})
export class AppRoutingModule { }
