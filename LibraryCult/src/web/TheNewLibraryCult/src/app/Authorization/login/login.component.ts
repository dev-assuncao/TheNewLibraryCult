import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
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

  form: FormGroup;

  ngOnInit(): void {
    this.form = new FormGroup({
      email: new FormControl("", [Validators.required]),
      password: new FormControl("", [Validators.required])
    })
  }

  validateControl = (controlName: string) => {
    return this.form.get(controlName)?.invalid && this.form.get(controlName)?.touched
  }

  loginUser() {
    const val = this.form.value;

    const userLogin: ILogin = {
      email: val.email,
      password: val.password
    }

    this.authService.loginUserApi(userLogin)
      .subscribe({
          next: (response) => console.log(response),
          error: (response) => console.error(response),
          complete: () => console.info('complete')
        });
  }
}
