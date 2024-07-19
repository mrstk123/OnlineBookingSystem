import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';
import { environment } from '../../environments/environment';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = environment.apiUrl + 'Account';
  jwtHelper = new JwtHelperService();
  decodedToken: any;
  currentUser?: User | null;

  constructor(private http: HttpClient) { }

  login(model: any) {
    return this.http.post(this.baseUrl + '/login', model).pipe(
      map((response: any) => {
        const user = response;
        if (user) {
          localStorage.setItem('token', user.token);
          localStorage.setItem('user', JSON.stringify(user.user));
          this.decodedToken = this.jwtHelper.decodeToken(user.token);
          this.currentUser = user.user;

          console.log(this.decodedToken);
          console.log(this.currentUser);
        }
      })
    );
  }

  register(user: User) {
    return this.http.post(this.baseUrl + '/register', user);
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    this.decodedToken = null;
    this.currentUser = null;
  }

  isAdmin() {
    return this.decodedToken && this.decodedToken.role === 'Admin';
  }

  isUser() {
    return this.decodedToken && this.decodedToken.role === 'User';
  }

}
