import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MatButtonModule, MatChipsModule, MatFormFieldModule, MatIconModule, MatInputModule, MatPaginatorModule, MatRippleModule, MatSelectModule, MatSnackBarModule, MatSortModule,
    MatTableModule, MatTabsModule ,MatCardModule,MatCheckboxModule,MatTooltipModule} from '@angular/material';

import { FuseSharedModule } from '@fuse/shared.module';

import { AuthGuard } from 'app/guards/auth.guard';
import { ReportsComponent } from './reports.component';
import { ReportsService } from './reports.service';
const routes: Routes = [
    {
        path     : '**',
        component: ReportsComponent,canActivate: [AuthGuard],
        data: {
            title: 'Reports'
        }
    }
];

@NgModule({
    declarations: [
        ReportsComponent
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
        ReportsService
    ]
})
export class ReportsModule
{
}

