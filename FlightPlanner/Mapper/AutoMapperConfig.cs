using AutoMapper;
using FlightPlanner.Core.DataTransfer;
using FlightPlanner.Core.Models;

namespace FlightPlanner.Mapper
{
    public static class AutoMapperConfig
    {
        public static IMapper CreateMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AddFlightReq, Flight>().ForMember(f => f.Id,
                    opt => opt.Ignore());
                cfg.CreateMap<AddAirportReq, Airport>().ForMember(a => a.AirportName,
                        opt => opt.MapFrom(s => s.Airport))
                    .ForMember(a => a.Id, opt => opt.Ignore());
                cfg.CreateMap<Flight, AddFlightResponse>();
                cfg.CreateMap<Airport, AddAirportResponse>()
                    .ForMember(a => a.Airport, opt => opt.MapFrom(a => a.AirportName));
            });

            config.AssertConfigurationIsValid();

            return config.CreateMapper();
        }
    }
}
