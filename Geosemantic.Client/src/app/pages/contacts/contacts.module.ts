import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MatButtonModule, MatChipsModule, MatFormFieldModule, MatIconModule, MatInputModule, MatPaginatorModule, MatRippleModule, MatSelectModule, MatSnackBarModule, MatSortModule,
    MatTableModule, MatTabsModule ,MatCardModule,MatCheckboxModule,MatTooltipModule} from '@angular/material';

import { FuseSharedModule } from '@fuse/shared.module';

import { AuthGuard } from 'app/guards/auth.guard';
import { ContactsService } from './contacts.service';
import { ContactsComponent } from './contacts.component';
const routes: Routes = [
    {
        path     : '**',
        component: ContactsComponent,canActivate: [AuthGuard],
        data: {
            title: 'Contacts'
          }
    }
];

@NgModule({
    declarations: [
        ContactsComponent
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
        ContactsService
    ]
})
export class ContactsModule
{
}

