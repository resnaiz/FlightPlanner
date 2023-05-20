using System;
using FlightPlanner.Core.DataTransfer;
using FlightPlanner.Core.Services;

namespace FlightPlanner.Data.Validation
{
    public class TimeValidator : IValidation
    {
        public bool IsValid(AddFlightReq req)
        {
            try
            {
                return DateTime.Parse(req.ArrivalTime) > DateTime.Parse(req.DepartureTime);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
