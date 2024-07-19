import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Car } from '../models/car';

@Injectable({
  providedIn: 'root'
})
export class CarService {
  private apiUrl = environment.apiUrl + "Car";

  constructor(private http: HttpClient) { }

  getCars(): Observable<Car[]> {
    return this.http.get<Car[]>(this.apiUrl);
  }

  getAvailCars(): Observable<Car[]> {
    return this.http.get<Car[]>(this.apiUrl + "/GetAvailCars");
  }

  getCar(id: number): Observable<Car> {
    return this.http.get<Car>(`${this.apiUrl}/${id}`);
  }

  addCar(car: Car, imageFile: File): Observable<Car> {
    const formData = this.buildFormData(car, imageFile);
    return this.http.post<Car>(this.apiUrl, formData);
  }

  updateCar(car: Car, imageFile: File | null): Observable<Car> {
    const formData = this.buildFormData(car, imageFile);
    return this.http.put<Car>(`${this.apiUrl}/${car.carId}`, formData);
  }

  deleteCar(id: number) {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }

  private buildFormData(car: Car, imageFile: File | null): FormData {
    const formData = new FormData();
    for (const [key, value] of Object.entries(car)) {
      formData.append(key, value as string);
    }

    if (imageFile) {
      formData.append('image', imageFile);
    }

    return formData;
  }
}
