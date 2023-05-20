using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Services
{
    public interface IValidationForSearch
    { 
        bool IsValid(SearchFlightsRequest req);
    }
}
