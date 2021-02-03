import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './member.component.html',
  styleUrls: ['./member.component.css']
})
export class MemberComponent {
 

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
       
  }
}



