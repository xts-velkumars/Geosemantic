import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MatButtonModule, MatFormFieldModule, MatIconModule, MatInputModule, MatTableModule, MatTabsModule } from '@angular/material';

import { FuseSharedModule } from '@fuse/shared.module';
import { NewsletterComponent } from './newsletter.component';
import { NewsletterService } from './newsletter.service';
import { AuthGuard } from '../../guards';



const routes = [
    {
        path     : '**',
        component: NewsletterComponent,canActivate: [AuthGuard],
        resolve  : {
            search: NewsletterService
        }
    }
];

@NgModule({
    declarations: [
        NewsletterComponent,
    ],
    imports     : [
        RouterModule.forChild(routes),

        MatButtonModule,
        MatFormFieldModule,
        MatIconModule,
        MatInputModule,
        MatTableModule,
        MatTabsModule,

        FuseSharedModule
    ],
    providers   : [
        NewsletterService
    ]
})
export class NewsletterModule
{
}
