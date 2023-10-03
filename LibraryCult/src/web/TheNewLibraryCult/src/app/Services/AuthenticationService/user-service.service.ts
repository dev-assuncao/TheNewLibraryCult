import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';



const API_URL =  'https://localhost:7292/'



@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  getPublicContent(): Observable<any>{
    return this.http.get(API_URL + 'WeatherForecast/GetWeatherForecast', {responseType : 'text'});
  }
}
