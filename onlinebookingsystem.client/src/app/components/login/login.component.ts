import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  loginForm: FormGroup;
  isLoading: boolean = false;

  constructor(private fb: FormBuilder, public authService: AuthService, private router: Router) {
    this.loginForm = this.fb.group({
      userName: ['', [Validators.required]],
      password: ['', [Validators.required]]
    });
  }

  login() {
    if (this.loginForm.valid) {
      this.isLoading = true;
      this.authService.login(this.loginForm.value).subscribe((next: any) => {
        // this.alertify.success('Logged in successfully');
        // console.log("Login");
        // console.log(this.authService.currentUser);
      }, (error: any) => {
        // this.alertify.error(error);
      }, () => {
        this.isLoading = false;
        this.router.navigate(['/home']);
      });
    }
  }
}
