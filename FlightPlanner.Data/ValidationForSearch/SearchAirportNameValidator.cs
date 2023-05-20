using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;

namespace FlightPlanner.Data.ValidationForSearch
{
    public class SearchAirportNameValidator : IValidationForSearch
    {
        public bool IsValid(SearchFlightsRequest req)
        {
            return !string.IsNullOrEmpty(req?.To) &&
                   !string.IsNullOrEmpty(req?.From);
        }
    }
}
