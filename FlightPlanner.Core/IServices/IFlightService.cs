using FlightPlanner.Core.Models;
using System.Collections.Generic;

namespace FlightPlanner.Core.Services
{
    public interface IFlightService : IEntityService<Flight>
    {
        Flight GetFlightById(int id);
        void DeleteFlight(int id);
        PageResult FindFlight(SearchFlightsRequest request);
        bool Exist(Flight flight);
    }
}
