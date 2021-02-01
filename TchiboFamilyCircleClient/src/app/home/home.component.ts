import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
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

interface Occasion{
  id: string;
  name: string;
  date: string;
}

