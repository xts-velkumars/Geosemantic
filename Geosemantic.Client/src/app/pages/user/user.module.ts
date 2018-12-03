import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MatButtonModule, MatChipsModule, MatFormFieldModule, MatIconModule, MatInputModule, MatPaginatorModule, MatRippleModule, MatSelectModule, MatSnackBarModule, MatSortModule,
    MatTableModule, MatTabsModule ,MatDatepickerModule} from '@angular/material';

import { FuseSharedModule } from '@fuse/shared.module';

import { UserComponent } from 'app/pages/user/user.component';
import { UserService } from 'app/pages/user/user.service';
import { AuthGuard } from 'app/guards/auth.guard';
import { UtilityService } from '../../services';
const routes: Routes = [
    {
        path     : '**',
        component: UserComponent,canActivate: [AuthGuard]
    }
];

@NgModule({
    declarations: [
        UserComponent
    ],
    imports     : [
        RouterModule.forChild(routes),

        MatButtonModule,
        MatChipsModule,
        MatFormFieldModule,
        MatIconModule,
        MatInputModule,
        MatPaginatorModule,
        MatRippleModule,
        MatSelectModule,
        MatSortModule,
        MatSnackBarModule,
        MatTableModule,
        MatTabsModule,
        MatDatepickerModule,
        FuseSharedModule,
    ],
    providers   : [
        UserService,
        UtilityService
    ]
})
export class UserModule
{
}

