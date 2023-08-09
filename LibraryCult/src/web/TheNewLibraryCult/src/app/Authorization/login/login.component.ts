import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, MaxValidator, MinLengthValidator, MinValidator, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ILogin } from 'src/app/Interfaces/UserForAuthentication';
import { UserResponseModel } from 'src/app/Models/UserResponses/UserResponseModel';
import { AuthenticationService } from 'src/app/Services/AuthenticationService/authentication.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styles: [],
})
export class LoginComponent implements OnInit {
  constructor(private authService: AuthenticationService) { }

  errorMessage: Array<string> = new Array();
  showError: boolean;
  form: FormGroup;

  ngOnInit(): void {
    this.form = new FormGroup({
      email: new FormControl("", [Validators.required]),
      password: new FormControl("", [Validators.required])
    });
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
        next: (response: UserResponseModel) => console.log(response),
        error: (err: HttpErrorResponse) => {
          for(let iError of err.error.errors){
            this.errorMessage.push(iError);
          }
          this.showError = true;
        },
        complete: () => console.info('complete')
      });
  }
}
