import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FamilyMember } from 'src/models/familyMember';
import { Occasion } from 'src/models/occasion';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  public familyMembers: FamilyMember[];
  public occasions: Occasion[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    
    http.get<FamilyMember[]>(baseUrl + 'api/FamilyMembers').subscribe(result => {
      console.debug(result);
      this.familyMembers = result;     
    }, error => console.error(error));
    
    http.get<Occasion[]>(baseUrl + 'api/Occasions').subscribe(result => {
      console.debug(result);
      this.occasions = result;
    }, error => console.error(error));
  }
}

