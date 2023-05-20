using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FlightPlanner.Core.DataTransfer;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Route("admin-api")]
    [ApiController]
    [Authorize]
    public class AdminApiController : BaseApiController
    {
        private readonly IFlightService _flightService;
        private readonly IMapper _mapper;
        private readonly IEnumerable<IValidation> _validators;
        private static readonly object lockMethod = new object();

        public AdminApiController(
            IFlightPlannerDbContext context,
            IFlightService flightService,
            IMapper mapper,
            IEnumerable<IValidation> validators) : base(context)
        {
            _flightService = flightService;
            _mapper = mapper;
            _validators = validators;
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult GetFlights(int id)
        {
            var flight = _flightService.GetById(id);

            if (flight == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<AddFlightResponse>(flight));
        }

        [HttpPut]
        [Route("flights")]
        [Authorize]
        public IActionResult AddingFlights(AddFlightReq request)
        {
            lock (lockMethod)
            {
                if (!_validators.All(x => x.IsValid(request)))
                {
                    return BadRequest();
                }

                var flight = _mapper.Map<Flight>(request);

                if (_flightService.Exist(flight))
                {
                    return Conflict();
                }

                _flightService.Create(flight);

                return Created("", _mapper.Map<AddFlightResponse>(flight));
            }
        }

        [HttpDelete]
        [Route("flights/{id}")]
        public IActionResult DeleteFlights(int id)
        {
            lock (lockMethod)
            {
                _flightService.DeleteFlight(id);

                return Ok();
            }
        }
    }
}
