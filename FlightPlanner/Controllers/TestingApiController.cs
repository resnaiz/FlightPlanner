using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data.Database;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Route("testing-api")]
    [ApiController]
    public class TestingApiController : BaseApiController
    {
        private readonly IClear _clear;

        public TestingApiController(
            IFlightPlannerDbContext context,
            IClear clear) : base(context)
        {
            _clear = clear;
        }

        private static readonly object lockMethod = new object();

        [HttpPost]
        [Route("clear")]
        public IActionResult Clear()
        {
            lock (lockMethod)
            {
                _clear.Clear<Flight>();
                _clear.Clear<Airport>();

                return Ok();
            }
        }
    }
}
