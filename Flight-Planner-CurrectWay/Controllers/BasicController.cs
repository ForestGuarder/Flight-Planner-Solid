using System.Web.Http;
using AutoMapper;
using Fligth_Planner.Core.Services;

namespace Flight_Planner_CurrectWay.Controllers
{
    public class BasicController : ApiController
    {
        protected readonly IAirportService _airportService;
        protected readonly IFlightService _flightService;
        protected readonly IMapper _mapper;

        public BasicController(IFlightService flightService, IAirportService airportService, IMapper mapper)
        {
            _flightService = flightService;
            _airportService = airportService;
            _mapper = mapper;
        }
    }
}
