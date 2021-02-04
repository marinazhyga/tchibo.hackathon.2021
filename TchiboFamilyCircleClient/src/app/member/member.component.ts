import { Component, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { FamilyMember } from "src/models/familyMember";
import { Article } from "src/models/article";


@Component({
  selector: "app-home",
  templateUrl: "./member.component.html",
  styleUrls: ["./member.component.css"],
})
export class MemberComponent {
  public familyMember: FamilyMember;
  public articles: Article[];
  public headline = "Loading...";
  constructor(http: HttpClient, @Inject("BASE_URL") baseUrl: string) {
    http.get<FamilyMember>(baseUrl + "api/FamilyMembers/" + "60187e1dea2b94a848b786aa").subscribe((result) => {
          console.debug(result);
          this.familyMember = result;
          if (this.familyMember) {
            this.headline = `Presents for ${this.familyMember.type} ${this.familyMember.name}`;
          }
        }, (error) => console.error(error)
      );

    http.get<Article[]>(baseUrl + "api/FamilyCircles?familyMemberId=60187e1dea2b94a848b786aa&occasionId=1").subscribe((result) => {
          console.debug(result);
          this.articles = result;
        },(error) => console.error(error)
      );
  }
}

