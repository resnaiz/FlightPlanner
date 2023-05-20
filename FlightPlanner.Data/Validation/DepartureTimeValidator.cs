using FlightPlanner.Core.DataTransfer;
using FlightPlanner.Core.Services;

namespace FlightPlanner.Data.Validation
{
    public class DepartureTimeValidator : IValidation
    {
        public bool IsValid(AddFlightReq req)
        {
            return !string.IsNullOrEmpty(req.DepartureTime);
        }
    }
}
