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

            Database.SetInitializer<FlightPlannerDbContext>(new MigrateDatabaseToLatestVersion<FlightPlannerDbContext,Configuration>());
        }

        public DbSet<Flight> Flights { get; set; }
        public DbSet<Airport> Airports { get; set; }
    }
}
