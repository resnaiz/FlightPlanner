using FlightPlanner.Core.DataTransfer;

namespace FlightPlanner.Core.Services
{
    public interface IValidation
    {
        public bool IsValid(AddFlightReq req);
    }
}
