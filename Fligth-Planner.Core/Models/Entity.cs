using Fligth_Planner.Core.Interfaces;

namespace Fligth_Planner.Core.Models
{
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
    }
}
