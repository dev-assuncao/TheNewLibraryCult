import { Injectable } from '@angular/core';
import {HttpClient, HttpHandler, HttpHeaders} from '@angular/common/http';
import {Observable, of} from 'rxjs';
import { UserResponseModel } from '../../Models/UserResponses/UserResponseModel';
import { ILogin } from '../../Interfaces/UserForAuthentication';

const url = 'https://localhost:7161/api/Auth/';

const httpOptions = {
  headers: new HttpHeaders({'Content-Type': 'application/json'})
}

@Injectable({
  providedIn: 'root'
})

export class AuthenticationService {
  constructor(private http: HttpClient) { }

    loginUserApi(login:ILogin) : Observable<UserResponseModel>{   
      return this.http.post<UserResponseModel>(url + 'login', login, httpOptions);
    }


    logoutUser() : Observable<UserResponseModel>{
      return this.http.post<UserResponseModel>(url + 'logout', {}, httpOptions);
    }
}
