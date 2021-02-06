import { Component, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { FamilyMember } from "src/models/familyMember";
import { Article } from "src/models/article";
import { ActivatedRoute } from '@angular/router';


@Component({
  selector: "app-home",
  templateUrl: "./member.component.html",
  styleUrls: ["./member.component.css"],
})
export class MemberComponent {
  public familyMember: FamilyMember;
  public articles: Article[];
  public headline = "Loading...";
  public familyMemberId: string;
  public occasionId: number;
  
  constructor(http: HttpClient, @Inject("FAMILY_CIRCLE_API_URL") baseUrl: string, private route: ActivatedRoute) {
    this.familyMemberId = this.route.snapshot.params.id;
    console.log(this.familyMemberId);
    this.occasionId = this.route.snapshot.params.occasionId;  
    console.log(this.occasionId);
    
    http.get<FamilyMember>(baseUrl + "api/FamilyMembers/" + this.familyMemberId).subscribe((result) => {
          console.debug(result);
          this.familyMember = result;
          if (this.familyMember) {
            this.headline = `Presents for ${this.familyMember.type} ${this.familyMember.name}`;
          }
        }, (error) => console.error(error)
      );

    http.get<Article[]>(baseUrl + "api/FamilyCircles?familyMemberId=" + this.familyMemberId + "&occasionId=" + this.occasionId).subscribe((result) => {
          console.debug(result);
          this.articles = result;
        },(error) => console.error(error)
      );
  }
}

