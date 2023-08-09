import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {RouterModule} from '@angular/router';
import { AppComponent } from './app.component';
import { HomeComponent } from './Navegation/home/home.component';
import { FooterComponent } from './Navegation/footer/footer.component';
import { MenuComponent } from './Navegation/menu/menu.component';
import { rootRouterConfig } from './app.route';
import { APP_BASE_HREF } from '@angular/common';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { LoginComponent } from './Authorization/login/login.component';
import { RegisterComponent } from './Authorization/register/register.component';
import {ReactiveFormsModule} from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AuthenticationService } from './Services/AuthenticationService/authentication.service';

import {httpInterceptorProviders} from './_helpers/http.interceptor';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    FooterComponent,
    MenuComponent,
    LoginComponent,
    RegisterComponent
  ],
  imports: [
    BrowserModule,
    [RouterModule.forRoot(rootRouterConfig, {useHash:false})],
    FontAwesomeModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [
    {provide: APP_BASE_HREF, useValue:'/'},
    AuthenticationService,
    httpInterceptorProviders
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
