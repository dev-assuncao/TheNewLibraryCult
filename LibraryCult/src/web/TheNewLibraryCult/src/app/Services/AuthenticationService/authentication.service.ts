import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable, of} from 'rxjs';
import { UserResponseModel } from '../../Models/UserResponses/UserResponseModel';
import { ILogin } from '../../Interfaces/UserForAuthentication';

@Injectable({
  providedIn: 'root'
})

export class AuthenticationService {
  constructor(private http: HttpClient) { }

  private url = 'https://localhost:7161/api/Auth/login';

    loginUserApi(login:ILogin) : Observable<UserResponseModel>{   
      return this.http.post<UserResponseModel>(this.url,login);
    }
}
