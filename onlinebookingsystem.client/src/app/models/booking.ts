export interface Booking {
  bookingId: number;
  bookingNo: string;
  customerName: string;
  vehicleNo: string;
  numberOfSeats: number;
  transmission: string;
  rentalRate: number;
  rentalStartDate: Date;
  rentalEndDate:  Date;
  totalCost: number;
  carId: number;
  userId: number;
  
  car: {
    carId: number;
    carModel: string;
    vehicleNumber: string;
    pricePerDay: number;
    imageUrl: string;
  };
}
