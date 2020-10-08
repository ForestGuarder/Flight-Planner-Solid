using Flight_Planner.Data;
using Fligth_Planner.Core.Models;
using Fligth_Planner.Core.Services;

namespace Flight_Planner.Services
{
    public class ClearDbService<T> : EntityService<T>, IClearDbService<T> where T : Entity
    {
        public ClearDbService(IFlightPlannerDbContext context) : base(context)
        {
        }

        public ServiceResult ClearDb()
        {
            _ctx.Set<T>().RemoveRange(_ctx.Set<T>());
            _ctx.SaveChanges();

            return new ServiceResult(true);
        }
    }
}
