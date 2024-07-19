import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CarService } from '../../services/car.service';
import { Car } from '../../models/car';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-car-form',
  templateUrl: './car-form.component.html',
  styleUrls: ['./car-form.component.css']
})
export class CarFormComponent implements OnInit {
  carForm: FormGroup;
  carId: number | null = null;
  isEditMode: boolean = false;
  availabilityStatuses = ['Available', 'Booked', 'Maintenance'];
  selectedFile: File | null = null;
  imageUrl: string | null = null;
  baseUrl: string = environment.apiUrl.replace('/api/', '');
  isLoading: boolean = true;

  constructor(
    private fb: FormBuilder,
    private carService: CarService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.carForm = this.fb.group({
      carName: ['', Validators.required],
      carModel: ['', Validators.required],
      carType: ['', Validators.required],
      carBrand: ['', Validators.required],
      pricePerDay: [0, [Validators.required, Validators.min(0)]],
      imageUrl: '',
      vehicleNumber: ['', Validators.required],
      numberOfSeats: [4, [Validators.required, Validators.min(1)]],
      transmission: ['', Validators.required],
      availability: [this.availabilityStatuses[0], Validators.required]
    });
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const id = params.get('id');
      if (id) {
        this.carId = +id;
        this.isEditMode = true;
        this.loadCar(this.carId);
      } else {
        this.isEditMode = false;
        this.isLoading = false;
      }

    });
  }

  loadCar(id: number): void {
    this.carService.getCar(id).subscribe((car: any) => {
      car.availability = this.availabilityStatuses[car.availability];
      this.carForm.patchValue(car);
      this.imageUrl = `${this.baseUrl}${car.imageUrl}`;
      this.isLoading = false;
    });
  }


  onFileChange(event: any): void {
    if (event.target.files.length > 0) {
      this.selectedFile = event.target.files[0];
      const reader = new FileReader();
      reader.onload = (e: any) => {
        this.imageUrl = e.target.result;
      };
      reader.readAsDataURL(this.selectedFile!);
    }
  }

  onSubmit(): void {
    if (this.carForm.valid) {
      this.isLoading = true;

      if (this.isEditMode) {
        const updatedCar: Car = { carId: this.carId!, ...this.carForm.value };

        this.carService.updateCar(updatedCar, this.selectedFile).subscribe(() => {
          this.isLoading = false; 
          this.router.navigate(['/cars']);
        });
      } else {
        if (this.selectedFile) {
          this.carService.addCar(this.carForm.value, this.selectedFile).subscribe(() => {
            this.isLoading = false;
            this.router.navigate(['/cars']);
          });
        }
        this.isLoading = false;
      }
    }
  }

  cancel(): void {
    this.router.navigate(['/cars']);
  }
}
