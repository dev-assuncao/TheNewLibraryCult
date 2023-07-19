import { Component } from '@angular/core';
import { faUser, faSignIn } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styles: [
  ]
})

export class MenuComponent {
  faUser = faUser;
  faSignIn = faSignIn;
}
