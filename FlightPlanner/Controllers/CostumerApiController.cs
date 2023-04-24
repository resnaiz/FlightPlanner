using FlightPlanner.Models;
using FlightPlanner.Storage;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Route("api")]
    [ApiController]
    public class CostumerApiController : ControllerBase
    {
        [HttpGet]
        [Route("airports")]
        public IActionResult SearchAirports(string search)
        {
            var airports = FlightStorage.SearchAirports(search);

            if (airports != null)
            {
                return Ok(airports);
            }

            return NotFound();
        }

        [HttpPost]
        [Route("flights/search")]
        public IActionResult SearchFlight(SearchFlightsRequest req)
        {
            if (req.To == req.From)
            {
                return BadRequest();
            }

            var flights = FlightStorage.SearchFlights();

            if (flights.Count == 0)
            {
                return Ok(new PageResult());
            }

            var firstPage = new PageResult
            {
                TotalItems = flights.Count
            };

            return Ok(firstPage);
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult FindFlightById(int id)
        {
            var flight = FlightStorage.GetFlight(id);

            if (flight == null)
            {
                return NotFound();
            }

            return Ok(flight);
        }
    }
}
