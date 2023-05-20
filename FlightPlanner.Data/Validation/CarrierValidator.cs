using FlightPlanner.Core.DataTransfer;
using FlightPlanner.Core.Services;

namespace FlightPlanner.Data.Validation
{
    public class CarrierValidator : IValidation
    {
        public bool IsValid(AddFlightReq req)
        {
            return !string.IsNullOrEmpty(req.Carrier);
        }
    }
}
