using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FlightPlanner.Core.DataTransfer;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data.Database;

namespace FlightPlanner.Controllers
{
    [Route("api")]
    [ApiController]
    public class CostumerApiController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IAirportService _airportService;
        private readonly IFlightService _flightService;
        private readonly IEnumerable<IValidationForSearch> _validators;

        public CostumerApiController(
            IFlightPlannerDbContext context,
            IFlightService flightService,
            IAirportService airportService,
            IMapper mapper,
            IEnumerable<IValidationForSearch> searchVal) : base(context)
        {
            _flightService = flightService;
            _airportService = airportService;
            _mapper = mapper;
            _validators = searchVal;
        }

        private static readonly object lockMethod = new object();

        [HttpGet]
        [Route("airports")]
        public IActionResult SearchAirports(string search)
        {
            var airport = _airportService.FindAirport(search);
            var map = _mapper.Map<AddAirportResponse>(airport);

            if (map == null)
            {
                return NotFound();
            }

            var airports = new List<AddAirportResponse> { map };

            return Ok(airports);
        }

        [HttpPost]
        [Route("flights/search")]
        public IActionResult SearchFlight(SearchFlightsRequest req)
        {
            if (!_validators.All(x => x.IsValid(req)))
            {
                return BadRequest();
            }  

            if (req.From == req.To)
            {
                return BadRequest();
            }

            return Ok(_flightService.FindFlight(req));
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult FindFlightById(int id)
        {
            var flightId = _flightService.GetFlightById(id);
            var flight = _mapper.Map<AddFlightResponse>(flightId);

            return flight != null ? Ok(_mapper.Map<AddFlightResponse>(flight)) : NotFound();
        }
    }
}
