using System;
using FlightPlanner.Core.DataTransfer;
using FlightPlanner.Core.Services;

namespace FlightPlanner.Data.Validation
{
    public class AirportNameEqualValidator : IValidation
    {
        public bool IsValid(AddFlightReq req)
        {
            return !string.Equals(req?.From?.Airport?.Trim(), req?.To?.Airport?.Trim(),
                StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
