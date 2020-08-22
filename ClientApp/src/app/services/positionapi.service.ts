import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface PositionHolder {
    personID: number;
    begin: string;
    end: string | null;
    firstName: string;
    lastName: string;
}

export interface Position {
    positionID: number;
    name: string;
    shortName: string;
    isActive: boolean;
    currentHolders: PositionHolder[];
}

@Injectable()
export class PositionApiService {
    positionApiRoot: string;
    constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
        this.positionApiRoot = this.baseUrl + "api/Position";
    }

    public getAll(): Observable<Position[]> {
        return this.http.get<Position[]>(this.positionApiRoot);
    }

    public get(id: number): Observable<Position> {
        return this.http.get<Position>(this.positionApiRoot+"/"+id);
    }
}