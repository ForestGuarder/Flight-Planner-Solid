using Flight_Planner.Data;
using Fligth_Planner.Core.Models;
using Fligth_Planner.Core.Services;

namespace Flight_Planner.Services
{
    public class AirportService : EntityService<Airport>, IAirportService
    {
        public AirportService(IFlightPlannerDbContext context) : base(context)
        {
        }
    }
}
