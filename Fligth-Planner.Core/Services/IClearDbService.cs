using Fligth_Planner.Core.Models;

namespace Fligth_Planner.Core.Services
{
    public interface IClearDbService<T> : IEntityService<T> where T : Entity
    {
        ServiceResult ClearDb();
    }
}
