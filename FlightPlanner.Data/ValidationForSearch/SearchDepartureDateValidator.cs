using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;

namespace FlightPlanner.Data.ValidationForSearch
{
    public class SearchDepartureDateValidator : IValidationForSearch
    {
        public bool IsValid(SearchFlightsRequest req)
        {
            return !string.IsNullOrEmpty(req.DepartureDate);
        }
    }
}
