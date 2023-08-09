import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable, of} from 'rxjs';
import { UserResponseModel } from '../../Models/UserResponses/UserResponseModel';
import { ILogin } from '../../Interfaces/UserForAuthentication';

const url = 'https://localhost:7161/api/Auth/';

@Injectable({
  providedIn: 'root'
})

export class AuthenticationService {
  constructor(private http: HttpClient) { }

    loginUserApi(login:ILogin) : Observable<UserResponseModel>{   
      return this.http.post<UserResponseModel>(url + 'login', login);
    }


    logoutUser() : Observable<UserResponseModel>{
      return this.http.post<UserResponseModel>(url + 'logout', {});
    }
}
