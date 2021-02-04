import { Component, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";

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
    http.get<FamilyMember>(baseUrl + "api/FamilyMembers/" + "60187f42ea2b94a848b786ab").subscribe((result) => {
          console.debug(result);
          this.familyMember = result;
          if (this.familyMember) {
            this.headline = `Presents for ${this.familyMember.type} ${this.familyMember.name}`;
          }
        }, (error) => console.error(error)
      );

    http.get<Article[]>(baseUrl + "api/FamilyCircles?familyMemberId=60187f42ea2b94a848b786ab&occasionId=1").subscribe((result) => {
          console.debug(result);
          this.articles = result;
        },(error) => console.error(error)
      );
  }
}

interface Article {
  articleNumber: Int16Array;
  productId: Int16Array;
  ean: Int16Array;
  title: string;
  imageUrl: string;
  pageUrl: string;
  priceAmount: string;
  priceOldAmount: string;
  priceCurrency: string;
  deliveryDate: string;
}

interface FamilyMember {
  id: string;
  name: string;
  type: string;
  dateOfBirth: string;
  occasions: Occasion[];
  sizes: string[];
  interests: string[];
  customerNumber: string;
}

interface Occasion {
  id: string;
  name: string;
  date: string;
}
