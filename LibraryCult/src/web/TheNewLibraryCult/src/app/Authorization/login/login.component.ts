import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, MaxValidator, MinLengthValidator, MinValidator, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ILogin } from 'src/app/Interfaces/UserForAuthentication';
import { UserClaimsModel } from 'src/app/Models/UserResponses/UserClaimsModel';
import { UserResponseModel } from 'src/app/Models/UserResponses/UserResponseModel';
import { AuthenticationService } from 'src/app/Services/AuthenticationService/authentication.service';
import { StorageService } from 'src/app/Services/AuthenticationService/storage.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styles: [],
})
export class LoginComponent implements OnInit {
  constructor(private authService: AuthenticationService, private storageService : StorageService, private router: Router) { }

  errorMessage: Array<string> = new Array();
  showError: boolean;
  form: FormGroup;
  isLoggedIn = false;
  isLogginFailed = false;
  roles: UserClaimsModel[] = [];
  
  ngOnInit(): void {
    this.form = new FormGroup({
      email: new FormControl("", [Validators.required]),
      password: new FormControl("", [Validators.required])
    });

    if(this.storageService.isLoggedIn()){
      this.isLoggedIn = true;
      this.roles = this.storageService.getUser().roles;
    }
  }

  validateControl = (controlName: string) => {
    return this.form.get(controlName).invalid && this.form.get(controlName).touched;
  }

  hasError = (controlName: string, errorName: string) => {
    return this.form.get(controlName).hasError(errorName);
  }

  loginUser() {
    this.showError = false;
    const val = this.form.value;

    const userLogin: ILogin = {
      email: val.email,
      password: val.password
    }

    this.authService.loginUserApi(userLogin)
      .subscribe({
        next: (response: UserResponseModel) => {
          this.storageService.saveUser(response.accessToken);
          this.isLogginFailed = false;
          this.isLoggedIn = true;

          for(let role of response.userToken.claims){
            this.roles.push(role);
          }
          this.router.navigate(['home']);         
          this.reloadPage();
          
        },
        error: (err: HttpErrorResponse) => {
          for(let iError of err.error.errors){
            this.errorMessage.push(iError);
          }
          this.isLogginFailed = true;
          this.isLoggedIn = false;
          this.showError = true;
        },
        complete: () => console.info('complete')
      });
  }

  reloadPage():void{
    window.location.reload();
  }
}
