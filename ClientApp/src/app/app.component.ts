import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  public baseURL:string;
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string){
    this.baseURL = baseUrl;
  }
  ngOnInit(): void {
 /*    const url = '';
    let body: any;
    this.http.post(url, body); */
  }
}
