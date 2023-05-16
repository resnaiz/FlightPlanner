using FlightPlanner.Models;
using System;

namespace FlightPlanner.Validation
{
    public class Validator
    {
        public static bool ValidateFlight(Flight flight)
        {
            if (flight.From == null || 
                flight.To == null || 
                flight.Carrier is null or "" || 
                flight.DepartureTime == null ||
                flight.ArrivalTime == null)
            {
                return false;
            }

            if (string.IsNullOrEmpty(flight.From.Country) || 
                string.IsNullOrEmpty(flight.To.Country) || 
                string.IsNullOrEmpty(flight.From.City) || 
                string.IsNullOrEmpty(flight.To.City) || 
                string.IsNullOrEmpty(flight.From.AirportName) || 
                string.IsNullOrEmpty(flight.To.AirportName))
            {
                return false;
            }

            if (string.Equals(flight.From.AirportName.Trim(), flight.To.AirportName.Trim(),
                    StringComparison.CurrentCultureIgnoreCase))
            {
                return false;
            }

            return Convert.ToDateTime(flight.DepartureTime) < Convert.ToDateTime(flight.ArrivalTime);
        }
    }
}
