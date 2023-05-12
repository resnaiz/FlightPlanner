using FlightPlanner.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FlightPlanner.Controllers
{
    [Route("api")]
    [ApiController]
    public class CostumerApiController : BaseApiController
    {
        public CostumerApiController(FlightPlannerDbContext context) : base(context)
        {
        }

        private static readonly object lockMethod = new object();

        [HttpGet]
        [Route("airports")]
        public IActionResult SearchAirports(string search)
        {
            search = search.ToUpper().Trim();

            var airport = _context.Airports.FirstOrDefault(x =>
                x.City.Substring(0, search.Length) == search ||
                x.Country.Substring(0, search.Length) == search ||
                x.AirportName.Substring(0, search.Length) == search);

            Airport[] airports = new Airport[1];
            airports[0] = airport;

            return Ok(airports);
        }

        [HttpPost]
        [Route("flights/search")]
        public IActionResult SearchFlight(SearchFlightsRequest req)
        {
            if (req.To == req.From)
            {
                return BadRequest();
            }

            var flights = _context.Flights
                .Include(f => f.From)
                .Include(f => f.To)
                .Where(f =>
                    f.From.AirportName == req.From
                    && f.To.AirportName == req.To
                )
                .ToList();

            var res = new PageResult();

            if (flights.Count > 0)
            {
                res.Items = flights;
                res.Page = 1;
                res.TotalItems = flights.Count;
            }
            else
            {
                res.Items = new List<Flight>();
                res.Page = 0;
                res.TotalItems = 0;
            }

            return Ok(res);
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult FindFlightById(int id)
        {
            var flight = _context.Flights.
                Include(f => f.From).
                Include(f => f.To).
                FirstOrDefault(f => f.Id == id);

            if (flight == null)
            {
                return NotFound();
            }

            return Ok(flight);
        }
    }
}
