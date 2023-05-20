using System.Collections.Generic;
using System.Linq;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data.Database;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Services.Services
{
    public class FlightService : EntityService<Flight>, IFlightService
    {
        public FlightService(IFlightPlannerDbContext context) : base(context)
        {
        }

        public Flight GetFlightById(int id)
        {
            return _context.Flights
                .Include(x => x.From)
                .Include(x => x.To)
                .SingleOrDefault(x => x.Id == id);
        }

        public PageResult FindFlight(SearchFlightsRequest request)
        {
            var flights = _context.Flights.SingleOrDefault(f =>
                f.From.AirportName == request.From &&
                f.To.AirportName == request.To &&
                f.DepartureTime.Substring(0, 10) == request.DepartureDate);

            var actingList = new List<int?> { flights?.Id };

            var value = new PageResult
            {
                Items = new List<Flight>()
            };

            if (flights == null)
            {
                value.Page = 0;
                value.TotalItems = 0;
                var targetType = value.Items.Select(fx => new int()).ToList();
                targetType.Add(0);
            }
            else
            {
                value.Page = 0;
                value.TotalItems = actingList.Count;
                value.Items.Add(flights);
            }

            return value;
        }

        public void DeleteFlight(int id)
        {
            var flight = _context.Flights
                .Include(x => x.To)
                .Include(x => x.From)
                .FirstOrDefault(x => x.Id == id);

            if (flight == null) return;
            {
                _context.Flights.Remove(_context.Flights.Include(x => x.From)
                    .Include(x => x.To).
                    First(x => x.Id == id));

                _context.SaveChanges();
            }
        }

        public bool Exist(Flight flight)
        {
            return _context.Flights.Any(x =>
                    x.From.AirportName == flight.From.AirportName &&
                    x.To.AirportName == flight.To.AirportName &&
                    x.DepartureTime == flight.DepartureTime &&
                    x.Carrier == flight.Carrier &&
                    x.ArrivalTime == flight.ArrivalTime);
        }
    }
}
