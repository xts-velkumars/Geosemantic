import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {
    MatButtonModule, MatFormFieldModule, MatIconModule, MatInputModule, MatPaginatorModule,
    MatRippleModule, MatSelectModule, MatSnackBarModule, MatSortModule,
    MatTableModule,  MatCardModule, MatTooltipModule} from '@angular/material';

import { FuseSharedModule } from '@fuse/shared.module';

import { UsersComponent } from 'app/pages/users/users.component';
import { UserService } from "../../services/userdata.service";
import { AuthGuard } from 'app/guards/auth.guard';
const routes: Routes = [
    {
        path     : '**',
        component: UsersComponent,canActivate: [AuthGuard],
        data: {
            title: 'Users'
          }
    }
];

@NgModule({
    declarations: [
        UsersComponent
    ],
    imports     : [
        RouterModule.forChild(routes),

        MatButtonModule,
        MatFormFieldModule,
        MatIconModule,
        MatInputModule,
        MatPaginatorModule,
        MatRippleModule,
        MatSelectModule,
        MatSortModule,
        MatSnackBarModule,
        MatTableModule,
        MatCardModule,
        MatTooltipModule,

        FuseSharedModule
    ],
    providers   : [
        UserService
    ]
})
export class UsersModule
{
}

