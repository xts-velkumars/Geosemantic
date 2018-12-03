import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MatDialogModule , MatButtonModule, MatDividerModule, MatFormFieldModule, MatIconModule, MatMenuModule, MatSelectModule, MatTableModule, MatTabsModule,MatRippleModule,MatTooltipModule } from '@angular/material';

import { FuseSharedModule } from '@fuse/shared.module';
import { ChartsModule } from 'ng2-charts';

import { DashboardComponent } from 'app/pages/dashboard/dashboard.component';
import { DashboardService } from './dashboard.service';
import { AuthGuard } from 'app/guards/auth.guard';

const routes: Routes = [
    {
        path     : '**',
        component: DashboardComponent,canActivate: [AuthGuard],
        resolve  : {
            data: DashboardService
        }
    }
];

@NgModule({
    declarations: [
        DashboardComponent
    ],
    imports     : [
        RouterModule.forChild(routes),

        MatButtonModule,
        MatDividerModule,
        MatFormFieldModule,
        MatIconModule,
        MatMenuModule,
        MatSelectModule,
        MatTableModule,
        MatTabsModule,
        MatDialogModule,
        MatRippleModule,
        MatTooltipModule,
        ChartsModule,
        FuseSharedModule,
    ],
    providers   : [
        DashboardService
    ]
})
export class DashboardModule
{
}

