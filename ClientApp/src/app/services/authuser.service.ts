
import { Injectable, Inject } from '@angular/core';
import { Observable, of, BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';

export interface AuthData {
    authenticated: boolean;
    claims: {[key: string]: string};
}

export interface User {
    name: string;
    email: string;
    claims: {[key: string]: string};
    permissions: string[];
}

const MAIL_CLAIM = 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress';

@Injectable()
export class AuthUserService {

    private currentUserSubject: BehaviorSubject<User>;
    public currentUser: Observable<User>;

    constructor(private httpClient: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
        this.currentUserSubject = new BehaviorSubject<User>(null);
        this.currentUser = this.currentUserSubject.asObservable();
        this.refreshAuthStatus();
    }

    public refreshAuthStatus() {
        return this.httpClient.get<AuthData>(this.baseUrl + 'Account/AuthStatus')
            .subscribe((data: AuthData) => {
                if (data.authenticated) {
                    let user: User = {
                        name: data.claims.name,
                        email: data.claims[MAIL_CLAIM],
                        claims: data.claims,
                        // TODO issue #51
                        permissions: Object.entries(data.claims)
                            // flatMap isn't is this standard yet :(
                            .filter(([_, val]) => val == '_')
                            .map(([key, _]) => key),
                    };
                    this.currentUserSubject.next(user);
                } else {
                    this.currentUserSubject.next(null);
                }
            });
    }

}
