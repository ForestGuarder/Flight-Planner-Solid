using System;
using System.Web.Http.Dependencies;
using StructureMap;

namespace Flight_Planner_CurrectWay.DependencyResolution
{
    public class StructureMapDependencyResolver : StructureMapScope, IDependencyResolver
    {
        public StructureMapDependencyResolver(IContainer container) : base(container)
        {
            _container = container ??
                         throw new ArgumentException(nameof(container));
        }
        private readonly IContainer _container;

        public IDependencyScope BeginScope()
        {
            var childContainer = _container.GetNestedContainer();
            return new StructureMapScope(childContainer);
        }
    }
}