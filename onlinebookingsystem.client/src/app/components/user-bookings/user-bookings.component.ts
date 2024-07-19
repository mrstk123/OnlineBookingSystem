import { Component, OnInit } from '@angular/core';
import { Booking } from '../../models/booking';
import { AuthService } from '../../services/auth.service';
import { BookingService } from '../../services/booking.service';

@Component({
  selector: 'app-user-bookings',
  templateUrl: './user-bookings.component.html',
  styleUrl: './user-bookings.component.css'
})
export class UserBookingsComponent implements OnInit {
  bookings: Booking[] = [];
  displayedColumns: string[] = ['bookingNo', 'customerName', 'carModel', 'vehicleNumber', 'rentalRate', 'rentalPeriod'];
  isLoading: boolean = true;

  constructor(private bookingService: BookingService, private authService: AuthService) {}

  ngOnInit(): void {
    const userId = this.authService.currentUser?.userId;
    if (userId) {
      this.bookingService.getBookingsByUser(userId).subscribe(
        (bookings) => {
          this.bookings = bookings;
        },
        (error) => {
          console.error('Error fetching bookings:', error);
        },
        ()=> {
          this.isLoading = false;
        }
      );
    }
  }
}
