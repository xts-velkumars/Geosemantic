import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MatButtonModule, MatChipsModule, MatFormFieldModule, MatIconModule, MatInputModule, MatPaginatorModule, MatRippleModule, MatSelectModule, MatSnackBarModule, MatSortModule,
    MatTableModule, MatTabsModule } from '@angular/material';

import { FuseSharedModule } from '@fuse/shared.module';

import { OrganisationComponent } from 'app/pages/organisation/organisation.component';
import {OrganisationService} from '../organisation/organisation.service';
import { AuthGuard } from 'app/guards/auth.guard';

const routes = [
    {
        path     : '**',
        component: OrganisationComponent,canActivate: [AuthGuard]
    }
];

@NgModule({
    declarations: [
        OrganisationComponent
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

        FuseSharedModule
    ],
    providers   : [
        OrganisationService
    ]
})
export class OrganisationModule
{
}
