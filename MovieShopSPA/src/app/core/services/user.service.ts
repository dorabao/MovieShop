import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { MovieCard } from 'src/app/shared/models/movieCard';
import { Review } from 'src/app/shared/models/review';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  getUserFavoriteMovies(id: number) : Observable<MovieCard[]> {
    return this.http.get<MovieCard[]>(`${environment.apiBaseUrl}User/${id}/favorites`);
  }
  getUserPurchasedMovies(id: number) : Observable<MovieCard[]> {
    return this.http.get<MovieCard[]>(`${environment.apiBaseUrl}User/${id}/purchases`);
  }

  getAllReviewsByUserId(id: number) : Observable<Review[]> {
    return this.http.get<Review[]>(`${environment.apiBaseUrl}User/${id}/reviews`);
  }

}
