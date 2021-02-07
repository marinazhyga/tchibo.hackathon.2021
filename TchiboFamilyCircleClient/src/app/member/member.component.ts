import { Component, Inject, OnInit } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { FamilyMember } from "src/models/familyMember";
import { Article } from "src/models/article";
import { ActivatedRoute } from "@angular/router";

@Component({
  selector: "app-home",
  templateUrl: "./member.component.html",
  styleUrls: ["./member.component.css"],
})
export class MemberComponent implements OnInit {
  familyMember: FamilyMember;
  articles: Article[];
  headline = "";
  familyMemberId: string;
  occasionId: number;
  errorMessage = "";
  loading = false;

  constructor(
    private http: HttpClient,
    @Inject("FAMILY_CIRCLE_API_URL") private baseUrl: string,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.loadData();
  }
  loadData() {
    this.familyMemberId = this.route.snapshot.params.id;
    this.occasionId = this.route.snapshot.params.occasionId;
    this.loading = true;
    this.errorMessage = "";
    this.http
      .get<FamilyMember>(
        this.baseUrl + "api/FamilyMembers/" + this.familyMemberId
      )
      .subscribe(
        (result) => {
          console.debug(result);
          this.familyMember = result;
          this.loading = false;
          this.errorMessage = "";
          if (this.familyMember) {
            this.headline = `Presents for ${this.familyMember.type} ${this.familyMember.name}`;
          }
        },
        (error) => {
          this.errorMessage = "Server Unavailable";
          this.loading = false;
          console.error(error);
        }
      );

    this.http
      .get<Article[]>(
        this.baseUrl +
          "api/FamilyCircles?familyMemberId=" +
          this.familyMemberId +
          "&occasionId=" +
          this.occasionId
      )
      .subscribe(
        (result) => {
          this.articles = result;
          this.errorMessage = "";
          console.debug(result);
        },
        (error) => {
          this.errorMessage = "Server Unavailable";
          this.loading = false;
          console.error(error);
        }
      );
  }
}
