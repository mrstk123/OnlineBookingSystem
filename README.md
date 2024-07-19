# Online Booking System

## Overview

This project is an online booking system for cars, allowing users to browse available cars, book a car, and manage their bookings. The application includes both a backend (ASP.NET Core) and a frontend (Angular).

## Architecture

The system is divided into two main parts:

1. **Backend**: Built with ASP.NET Core, it provides RESTful APIs for car listings, bookings, and user management. It uses the Repository and Unit of Work patterns to ensure clean code structure and maintainability. JWT authentication is implemented to secure the endpoints.

2. **Frontend**: Developed with Angular, it provides a responsive user interface for browsing cars, making bookings, viewing booking confirmations, and managing car inventory for admins.

## Technologies Used

### Backend

- ASP.NET Core (version 8.x)
- Entity Framework Core
- SQL Server
- Repository Pattern
- Unit of Work Pattern
- JWT Authentication

### Frontend

- Angular (version 18.x)
- Angular Material
- TypeScript
- HTML/CSS

## Operating the System

1. **Browse Cars**: Users can view available cars on the home page.
2. **Make a Booking**: Users can select a car, choose booking dates, and submit a booking.
3. **View Booking Confirmation**: After booking, users can see the booking details on the confirmation page.
4. **Manage Bookings**: Users can view and manage their bookings on the 'My Bookings' page.
5. **Admin View**: Admins can add, remove, or update car inventory via the admin interface.


## Instructions for Setting Up and Running the Application

### Prerequisites

- Node.js (version 22.x)
- Angular CLI (version 18.x)
- .NET SDK (version 8.x)
- SQL Server
- Git

### Backend Setup (ASP.NET Core)

1. Clone the Repository
```bash
git clone https://github.com/mrstk123/OnlineBookingSystem.git
cd OnlineBookingSystem-master/OnlineBookingSystem.Server
```

2. Restore NuGet Packages
```bash
dotnet restore
```

3. Update the Database Connection String

Modify the appsettings.json file to update the connection string for your SQL Server database.

```bash
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=<your-server>;Database=<your-database>;User Id=<your-user>;Password=<your-password>;"
  }
}
```

4. Run the Application
```bash
dotnet run
```
The backend server will start at https://localhost:7041 or a different port if specified.

### Frontend Setup (Angular)

1. Navigate to the Frontend Directory
```bash
cd OnlineBookingSystem-master/onlinebookingsystem.client
```

2. Install Node.js Dependencies
```bash
npm install
```

3. Update API Endpoint

Modify the environment.ts file in src/environments to update the API endpoint. (Update host and apiUrl)
```bash
export const environment = {
  production: false,
  host: "localhost:7041",
  apiUrl: 'https://localhost:7041/api/'
};
```

4. Run the Angular Application
```bash
ng serve
```
The frontend server will start at http://localhost:4200


### Potential Improvements
1. Enhanced Admin Dashboard: Add more detailed statistics and reports for admins.
2. Notifications: Add email or SMS notifications for booking confirmations and updates.
3. Additional Filters: Enhance the car browsing experience with more filters (e.g., car type, fuel type).
4. Internationalization: Support multiple languages for a wider audience.
