using System;
using System.Collections.Generic;
using System.Linq;
using FlightPlanner.Models;

namespace FlightPlanner.Storage
{
    public static class FlightStorage
    {
        private static readonly object _locker = new object();
        private static readonly List<Flight> _flights = new List<Flight>();
        private static int _id;

        public static Flight AddFlight(AddFlightRequest request)
        {
            lock (_locker)
            {
                var flight = new Flight
                {
                    From = request.From,
                    To = request.To,
                    ArrivalTime = request.ArrivalTime,
                    DepartureTime = request.DepartureTime,
                    Carrier = request.Carrier,
                    Id = ++_id
                };

                _flights.Add(flight);
                return flight;
            }
        }

        public static Flight GetFlight(int id)
        {
            lock (_locker)
            {
                return _flights.SingleOrDefault(x => x.Id == id);
            }
        }

        public static void ClearFlights()
        {
            lock (_locker)
            {
                _flights.Clear();
                _id = 0;
            }
        }

        public static bool IsFound(AddFlightRequest req)
        {
            lock (_locker)
            {
                return _flights.Any(flight =>
                    flight.Carrier?.ToLower().Trim() == req?.Carrier?.ToLower()?.Trim() &&
                    flight.DepartureTime == req?.DepartureTime &&
                    flight.ArrivalTime == req?.ArrivalTime &&
                    flight.To?.AirportName?.ToLower()?.Trim() == req?.To?.AirportName?.ToLower()?.Trim() &&
                    flight.From?.AirportName?.ToLower()?.Trim() == req?.From?.AirportName?.ToLower()?.Trim());
            }
        }

        public static bool IsValidRequest(AddFlightRequest req)
        {
            lock (_locker)
            {
                if (req == null || req.To == null || req.From == null)
                {
                    return false;
                }

                if (string.IsNullOrEmpty(req.To.AirportName) ||
                    string.IsNullOrEmpty(req.To.Country) ||
                    string.IsNullOrEmpty(req.To.City) ||
                    string.IsNullOrEmpty(req.From.AirportName) ||
                    string.IsNullOrEmpty(req.From.Country) ||
                    string.IsNullOrEmpty(req.From.City) ||
                    string.IsNullOrEmpty(req.Carrier) ||
                    string.IsNullOrEmpty(req.DepartureTime) ||
                    string.IsNullOrEmpty(req.ArrivalTime))
                {
                    return false;
                }

                if (req.From.AirportName.ToLower().Trim() == req.To.AirportName.ToLower().Trim())
                {
                    return false;
                }

                var departure = DateTime.Parse(req.DepartureTime);
                var arrival = DateTime.Parse(req.ArrivalTime);

                if (departure >= arrival)
                {
                    return false;
                }

                return true;
            }
        }

        public static void DeleteFlight(int id)
        {
            lock (_locker)
            {
                var flight = GetFlight(id);

                if (flight != null)
                {
                    _flights.Remove(flight);
                }
            }
        }

        public static List<Airport> SearchAirports(string search)
        {
            lock (_locker)
            {
                search = search.ToLower().Trim();

                var fromAirports = new List<Airport>();

                foreach (var flight in _flights)
                {
                    if (flight.From.AirportName.ToLower().Trim().Contains(search) ||
                        flight.From.City.ToLower().Trim().Contains(search) ||
                        flight.From.Country.ToLower().Trim().Contains(search))
                    {
                        fromAirports.Add(flight.From);
                    }
                }

                var toAirports = new List<Airport>();

                foreach (var flight in _flights)
                {
                    if (flight.To.AirportName.ToLower().Trim().Contains(search) ||
                        flight.To.City.ToLower().Trim().Contains(search) ||
                        flight.To.Country.ToLower().Trim().Contains(search))
                    {
                        toAirports.Add(flight.To);
                    }
                }

                var airports = new List<Airport>();
                airports.AddRange(toAirports);
                airports.AddRange(fromAirports);
                return airports;
            }
        }

        public static List<Flight> SearchFlights()
        {
            lock (_locker)
            {
                return _flights.ToList();
            }
        }
    }
}
