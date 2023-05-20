using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data.Database;

namespace FlightPlanner.Services.Services
{
    public class ClearFunction : DbService, IClear
    {
        public ClearFunction(IFlightPlannerDbContext context) : base(context)
        {
        }

        public void Clear<T>() where T : Entity
        {
            _context.Set<T>().RemoveRange(_context.Set<T>());
            _context.SaveChanges();
        }
    }
}
