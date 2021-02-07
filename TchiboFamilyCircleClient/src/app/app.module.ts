import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { RouterModule } from "@angular/router";

import { AppComponent } from "./app.component";
import { HeaderComponent } from "./tchibo-layouts/header/header.component";
import { MainComponent } from "./tchibo-layouts/main/main.component";
import { FooterComponent } from "./tchibo-layouts/footer/footer.component";
import { FamilyCircleComponent } from "./family-circle/family-circle.component";
import { MemberComponent } from "./member/member.component";

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    MainComponent,
    FooterComponent,
    FamilyCircleComponent,
    MemberComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: "ng-cli-universal" }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: "", component: FamilyCircleComponent, pathMatch: "full" },
      { path: "member/:id/:occasionId", component: MemberComponent },
    ]),
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
