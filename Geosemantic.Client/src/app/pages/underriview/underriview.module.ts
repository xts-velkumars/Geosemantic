import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MatButtonModule, MatChipsModule, MatFormFieldModule,MatCardModule, MatIconModule, MatInputModule, MatPaginatorModule, MatRippleModule, MatSelectModule, MatSnackBarModule, MatSortModule,
    MatTableModule, MatTabsModule ,MatDatepickerModule} from '@angular/material';

import { FuseSharedModule } from '@fuse/shared.module';
import { UnderRiviewComponent } from 'app/pages/underriview/underriview.component';
import { UserService } from 'app/pages/user/user.service';
import { AuthGuard } from 'app/guards/auth.guard';
import { UtilityService } from '../../services';
const routes: Routes = [
    {
        path     : '**',
        component: UnderRiviewComponent,canActivate: [AuthGuard]
    }
];

@NgModule({
    declarations: [
        UnderRiviewComponent
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
        MatCardModule
    ],
    providers   : [
        UserService,
        UtilityService
    ]
})
export class UnderRiviewModule
{
}

