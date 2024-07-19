using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineBookingSystem.Server.Dtos;
using OnlineBookingSystem.Server.Models;
using OnlineBookingSystem.Server.Repositories.Interfaces;

namespace OnlineCaringSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _environment;

        public CarController(IUnitOfWork unitOfWork, IWebHostEnvironment environment)
        {
            _unitOfWork = unitOfWork;
            _environment = environment;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars()
        {
            var cars = await _unitOfWork.CarRepository.GetAllAsync();
            return Ok(cars);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            var car = await _unitOfWork.CarRepository.GetByIdAsync(id);
            return car == null ? NotFound() : car;
        }

        [HttpGet("GetAvailCars")]
        public async Task<ActionResult<IEnumerable<Car>>> GetAvailCars()
        {
            var cars = await _unitOfWork.CarRepository.FindAsync(x => x.Availability == AvailabilityStatus.Available);
            return Ok(cars);
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<Car>> CreateCar([FromForm] CarDto car)
        {
            string contentRootPath = _environment.ContentRootPath;
            string webRootPath = _environment.WebRootPath;

            if (string.IsNullOrWhiteSpace(_environment.WebRootPath))
            {
                _environment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            }

            try
            {
                string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
                string fileExtension = Path.GetExtension(car.Image?.FileName)?.ToLower() ?? "";
                if (!allowedExtensions.Contains(fileExtension))
                {
                    return BadRequest("Only JPG, JPEG, PNG, and GIF files are allowed.");
                }

                if (car.Image != null && car.Image.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    string filePath = Path.Combine(uploadsFolder, car.Image.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await car.Image.CopyToAsync(stream);
                    }
                    car.ImageUrl = "/uploads/" + car.Image.FileName;
                }

                var newCar = new Car
                {
                    CarName = car.CarName,
                    CarModel = car.CarModel,
                    CarType = car.CarType,
                    CarBrand = car.CarBrand,
                    PricePerDay = car.PricePerDay,
                    ImageUrl = car.ImageUrl ?? string.Empty,
                    VehicleNumber = car.VehicleNumber,
                    NumberOfSeats = car.NumberOfSeats,
                    Transmission = car.Transmission,
                    Availability = car.Availability,
                };

                await _unitOfWork.CarRepository.AddAsync(newCar);
                await _unitOfWork.SaveAsync();
                return CreatedAtAction(nameof(GetCars), new { id = car.CarId }, car);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> UpdateCar(int id, CarDto car)
        {
            if (id != car.CarId)
            {
                return BadRequest();
            }

            var existingCar = await _unitOfWork.CarRepository.GetByIdAsync(id);
            if (existingCar == null)
            {
                return NotFound();
            }

            try
            {
                //existingCar.CarName = car.CarName;
                //existingCar.CarModel = car.CarModel;
                //existingCar.CarType = car.CarType;
                //existingCar.CarBrand = car.CarBrand;
                existingCar.PricePerDay = car.PricePerDay;
                //existingCar.VehicleNumber = car.VehicleNumber;
                existingCar.NumberOfSeats = car.NumberOfSeats;
                existingCar.Transmission = car.Transmission;
                existingCar.Availability = car.Availability;

                // Update image if provided
                if (car.Image != null && car.Image.Length > 0)
                {
                    //// Delete existing image if it exists
                    //if (!string.IsNullOrEmpty(existingCar.ImageUrl))
                    //{
                    //    var imagePath = Path.Combine(_environment.WebRootPath, existingCar.ImageUrl.TrimStart('/'));
                    //    if (System.IO.File.Exists(imagePath))
                    //    {
                    //        System.IO.File.Delete(imagePath);
                    //    }
                    //}

                    // Save new image
                    var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                    string filePath = Path.Combine(uploadsFolder, car.Image.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await car.Image.CopyToAsync(stream);
                    }

                    existingCar.ImageUrl = "/uploads/" + car.Image.FileName;
                }

                await _unitOfWork.CarRepository.UpdateAsync(existingCar);
                await _unitOfWork.SaveAsync();
            }
            catch (DbUpdateConcurrencyException) when (!CarExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            await _unitOfWork.CarRepository.DeleteAsync(id);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        private bool CarExists(int id)
        {
            return _unitOfWork.CarRepository.FindAsync(e => e.CarId == id).GetAwaiter().GetResult().Any();
        }
    }

}
