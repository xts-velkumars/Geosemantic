import { NgModule } from '@angular/core';
import { SpinnerComponent } from "./spinnercomponent/spinner.component";
import { ConfirmationModalComponent } from "./modalcomponent/confirmationmodal.component";
import { GenericMessageModalComponent } from "./modalcomponent/genericmessagemodal.component";


import { MatDividerModule, MatDialogModule, MatButtonModule } from '@angular/material';

export const components = [
  SpinnerComponent,
  ConfirmationModalComponent,
  GenericMessageModalComponent
];

@NgModule({
  declarations: [components],
  imports: [
    MatDividerModule,
    MatDialogModule,
    MatButtonModule],
  exports: [components],

})
export class ComponentsModule { }
