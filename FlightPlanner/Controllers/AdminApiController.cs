﻿using FlightPlanner.Models;
using FlightPlanner.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Route("admin-api")]
    [ApiController]
    [Authorize]
    public class AdminApiController : ControllerBase
    {
        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult GetFlights(int id)
        {
            var flight = FlightStorage.GetFlight(id);

            if (flight == null)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpPut]
        [Route("flights")]
        [Authorize]
        public IActionResult AddingFlights(AddFlightRequest request)
        {
            if (!FlightStorage.IsValidRequest(request))
            {
                return BadRequest();
            }

            if (FlightStorage.IsFound(request))
            {
                return Conflict();
            }

            return Created("", FlightStorage.AddFlight(request));
        }

        [HttpDelete]
        [Route("flights/{id}")]
        public IActionResult DeleteFlights(int id)
        {
            FlightStorage.DeleteFlight(id);
            return Ok();
        }
    }
}
