import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FavoritesComponent } from './favorites/favorites.component';
import { PurchasesComponent } from './purchases/purchases.component';
import { ReviewsComponent } from './reviews/reviews.component';
import { UserComponent } from './user.component';

const routes: Routes = [
  {
    path: ':id', component: UserComponent,
    children: [
      {path: 'favorites', component: FavoritesComponent},
      {path: 'purchases', component: PurchasesComponent},
      {path:'reviews', component: ReviewsComponent}
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserRoutingModule { }
