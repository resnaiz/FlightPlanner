using FlightPlanner.Core.DataTransfer;
using FlightPlanner.Core.Services;

namespace FlightPlanner.Data.Validation
{
    public class AirportNameValidator : IValidation
    {
        public bool IsValid(AddFlightReq req)
        {
            return !string.IsNullOrEmpty(req?.To?.Airport)
                   && !string.IsNullOrEmpty(req?.From?.Airport);
        }
    }
}

