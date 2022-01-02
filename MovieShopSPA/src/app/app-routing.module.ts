import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './account/login/login.component';
import { RegisterComponent } from './account/register/register.component';
import { HomeComponent } from './home/home.component';

const routes: Routes = [
  {path:"", component: HomeComponent},
  //lazily load the movies module
  {path: 'movies', loadChildren: () => import("./movies/movies.module").then(mod => mod.MoviesModule) },
  {path: 'user', loadChildren: () => import("./user/user.module").then(mod => mod.UserModule) },
  {path:"account/login", component:LoginComponent},
  {path:"account/register", component:RegisterComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
