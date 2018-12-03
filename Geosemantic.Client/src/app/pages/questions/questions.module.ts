import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MatButtonModule, MatChipsModule, MatFormFieldModule, MatIconModule, MatInputModule, MatPaginatorModule, MatRippleModule, MatSelectModule, MatSnackBarModule, MatSortModule,
    MatTableModule, MatTabsModule ,MatCardModule,MatCheckboxModule,MatTooltipModule} from '@angular/material';

import { FuseSharedModule } from '@fuse/shared.module';

import { AuthGuard } from 'app/guards/auth.guard';
import { QuestionsComponent } from './questions.component';
import { QuestionsService } from './questions.service';
const routes: Routes = [
    {
        path     : '**',
        component: QuestionsComponent,canActivate: [AuthGuard],
        data: {
            title: 'Forms'
          }
    }
];

@NgModule({
    declarations: [
        QuestionsComponent
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
        QuestionsService
    ]
})
export class QuestionsModule
{
}

