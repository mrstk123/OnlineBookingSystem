// src/app/components/booking/booking.component.ts
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BookingService } from '../../services/booking.service';
import { Booking } from '../../models/booking';
import { CarService } from '../../services/car.service';
import { User } from '../../models/user';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-booking',
  templateUrl: './booking.component.html',
  styleUrls: ['./booking.component.css']
})
export class BookingComponent implements OnInit {
  bookingForm: FormGroup;
  carId!: number;
  rentalRate: number = 100;
  totalCost: number = 0;
  isLoading: boolean = true;

  constructor(
    private fb: FormBuilder,
    private bookingService: BookingService,
    private carService: CarService,
    private authService: AuthService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.bookingForm = this.fb.group({
      // bookingNo: [{ value: '', disabled: true }],
      bookingNo: '',
      customerName: ['', Validators.required],
      vehicleNo: '',
      numberOfSeats: '',
      transmission: '',
      rentalRate: '',
      rentalStartDate: ['', Validators.required],
      rentalEndDate: ['', Validators.required],
      totalCost: [0]
      // totalCost: { value: '', disabled: true }
    });
  }

  ngOnInit(): void {
    this.bookingService.getBookingNumber().subscribe(bookingNumber => {
      this.bookingForm.patchValue({
        bookingNo: bookingNumber as string
      });
    });

    this.route.paramMap.subscribe(params => {
      this.carId = Number(params.get('id'));
      // Fetch car details by this.carId and populate vehicleNo and specifications fields
      this.carService.getCar(this.carId).subscribe(car => {
        this.bookingForm.patchValue({
          vehicleNo: car.vehicleNumber,
          numberOfSeats: car.numberOfSeats,
          transmission: car.transmission,
          rentalRate: car.pricePerDay
        });
        this.rentalRate = car.pricePerDay;
        this.isLoading = false;
      });
    });

    this.bookingForm.get('rentalStartDate')?.valueChanges.subscribe(() => {
      this.calculateTotalCost();
    });
    this.bookingForm.get('rentalEndDate')?.valueChanges.subscribe(() => {
      this.calculateTotalCost();
    });
  }

  calculateTotalCost(): void {
    const rentalStartDate = this.bookingForm.get('rentalStartDate')?.value;
    const rentalEndDate = this.bookingForm.get('rentalEndDate')?.value;
    if (rentalStartDate && rentalEndDate) {
      const start = new Date(rentalStartDate);
      const end = new Date(rentalEndDate);
      const diffInTime = end.getTime() - start.getTime();
      const diffInDays = diffInTime / (1000 * 3600 * 24) + 1;
      this.totalCost = (diffInDays) * this.rentalRate;
      this.bookingForm.patchValue({ totalCost: this.totalCost });
    }
  }

  onSubmit(): void {
    if (this.bookingForm.valid) {
      this.isLoading = true;
      const userId = this.authService.currentUser?.userId ?? 0;

      const booking: Booking = this.bookingForm.getRawValue();
      booking.carId = this.carId;
      booking.bookingNo = booking.bookingNo.toString();
      booking.userId = userId;
      // booking.rentalStartDate = new Date(booking.rentalStartDate.toUTCString());
      // booking.rentalEndDate = new Date(booking.rentalStartDate.toUTCString());
      this.bookingService.createBooking(booking).subscribe((data: Booking) => {
        // console.log(data)
        this.router.navigate(['/booking-confirmation', data.bookingId]);
        this.isLoading = false;

      });
      this.isLoading = true;

    }
  }

  cancel(): void {
    this.router.navigate(['/home']);
  }
}
