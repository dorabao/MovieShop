import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AuthenticationService } from 'src/app/core/services/authentication.service';
import { Login } from 'src/app/shared/models/login';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  id: number = 0;
  accountInfo! : Login;
  constructor(private route: ActivatedRoute, private authenticationService: AuthenticationService) { }

  ngOnInit(): void {
    console.log('inside Login Component')
    this.route.paramMap.subscribe(
      p => {
        this.id =Number(p.get('id'));
        console.log("accountId:" + this.id);
        this.authenticationService.login(this.id).subscribe(
          l => {
            this.accountInfo = l;
            console.log(this.accountInfo);
          }
        )
      }
    );
  }

}
