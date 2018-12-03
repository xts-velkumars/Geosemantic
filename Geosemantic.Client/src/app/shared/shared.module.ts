import { NgModule } from '@angular/core';
import { ComponentsModule } from "./component/components.module";
import { DirectivesModule } from "./directives/directives.module";
 

@NgModule({
  imports: [
    ComponentsModule,
    DirectivesModule,
    
  ],
  declarations: [

  ],
  exports: [
    ComponentsModule,
    DirectivesModule,
   
  ]
})


export class SharedModule { }
