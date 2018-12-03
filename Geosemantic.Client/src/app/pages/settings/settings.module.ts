import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MatButtonModule, MatChipsModule, MatFormFieldModule, MatIconModule, MatInputModule, MatPaginatorModule, MatRippleModule, MatSelectModule, MatSnackBarModule, MatSortModule,
    MatTableModule, MatTabsModule ,MatCardModule,MatCheckboxModule,MatTooltipModule} from '@angular/material';

import { FuseSharedModule } from '@fuse/shared.module';

import { AuthGuard } from 'app/guards/auth.guard';
import { SettingsService } from './settings.service';
import { SettingsComponent } from './settings.component';
const routes: Routes = [
    {
        path     : '**',
        component: SettingsComponent,canActivate: [AuthGuard],
        data: {
            title: 'Settings'
          }
    }
];

@NgModule({
    declarations: [
        SettingsComponent
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
        SettingsService
    ]
})
export class SettingsModule
{
}

