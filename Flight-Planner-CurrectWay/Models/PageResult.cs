using System.Collections.Generic;

namespace Flight_Planner_CurrectWay.Models
{
    public class PageResult
    {
        public int Page;
        public int TotalItems;
        public List<FlightResponse> Items;

        public PageResult(int page, int totalItems, List<FlightResponse> items)
        {
            Page = page;
            TotalItems = totalItems;
            Items = items;
        }
    }
}