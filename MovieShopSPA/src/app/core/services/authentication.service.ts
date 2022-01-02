import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Login } from 'src/app/shared/models/login';
import { Register } from 'src/app/shared/models/register';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  constructor(private http: HttpClient) { }

  register() : Observable<Register> {
    return this.http.get<Register>(`${environment.apiBaseUrl}Account`);
  }

  login(id: number) : Observable<Login> {
    return this.http.get<Login>(`${environment.apiBaseUrl}Account/{id}`);
  }
}
