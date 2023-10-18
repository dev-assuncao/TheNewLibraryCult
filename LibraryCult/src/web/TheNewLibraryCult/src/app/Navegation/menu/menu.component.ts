import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { faUser, faSignIn } from '@fortawesome/free-solid-svg-icons';
import { Observable } from 'rxjs';
import { AuthenticationService } from 'src/app/Services/AuthenticationService/authentication.service';
import { StorageService } from 'src/app/Services/AuthenticationService/storage.service';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styles: [
  ]
})

export class MenuComponent implements OnInit{
  faUser = faUser;
  faSignIn = faSignIn;
  currentUser : any;
  hasLogged : any;

  constructor(private storage: StorageService, private auth: AuthenticationService, private router : Router){}

  ngOnInit(): void {
      this.currentUser = this.storage.getUser();
      this.hasLogged = this.storage.isLoggedIn();
  }

  logOut(){
    this.auth.logoutUser().subscribe({
      next: (resp) => {
        this.storage.clean();
        this.router.navigate(['/login']);
      },
      error: (err) => {
        console.log(err);
      }
    })
    window.location.reload(); 
  }
}
