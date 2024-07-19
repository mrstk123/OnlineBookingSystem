import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { Router } from '@angular/router';
import { Car } from '../../models/car';
import { CarService } from '../../services/car.service';
import { ConfirmationDialogComponent } from '../confirmation-dialog/confirmation-dialog.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-car-list',
  templateUrl: './car-list.component.html',
  styleUrls: ['./car-list.component.css']
})
export class CarListComponent implements OnInit {
  displayedColumns: string[] = ['carName', 'carModel', 'carType', 'carBrand', 'pricePerDay', 'vehicleNumber', 'actions'];
  dataSource: MatTableDataSource<Car>;
  isLoading: boolean = true;


  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(private carService: CarService, public dialog: MatDialog, private router: Router) {
    this.dataSource = new MatTableDataSource<Car>();
  }

  ngOnInit(): void {
    this.loadCars();
  }

  loadCars(): void {
    this.carService.getCars().subscribe((cars: any) => {
      this.dataSource.data = cars;
      this.dataSource.paginator = this.paginator;
      this.isLoading = false;
    });
  }

  editCar(carId: number): void {
    this.router.navigate(['/car-form', carId]);
  }
  openDeleteConfirmation(carId: number) {
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      width: '400px',
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        // Perform delete operation here
        this.deleteCar(carId);
      }
    });
  }

  deleteCar(carId: number): void {
    this.isLoading = true;
    this.carService.deleteCar(carId).subscribe(() => {
      this.loadCars();
    });
  }

  applyFilter(event: Event): void {
    this.isLoading = true;
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
    this.isLoading = false;
  }

  addCar(): void {
    this.router.navigate(['/car-form']);
  }
}
