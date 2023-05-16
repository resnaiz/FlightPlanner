using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Route("testing-api")]
    [ApiController]
    public class TestingApiController : BaseApiController
    {
        public TestingApiController(FlightPlannerDbContext context) : base(context)
        {
        }

        private static readonly object lockMethod = new object();

        [HttpPost]
        [Route("clear")]
        public IActionResult Clear()
        {
            lock (lockMethod)
            {
                _context.Flights.RemoveRange(_context.Flights);
                _context.Airports.RemoveRange(_context.Airports);
                _context.SaveChanges();

                return Ok();
            }
        }
    }
}
