import { Component, Inject, OnInit } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { FamilyMember } from "src/models/familyMember";
import { Occasion } from "src/models/occasion";

@Component({
  selector: "app-family-circle",
  templateUrl: "./family-circle.component.html",
  styleUrls: ["./family-circle.component.css"],
})
export class FamilyCircleComponent implements OnInit {
  familyMembers: FamilyMember[];
  occasions: Occasion[];
  errorMessage = "";
  loading = false;
  constructor(
    private http: HttpClient,
    @Inject("FAMILY_CIRCLE_API_URL") private baseUrl: string
  ) {}

  ngOnInit() {
    this.loadData();
  }
  loadData() {
    this.loading = true;

    this.errorMessage = "";
    this.http.get<FamilyMember[]>(this.baseUrl + "api/FamilyMembers").subscribe(
      (result) => {
        this.loading = false;
        this.errorMessage = "";
        console.debug(result);
        this.familyMembers = result;
      },
      (error) => {
        this.loading = false;
        this.errorMessage = "Server Unavailable";
        console.error(error);
      }
    );

    this.http.get<Occasion[]>(this.baseUrl + "api/Occasions").subscribe(
      (result) => {
        this.loading = false;
        console.debug(result);
        this.occasions = result;
      },
      (error) => {
        this.loading = false;
        this.errorMessage = "Server Unavailable";
        console.error(error);
      }
    );
  }
}
