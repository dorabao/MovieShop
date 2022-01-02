import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UserRoutingModule } from './user-routing.module';
import { FavoritesComponent } from './favorites/favorites.component';
import { PurchasesComponent } from './purchases/purchases.component';
import { ReviewsComponent } from './reviews/reviews.component';
import { UserComponent } from './user.component';


@NgModule({
  declarations: [
    FavoritesComponent,
    PurchasesComponent,
    ReviewsComponent,
    UserComponent
  ],
  imports: [
    CommonModule,
    UserRoutingModule
  ]
})
export class UserModule { }
