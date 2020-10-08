using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Flight_Planner_CurrectWay.Models;
using FlightPlaner.Attributes;
using Fligth_Planner.Core.Models;
using Fligth_Planner.Core.Services;

namespace Flight_Planner_CurrectWay.Controllers
{
    [BasicAuthentication]
    public class AdminController : BasicController
    {
        public AdminController(IFlightService flightService, IAirportService airportService, IMapper mapper)
            : base(flightService, airportService, mapper)
        {
        }

        private static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);

        private readonly Validations _valid = new Validations();

        [HttpGet, Route("admin-api/flights/{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            Flight flight = await _flightService.GetById(id);

            if (flight == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map(flight, new FlightResponse()));
        }

        [HttpPut, Route("admin-api/flights")]
        public async Task<IHttpActionResult> AddFlights(FlightRequest flight)
        {
            await semaphoreSlim.WaitAsync();
            try
            {
                if (!_valid.FlightCheck(flight))
                {
                    if (_flightService.Query().Any(f =>
                        f.To.AirportCode == flight.To.Airport && f.From.AirportCode == flight.From.Airport &&
                        f.DepartureTime == flight.DepartureTime && f.ArrivalTime == flight.ArrivalTime &&
                        f.Carrier == flight.Carrier))
                    {
                        return Conflict();
                    }
                    else
                    {
                        Flight userFlight = _mapper.Map(flight, new Flight());

                        _airportService.Create(userFlight.From);
                        _airportService.Create(userFlight.To);

                        _flightService.Create(userFlight);

                        return Created("", _mapper.Map(userFlight, new FlightResponse()));
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }

        [HttpDelete, Route("admin-api/flights/{id}")]
        public async Task<IHttpActionResult> DeleteFlight(int id)
        {
            Flight flight = await _flightService.GetById(id);

            if (flight == null)
            {
                return Ok();
            }

            _flightService.Delete(flight);

            return Ok();
        }
    }
}
