import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { AuthenticatedRouting } from "./authenticated.routing";
import { AuthenticatedComponent } from "./authenticated.component";
import { HeaderModule, MenuModule } from "@educa-online/components";

@NgModule({
    imports: [
      CommonModule,
      RouterModule,
      MenuModule,
      AuthenticatedRouting,
      HeaderModule,
    ],
    exports: [],
    declarations: [AuthenticatedComponent],
    providers: [],
})

export class AuthenticatedModule {}