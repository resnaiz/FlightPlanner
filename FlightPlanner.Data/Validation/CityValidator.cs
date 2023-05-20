using FlightPlanner.Core.DataTransfer;
using FlightPlanner.Core.Services;

namespace FlightPlanner.Data.Validation
{
    public class CityValidator : IValidation
    {
        public bool IsValid(AddFlightReq req)
        {
            return !string.IsNullOrEmpty(req?.From?.City)
                   && !string.IsNullOrEmpty(req?.To?.City);
        }
    }
}
