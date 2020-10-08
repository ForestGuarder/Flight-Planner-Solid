using System.Linq;
using System.Web.Http;
using AutoMapper;
using Flight_Planner_CurrectWay.Models;
using Fligth_Planner.Core.Models;
using Fligth_Planner.Core.Services;

namespace Flight_Planner_CurrectWay.Controllers
{
    public class TestingController : ApiController
    {
        protected readonly IClearDbService<Flight> _clearFlyDbService;
        protected readonly IClearDbService<Airport> _clearAirDbService;
        protected readonly IFlightService _flightService;
        protected readonly IMapper _mapper;

        public TestingController(IClearDbService<Flight> clearFlyDvService, IClearDbService<Airport> clearAirDbService,
            IFlightService flightService, IMapper mapper)
        {
            _clearAirDbService = clearAirDbService;
            _clearFlyDbService = clearFlyDvService;
            _flightService = flightService;
            _mapper = mapper;
        }

        [HttpPost, Route("testing-api/clear")]
        public IHttpActionResult Clear()
        {
            _clearFlyDbService.ClearDb();
            _clearAirDbService.ClearDb();
            return Ok();
        }

        [HttpPost, Route("testing-api/flights")]
        public IHttpActionResult Post(FlightRequest flight)
        {
            if (_flightService.Query().Any(f => f.To.AirportCode == flight.To.Airport &&
                                              f.To.City == flight.To.City && f.To.Country == flight.To.Country &&
                                              f.From.AirportCode == flight.From.Airport &&
                                              f.From.City == flight.From.City &&
                                              f.From.Country == flight.From.Country &&
                                              f.ArrivalTime == flight.ArrivalTime &&
                                              f.DepartureTime == flight.DepartureTime &&
                                              f.Carrier == flight.Carrier))
            {
                Flight userFlight = _flightService.Query().FirstOrDefault(f => f.To.AirportCode == flight.To.Airport &&
                                                                           f.To.City == flight.To.City && f.To.Country == flight.To.Country &&
                                                                           f.From.AirportCode == flight.From.Airport &&
                                                                           f.From.City == flight.From.City &&
                                                                           f.From.Country == flight.From.Country &&
                                                                           f.ArrivalTime == flight.ArrivalTime &&
                                                                           f.DepartureTime == flight.DepartureTime &&
                                                                           f.Carrier == flight.Carrier);

                _flightService.Delete(userFlight);
            }

            return Ok();
        }
    }
}