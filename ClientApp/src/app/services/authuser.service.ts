
import { Injectable, Inject } from '@angular/core';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';

export interface AuthData {
    authenticated: boolean;
    claims: {[key: string]: string};
}

const MAIL_CLAIM = 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress';

@Injectable()
export class AuthUserService {

    authData: AuthData | null = null;
    constructor(private httpClient: HttpClient, @Inject('BASE_URL') private baseUrl: string) {

    }

    public getAuthStatus(): Observable<AuthData> {
        if (this.authData == null) {
            return this.httpClient.get<AuthData>(this.baseUrl + 'Account/AuthStatus')
                .pipe(map(data => {
                    this.authData = data;
                    return data;
                }));
        } else {
            return of(this.authData);
        }
    }

    public refreshAuthStatus(): Observable<AuthData> {
        return this.httpClient.get<AuthData>(this.baseUrl + 'Account/AuthStatus')
            .pipe(map(data => {
                this.authData = data;
                return data;
            }));
    }

    // public getEmail(): Observable<string> {
    //     return this.getAuthStatus().pipe(map(data => data.claims[MAIL_CLAIM]))
    // }

    // public getName(): Observable<string> {
    //     return this.getAuthStatus().pipe(map(data => data.claims['name']))
    // }

}
