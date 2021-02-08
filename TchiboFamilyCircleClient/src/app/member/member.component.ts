import { Component, Inject, OnInit } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { FamilyMember } from "src/models/familyMember";
import { Article } from "src/models/article";
import { ActivatedRoute } from "@angular/router";
import { Occasion } from "src/models/occasion";

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
  occasion: Occasion;
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
  onDataLoaded() {
    if (this.familyMember && this.occasion && this.articles) {
      this.loading = false;
      if (this.familyMember && this.occasion) {
        this.headline = `Presents for your ${this.familyMember.type.toLowerCase()} ${
          this.familyMember.name
        } on ${this.occasion.name.toLowerCase()}`;
      }
    }
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
          this.errorMessage = "";
          this.onDataLoaded();
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
          this.onDataLoaded();
          console.debug(result);
        },
        (error) => {
          this.errorMessage = "Server Unavailable";
          this.loading = false;
          console.error(error);
        }
      );

    this.http
      .get<Occasion>(this.baseUrl + "api/Occasions/" + this.occasionId)
      .subscribe(
        (result) => {
          this.occasion = result;
          this.errorMessage = "";
          this.onDataLoaded();
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
