using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Flight_Planner_CurrectWay.Models;
using Fligth_Planner.Core.Models;
using Fligth_Planner.Core.Services;

namespace Flight_Planner_CurrectWay.Controllers
{
    public class ClientApiController : BasicController
    {
        public ClientApiController(IFlightService flightService, IAirportService airportService, IMapper mapper)
            : base(flightService, airportService, mapper)
        {
        }

        private readonly Validations _valid = new Validations();

        [HttpGet, Route("api/airports")]
        public IHttpActionResult SearchAirports(string search)
        {
            HashSet<AirportResponse> contains = new HashSet<AirportResponse>();

            List<Airport> searchAirports = _airportService.Get().Where(f => f.AirportCode.Trim().ToUpper().Contains(search.Trim().ToUpper()) ||
                                                                      f.City.Trim().ToUpper().Contains(search.Trim().ToUpper()) ||
                                                                      f.Country.Trim().ToUpper().Contains(search.Trim().ToUpper())).ToList();

            foreach (Airport airport in searchAirports)
            {
                contains.Add(_mapper.Map(airport, new AirportResponse()));
            }

            return Ok(contains);
        }

        [HttpPost, Route("api/flights/search")]
        public IHttpActionResult SearchFlights(SearchByFlight flySearch)
        {
            List<Flight> items = new List<Flight>();
            List<FlightResponse> foundItems = new List<FlightResponse>();
            int totalItems;
            int page;

            if (flySearch == null)
            {
                return BadRequest();
            }

            if (_valid.FlightSearchCheck(flySearch.To, flySearch.From, flySearch.DepartureDate))
            {
                return BadRequest();
            }

            items = _flightService.Query().Include(f => f.To).Include(f => f.From).Where(f =>
                f.From.AirportCode.ToLower().Trim().Contains(flySearch.From.ToLower().Trim()) ||
                f.To.AirportCode.ToLower().Trim().Contains(flySearch.To.ToLower().Trim()) ||
                f.DepartureTime.ToLower().Trim()
                    .Contains(flySearch.DepartureDate.ToLower().Trim())).ToList();

            totalItems = items.Count();

            foreach (Flight fly in items)
            {
                foundItems.Add(_mapper.Map(fly, new FlightResponse()));
            }

            page = items.Count == 0 ? 0 : 1;

            PageResult pageResult = new PageResult(page, totalItems, foundItems);

            return Ok(pageResult);
        }

        [HttpGet, Route("api/flights/{id}")]
        public async Task<IHttpActionResult> FindFlightById(int id)
        {
            Flight flight = await _flightService.QueryById(id).Include(f => f.To).Include(f => f.From).SingleOrDefaultAsync();

            if (flight == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(_mapper.Map(flight, new FlightResponse()));
            }
        }
    }
}