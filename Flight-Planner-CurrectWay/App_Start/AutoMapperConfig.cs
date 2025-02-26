﻿using AutoMapper;
using Flight_Planner_CurrectWay.Models;
using Fligth_Planner.Core.Models;

namespace Flight_Planner_CurrectWay.App_Start
{
    public class AutoMapperConfig
    {
        public static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AirportRequest, Airport>()
                    .ForMember(d => d.AirportCode,
                        s => s.MapFrom(p => p.Airport))
                    .ForMember(d => d.Id, s => s.Ignore());
                cfg.CreateMap<Airport, AirportRequest>()
                    .ForMember(d => d.Airport,
                    s => s.MapFrom(p => p.AirportCode));
                cfg.CreateMap<FlightRequest, Flight>();
                cfg.CreateMap<Flight, FlightRequest>();
                cfg.CreateMap<AirportResponse, AirportRequest>().ForMember(d => d.Id,
                    opt => opt.Ignore());
                cfg.CreateMap<AirportRequest, AirportResponse>();
                cfg.CreateMap<FlightRequest, FlightResponse>();
                cfg.CreateMap<Airport, AirportResponse>().ForMember(m => m.Airport,
                    opt => opt.MapFrom(s => s.AirportCode));
                cfg.CreateMap<AirportResponse, Airport>().ForMember(m => m.Id,
                        opt => opt.Ignore())
                    .ForMember(m => m.AirportCode, 
                        opt => opt.MapFrom(s => s.Airport));
                cfg.CreateMap<Flight, FlightResponse>();
            });
            config.AssertConfigurationIsValid();
            var mapper = config.CreateMapper();
            return mapper;
        }
    }
}