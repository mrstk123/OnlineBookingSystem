// booking-confirmation.component.ts
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Booking } from '../../models/booking';
import { BookingService } from '../../services/booking.service';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-booking-confirmation',
  templateUrl: './booking-confirmation.component.html',
  styleUrls: ['./booking-confirmation.component.css']
})
export class BookingConfirmationComponent implements OnInit {
  baseUrl: string = environment.apiUrl.replace('/api/', '');
  booking?: Booking;
  isLoading = true;

  constructor(
    private route: ActivatedRoute,
    private bookingService: BookingService
  ) { }

  ngOnInit(): void {
    const bookingId = +(this.route.snapshot.paramMap.get('id') ?? "0");
    this.bookingService.getBookingById(bookingId).subscribe((data: Booking) => {
      this.booking = data;
      this.isLoading = false;
    });
  }
}
