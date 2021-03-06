import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MatButtonModule, MatCheckboxModule, MatFormFieldModule, MatInputModule,MatIconModule,MatDatepickerModule,MatSelectModule } from '@angular/material';

import { FuseSharedModule } from '@fuse/shared.module';
import { UtilityService } from '../../services';
import { RegisterComponent } from 'app/pages/register/register.component';

const routes = [
    {
        path     : '**',
        component: RegisterComponent
    }
];

@NgModule({
    declarations: [
        RegisterComponent
    ],
    imports     : [
        RouterModule.forChild(routes),

        MatButtonModule,
        MatCheckboxModule,
        MatFormFieldModule,
        MatInputModule,
        MatDatepickerModule,
        MatSelectModule,
        FuseSharedModule,
        MatIconModule
    ],
    providers   : [        
        UtilityService
    ]
})
export class RegisterModule
{
}
