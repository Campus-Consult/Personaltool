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
  public permissions: string[] = [];
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string, private authUserService: AuthUserService){
    this.baseURL = baseUrl;
  }
  ngOnInit(): void {
    this.authUserService.currentUser.subscribe(user => {
      this.authUser = user;
      this.permissions = user?.permissions || [];
    });
  }

  // trigger logout by posting to the logout endpoint, then refresh auth status
  logout(): void {
    this.http.post(this.baseURL + 'Account/Logout', {})
      .subscribe(a => this.authUserService.refreshAuthStatus());
  }
}
