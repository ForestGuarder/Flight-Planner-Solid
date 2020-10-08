using System.Data.Entity;
using Flight_Planner.Data.Migrations;
using Fligth_Planner.Core.Models;

namespace Flight_Planner.Data
{
    public class FlightPlannerDbContext : DbContext, IFlightPlannerDbContext
    {
        public FlightPlannerDbContext() : base("Flight-Planer")
        {
            Database.SetInitializer<FlightPlannerDbContext>(null);

            //Gold line vvvvv
            Database.SetInitializer<FlightPlannerDbContext>(new MigrateDatabaseToLatestVersion<FlightPlannerDbContext,Configuration>());
            //Gold Line ^^^^^
        }

        public DbSet<Flight> Flights { get; set; }
        public DbSet<Airport> Airports { get; set; }
    }
}
