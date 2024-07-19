import { Component } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { CarService } from '../../services/car.service';
import { Car } from '../../models/car';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  cars: any;
  filteredCars: any;
  baseUrl: string = environment.apiUrl.replace('/api/','');
  isLoading: boolean = true;

  carTypes = ['SUV', 'Sedan', 'Coupe', 'Convertible'];
  priceRanges = [
    { label: 'Below $50', value: { min: 0, max: 50 } },
    { label: '$50 - $100', value: { min: 50, max: 100 } },
    { label: 'Above $100', value: { min: 100, max: Infinity } }
  ];

  filterForm: FormGroup;

  constructor(private fb: FormBuilder, private carService: CarService) {
    this.filterForm = this.fb.group({
      carType: [''],
      priceRange: [null]
    });
  }

  ngOnInit(): void {
    this.loadCars();
    // this.filterForm.valueChanges.subscribe(() => {
    //   this.applyFilters();
    // });

  }

  loadCars(): void {
    this.carService.getAvailCars().subscribe((cars : Car[])=> {
      this.cars = cars;
      this.filteredCars = cars;
      this.isLoading = false;
    });
  }

  applyFilters() {
    this.isLoading = true;
    const { carType, priceRange } = this.filterForm.value;

    this.filteredCars = this.cars.filter((car: Car) => {
      const matchesType = carType ? car.carType === carType : true;
      const matchesPrice = priceRange ? car.pricePerDay >= priceRange.min && car.pricePerDay <= priceRange.max : true;
      return matchesType && matchesPrice;
    });
    this.isLoading = false;
  }

  clearFilters(): void {
    this.isLoading = true;
    this.filterForm.reset();
    this.filteredCars = this.cars;
    this.isLoading = false;
  }

}
