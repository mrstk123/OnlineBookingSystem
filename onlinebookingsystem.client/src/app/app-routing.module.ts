import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { AuthGuard } from './guards/auth.guard';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { BookingComponent } from './components/booking/booking.component';
import { CarFormComponent } from './components/car-form/car-form.component';
import { CarListComponent } from './components/car-list/car-list.component';
import { AdminAuthGuard } from './guards/adminauth.guard';
import { BookingConfirmationComponent } from './components/booking-confirmation/booking-confirmation.component';
import { UserBookingsComponent } from './components/user-bookings/user-bookings.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  {
    path: '',
    canActivate: [AuthGuard],
    children: [
      { path: 'booking/:id', component: BookingComponent },
      { path: 'booking-confirmation/:id', component: BookingConfirmationComponent },
      { path: 'user-bookings', component: UserBookingsComponent },
    ]
  },
  {
    path: '',
    canActivate: [AdminAuthGuard],
    children: [
      { path: 'cars', component: CarListComponent },
      { path: 'car-form', component: CarFormComponent },
      { path: 'car-form/:id', component: CarFormComponent },
    ]
  },
  { path: '**', redirectTo: '', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
