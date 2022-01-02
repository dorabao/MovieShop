import { Component, Input, OnInit } from '@angular/core';
import { MovieCard } from '../../models/movieCard';

@Component({
  selector: 'app-movie-card',
  templateUrl: './movie-card.component.html',
  styleUrls: ['./movie-card.component.css']
})
export class MovieCardComponent implements OnInit {

  @Input() movieCard!: MovieCard;
  @Input() testInfo!: string;

  constructor() { }

  ngOnInit(): void {

    console.log('inside the child component');
    console.log(this.movieCard);
    console.log(this.testInfo);
  }

}
