using System.Web.WebPages;
using Microsoft.Ajax.Utilities;

namespace Flight_Planner_CurrectWay.Models
{
    public class Validations
    {
        public bool FlightSearchCheck(string To, string From, string DepartureTime)
        {
            if (From != null &&
                To != null &&
                DepartureTime != null &&
                To != From)
            {
                return false;
            }

            return true;
        }

        public bool FlightCheck(FlightRequest flight)
        {
            {
                if (flight == null ||
                    flight.From == null || flight.To == null ||
                    flight.From.City.IsNullOrWhiteSpace() ||
                    flight.From.Country.IsNullOrWhiteSpace() ||
                    flight.From.Airport.IsNullOrWhiteSpace() ||
                    flight.To.City.IsNullOrWhiteSpace() ||
                    flight.To.Country.IsNullOrWhiteSpace() ||
                    flight.To.Airport.IsNullOrWhiteSpace() ||
                    flight.From.Airport.ToUpper().Trim() == flight.To.Airport.ToUpper().Trim() ||
                    flight.Carrier.IsNullOrWhiteSpace() ||
                    flight.DepartureTime.IsNullOrWhiteSpace() ||
                    flight.ArrivalTime.IsNullOrWhiteSpace() ||
                    flight.ArrivalTime.AsDateTime() <= flight.DepartureTime.AsDateTime())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}