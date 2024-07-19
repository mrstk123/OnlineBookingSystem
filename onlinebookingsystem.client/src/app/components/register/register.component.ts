import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators, AbstractControl } from '@angular/forms';
import { User } from '../../models/user';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  registerForm: FormGroup;
  user!: User;
  isLoading: boolean = false;

  constructor(private authService: AuthService, private router: Router, private fb: FormBuilder) {
    this.registerForm = this.fb.group({
      userName: ['', [Validators.required]],
      password: ['', [Validators.required]],
      confirmPassword: ['', [Validators.required]],
      firstName: ['', [Validators.required]],
      lastName: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      phone: ['', [Validators.required]],
    }, { validator: this.passwordMatchValidator });
  }

  passwordMatchValidator(control: AbstractControl): { [key: string]: boolean } | null {
    const password = control.get('password');
    const confirmPassword = control.get('confirmPassword');
    if (password && confirmPassword && password.value !== confirmPassword.value) {
      confirmPassword.setErrors({ match: true });
      return { match: true };
    }
    return null;
  }

  register() {
    if (this.registerForm.valid) {
      this.isLoading = true;
      this.user = Object.assign({}, this.registerForm.value);
      // console.log(this.user);
      this.authService.register(this.user).subscribe(() => {
        // this.alertify.success('Registration successful');
      }, error => {
        // this.alertify.error(error);
      }, () => {
        this.isLoading = false;
        this.authService.login(this.user).subscribe(() => {
          this.router.navigate(['/home']);
        });
      });
    }
  }


}
