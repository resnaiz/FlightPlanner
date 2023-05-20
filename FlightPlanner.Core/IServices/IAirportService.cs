using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Services
{
    public interface IAirportService : IEntityService<Flight>
    {
        public Airport FindAirport(string airport);
    }
}
