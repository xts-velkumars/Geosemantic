import { NgModule } from '@angular/core';
import { MatButtonModule, MatIconModule } from '@angular/material';

import { FuseNavigationModule } from '@fuse/components';
import { FuseSharedModule } from '@fuse/shared.module';

import { NavbarVerticalStyleComponent } from 'app/layout/components/navbar/vertical/style/style.component';

@NgModule({
    declarations: [
        NavbarVerticalStyleComponent
    ],
    imports     : [
        MatButtonModule,
        MatIconModule,

        FuseSharedModule,
        FuseNavigationModule
    ],
    exports     : [
        NavbarVerticalStyleComponent
    ]
})
export class NavbarVerticalStyleModule
{
}
