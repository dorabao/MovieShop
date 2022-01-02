import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {

  id: number = 0;
  constructor(private route: ActivatedRoute) { }

  ngOnInit(): void {
    console.log('inside user component');
    this.route.paramMap.subscribe(
      p => {
        this.id =Number(p.get('id'));
        console.log("userId:" + this.id);
      }
    );
  }
}
