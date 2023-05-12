using System.Linq;
using FlightPlanner.Models;
using FlightPlanner.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Controllers
{
    [Route("admin-api")]
    [ApiController]
    [Authorize]
    public class AdminApiController : BaseApiController
    {
        private static readonly object lockMethod = new object();
        public AdminApiController(FlightPlannerDbContext context) : base(context)
        {
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult GetFlights(int id)
        {
            var flight = _context.Flights
                    .Include(x => x.From)
                    .Include(x => x.To)
                    .SingleOrDefault(x => x.Id == id);

            if (flight == null)
            {
                return NotFound();
            }

            return Ok(flight);
        }

        [HttpPut]
        [Route("flights")]
        [Authorize]
        public IActionResult AddingFlights([FromBody] Flight flight)
        {
            lock (lockMethod)
            {
                var checkValid = Validator.ValidateFlight(flight);

                if (!checkValid)
                {
                    return BadRequest();
                }

                var existingFlight = _context.Flights.FirstOrDefault(x => x.From.AirportName == flight.From.AirportName &&
                                                                          x.To.AirportName == flight.To.AirportName &&
                                                                          x.DepartureTime == flight.DepartureTime);

                if (existingFlight != null)
                {
                    return Conflict();
                }

                _context.Flights.Add(flight);
                _context.SaveChanges();

                return Created("", flight);
            }
        }

        [HttpDelete]
        [Route("flights/{id}")]
        public IActionResult DeleteFlights(int id)
        {
            lock (lockMethod)
            {
                var flight = _context.Flights
                    .Include(a => a.From)
                    .Include(a => a.To)
                    .SingleOrDefault(f => f.Id == id);

                if (flight != null)
                {
                    _context.Airports.Remove(flight.From);
                    _context.Airports.Remove(flight.To);
                    _context.Flights.Remove(flight);
                    _context.SaveChanges();
                }

                return Ok();
            }
        }
    }
}
