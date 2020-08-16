import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthUserService, AuthData } from './services/authuser.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  public baseURL:string;
  public authUserData: AuthData;
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string, private authUserService: AuthUserService){
    this.baseURL = baseUrl;
  }
  ngOnInit(): void {
    this.authUserService.getAuthStatus().subscribe(data => this.authUserData = data);
 /*    const url = '';
    let body: any;
    this.http.post(url, body); */
  }
}
