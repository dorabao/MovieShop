import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserService } from 'src/app/core/services/user.service';
import { MovieCard } from 'src/app/shared/models/movieCard';

@Component({
  selector: 'app-purchases',
  templateUrl: './purchases.component.html',
  styleUrls: ['./purchases.component.css']
})
export class PurchasesComponent implements OnInit {
  id: number = 0;
  purchasedMovies!: MovieCard[];

  constructor(private route: ActivatedRoute, private userService: UserService) { }

  ngOnInit(): void {
    console.log('inside user purchases component')
    this.route.paramMap.subscribe(
      p => {
        this.id  = Number(p.get('id'));
        console.log("userId:" + this.id);
        this.userService.getUserPurchasedMovies(this.id).subscribe(
          up => {
            this.purchasedMovies = up;
            console.log(this.purchasedMovies);
          }
        )
      }
    );
  }
}
