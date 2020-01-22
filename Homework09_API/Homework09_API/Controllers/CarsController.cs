using Homework09_API.Data;
using Homework09_API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Homework09_API.Controllers
{
    public class BookDate
    {
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

    }

    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly CarBookingDbContext _context;

        public CarsController(CarBookingDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCar()
        {
            return await _context.Car.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            var car = await _context.Car.FindAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            return car;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(int id, Car car)
        {
            if (id != car.CarId)
            {
                return BadRequest();
            }

            _context.Entry(car).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(Car car)
        {
            _context.Car.Add(car);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCar", new { id = car.CarId }, car);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Car>> DeleteCar(int id)
        {
            var car = await _context.Car.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            _context.Car.Remove(car);
            await _context.SaveChangesAsync();

            return car;
        }

        private bool CarExists(int id)
        {
            return _context.Car.Any(e => e.CarId == id);
        }


        //Self-created methods
        [HttpGet("avail/{date}")]
        public async Task<ActionResult<IEnumerable<Car>>> GetCarsForDay(string date)
        {
            DateTime curDate = DateTime.Parse(date);

            var cars = await _context.Car.Include(c => c.Bookings).ToListAsync();
            return Ok(cars.Where(c => !c.Bookings.Any(b => b.Date.Year == curDate.Year && b.Date.Month == curDate.Month && b.Date.Day == curDate.Day)).ToList());
        }

        [HttpPut("{id}/book")]
        public async Task<IActionResult> BookCar(int id, [FromBody] BookDate date)
        {
            if (!CarExists(id))
            {
                return NotFound();
            }

            Car curCar = await _context.Car.Include(c => c.Bookings).FirstAsync(c => c.CarId == id);

            DateTime curDate = date.Date;
            if (curCar.Bookings.Any(b => b.Date.Year == curDate.Year && b.Date.Month == curDate.Month && b.Date.Day == curDate.Day))
            {
                return BadRequest("The car has already been booked for that day");
            }

            curCar.Bookings.Add(new Booking { Date = curDate });
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
