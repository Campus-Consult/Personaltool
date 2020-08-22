import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthUserService, User } from './services/authuser.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  public baseURL: string;
  public authUser: User;
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string, private authUserService: AuthUserService){
    this.baseURL = baseUrl;
  }
  ngOnInit(): void {
    this.authUserService.currentUser.subscribe(user => {this.authUser = user; console.log(user)});
 /*    const url = '';
    let body: any;
    this.http.post(url, body); */
  }
}
