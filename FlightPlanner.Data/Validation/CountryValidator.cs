using FlightPlanner.Core.DataTransfer;
using FlightPlanner.Core.Services;

namespace FlightPlanner.Data.Validation
{
    public class CountryValidator : IValidation
    {
        public bool IsValid(AddFlightReq req)
        {
            return !string.IsNullOrEmpty(req?.To?.Country) 
                   && !string.IsNullOrEmpty(req?.From?.Country);
        }
    }
}
