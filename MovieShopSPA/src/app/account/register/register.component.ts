import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from 'src/app/core/services/authentication.service';
import { Register } from 'src/app/shared/models/register';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  newAccount!: Register;
  constructor(private authenticationService: AuthenticationService) { }

  ngOnInit(): void {
    console.log('inside Register Component')
    this.authenticationService.register()
      .subscribe(
        r => {
          this.newAccount = r;
        }
      );
  }

}
