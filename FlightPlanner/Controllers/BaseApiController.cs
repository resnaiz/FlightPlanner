using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    public class BaseApiController : ControllerBase
    {
        protected readonly FlightPlannerDbContext _context;

        public BaseApiController(FlightPlannerDbContext context)
        {
            _context = context;
        }
    }
}
