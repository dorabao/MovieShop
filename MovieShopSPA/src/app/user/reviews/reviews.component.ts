import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserService } from 'src/app/core/services/user.service';
import { Review } from 'src/app/shared/models/review';

@Component({
  selector: 'app-reviews',
  templateUrl: './reviews.component.html',
  styleUrls: ['./reviews.component.css']
})
export class ReviewsComponent implements OnInit {

  id: number = 0;
  reviews!: Review[];

  constructor(private route: ActivatedRoute, private userService: UserService) { }

  ngOnInit(): void {
    console.log('inside user favorites component')
    this.route.paramMap.subscribe(
      p => {
        this.id  = Number(p.get('id'));
        console.log("userId:" + this.id);
        this.userService.getAllReviewsByUserId(this.id).subscribe(
          r => {
            this.reviews = r;
            console.log(this.reviews);
          }
        )
      }
    );
  }

}
