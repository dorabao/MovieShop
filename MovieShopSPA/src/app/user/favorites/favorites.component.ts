import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserService } from 'src/app/core/services/user.service';
import { MovieCard } from 'src/app/shared/models/movieCard';


@Component({
  selector: 'app-favorites',
  templateUrl: './favorites.component.html',
  styleUrls: ['./favorites.component.css']
})
export class FavoritesComponent implements OnInit {
  id: number = 0;
  favoriteMovies!: MovieCard[];

  constructor(private route: ActivatedRoute, private userService: UserService) { }

  ngOnInit(): void {
    console.log('inside user favorites component')
    this.route.paramMap.subscribe(
      p => {
        this.id  = Number(p.get('id'));
        console.log("userId:" + this.id);
        this.userService.getUserFavoriteMovies(this.id).subscribe(
          f => {
            this.favoriteMovies = f;
            console.log(this.favoriteMovies);
          }
        )
      }
    );
  }

}
