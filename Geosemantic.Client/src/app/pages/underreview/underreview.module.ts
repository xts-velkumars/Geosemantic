import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MatButtonModule,MatFormFieldModule,MatIconModule, MatCardModule} from '@angular/material';
import { FuseSharedModule } from '@fuse/shared.module';
import { UnderReviewComponent } from 'app/pages/underreview/underreview.component';
const routes: Routes = [
    {
        path     : '**',
        component: UnderReviewComponent
    }
];

@NgModule({
    declarations: [
        UnderReviewComponent
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
export class UnderReviewModule
{
}

