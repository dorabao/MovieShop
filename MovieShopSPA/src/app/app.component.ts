import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'MovieShopSPA';
}

//get top revenue movies and send to view
//use services to make the call to the API as they isolate business logic from components UI logic
//in Angular, we use HttpClient to call our API to get the Json data