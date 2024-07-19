import { Component, OnInit } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AuthService } from './services/auth.service';
import { User } from './models/user';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  jwtHelper = new JwtHelperService();
  
  constructor(private authService: AuthService) {}

  ngOnInit() {
    const token = localStorage.getItem('token');
    const user = localStorage.getItem('user');
    if (token) {
      this.authService.decodedToken = this.jwtHelper.decodeToken(token);
    }
    if (user && user != "undefined") {
      this.authService.currentUser = JSON.parse(user) as User;

      // console.log(this.authService.decodedToken)
      // console.log(this.authService.currentUser)
    }
  }
}
