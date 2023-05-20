using FlightPlanner.Data.Database;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    public class BaseApiController : ControllerBase
    {
        protected readonly FlightPlannerDbContext _context;

        public BaseApiController(IFlightPlannerDbContext context)
        {
            Context = context;
        }

        public IFlightPlannerDbContext Context { get; }
    }
}
