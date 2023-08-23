import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root'
})


export class CookieHandlerService {

  constructor(private cookieService:CookieService) { 
  }

  setTokenInCookie(token: string){
      const cookieName = 'token';

      const cookieValue = token;

      const expirationDays = 0.1;

      const expirationDate = new Date();
      expirationDate.setDate(expirationDate.getDate() + expirationDays);

      this.cookieService.set(cookieName, cookieValue, expirationDate, '/');
  }
}
