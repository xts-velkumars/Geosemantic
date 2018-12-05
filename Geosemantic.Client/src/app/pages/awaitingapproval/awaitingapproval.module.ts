import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MatButtonModule,MatFormFieldModule,MatIconModule, MatCardModule} from '@angular/material';
import { FuseSharedModule } from '@fuse/shared.module';
import { AwaitingApprovalComponent } from 'app/pages/awaitingapproval/awaitingapproval.component';
const routes: Routes = [
    {
        path     : '**',
        component: AwaitingApprovalComponent
    }
];

@NgModule({
    declarations: [
        AwaitingApprovalComponent
    ],
    imports     : [
        RouterModule.forChild(routes),

        MatButtonModule,
        MatFormFieldModule,
        MatIconModule,
        FuseSharedModule,
        MatCardModule
    ]
})
export class AwaitingApprovalModule
{
}

