import { Routes } from "@angular/router";
import { HomeComponent } from "./Navegation/home/home.component";
import { FooterComponent } from "./Navegation/footer/footer.component";
import { MenuComponent } from "./Navegation/menu/menu.component";
import { LoginComponent } from "./Authorization/login/login.component";
import { RegisterComponent } from "./Authorization/register/register.component";


export const rootRouterConfig : Routes = [
    {path: 'home', component:HomeComponent},
    {path: 'footer', component:FooterComponent},
    {path: 'menu', component:MenuComponent},
    {path: 'login', component: LoginComponent},
    {path: 'register', component: RegisterComponent}
]