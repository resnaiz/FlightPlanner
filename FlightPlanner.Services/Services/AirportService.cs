using System.Linq;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data.Database;

namespace FlightPlanner.Services.Services
{
    public class AirportService : EntityService<Flight>, IAirportService
    {
        public AirportService(IFlightPlannerDbContext context) : base(context)
        {
        }

        public Airport FindAirport(string airport)
        {
            var airportChanged = airport.Trim().ToLower();

            if (string.IsNullOrEmpty(airportChanged)) return null;
            if (!_context.Airports.Any()) return null;
            var newAirport = _context.Airports.FirstOrDefault(f =>
                f.AirportName.ToLower().Contains(airportChanged)
                || f.Country.ToLower().Contains(airportChanged)
                || f.City.ToLower().Contains(airportChanged));

            return newAirport;

        }
    }
}
