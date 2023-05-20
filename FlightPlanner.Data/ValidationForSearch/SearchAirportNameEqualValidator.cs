using System;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;

namespace FlightPlanner.Data.ValidationForSearch
{
    public class SearchAirportNameEqualValidator : IValidationForSearch
    {
        public bool IsValid(SearchFlightsRequest req)
        {
            return !string.Equals(req.From, req.To, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
