import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MatButtonModule, MatChipsModule, MatFormFieldModule, MatIconModule, MatInputModule, MatPaginatorModule, MatRippleModule, MatSelectModule, MatSnackBarModule, MatSortModule,
    MatTableModule, MatTabsModule ,MatCardModule,MatCheckboxModule,MatTooltipModule} from '@angular/material';

import { FuseSharedModule } from '@fuse/shared.module';

import { UsersComponent } from 'app/pages/users/users.component';
import { UsersService } from 'app/pages/users/users.service';
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
        MatCardModule,
        MatCheckboxModule,
        MatTooltipModule,
        FuseSharedModule
    ],
    providers   : [
        UsersService
    ]
})
export class UsersModule
{
}

