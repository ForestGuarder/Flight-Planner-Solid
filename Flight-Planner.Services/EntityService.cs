using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flight_Planner.Data;
using Fligth_Planner.Core.Models;
using Fligth_Planner.Core.Services;

namespace Flight_Planner.Services
{
    public class EntityService<T> : DbService, IEntityService<T> where T: Entity
    {
        public EntityService(IFlightPlannerDbContext context) : base(context)
        {
        }

        public IQueryable<T> Query()
        {
            return Query<T>();
        }

        public IQueryable<T> QueryById(int id)
        {
            return QueryById<T>(id);
        }

        public IEnumerable<T> Get()
        {
            return Get<T>();
        }

        public async Task<T> GetById(int id)
        {
            return await GetById<T>(id);
        }

        public ServiceResult Create(T entity)
        {
            return Create<T>(entity);
        }

        public ServiceResult Delete(T entity)
        {
            return Delete<T>(entity);
        }

        public ServiceResult Update(T entity)
        {
            return Update<T>(entity);
        }

        public bool Exists(int id)
        {
            return Exists<T>(id);
        }
    }
}
