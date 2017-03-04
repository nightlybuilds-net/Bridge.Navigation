using System;
using System.Collections.Generic;
using System.Linq;
using Bridge.Navigation.Abstraction;

namespace Bridge.Navigation
{
    [Reflectable]
    public abstract class NavigatorConfigBase : INavigatorConfigurator
    {
        private readonly IList<IPageDescriptor> _routes;

        public abstract IList<IPageDescriptor> CreateRoutes();
        public abstract jQuery Body { get; }
        public abstract string HomeId { get; }


        protected NavigatorConfigBase()
        {
            this._routes = this.CreateRoutes();
        }

        public IPageDescriptor GetPageDescriptorByKey(string key)
        {
            return this._routes.SingleOrDefault(s=> string.Equals(s.Key, key, StringComparison.CurrentCultureIgnoreCase));
        }
      
    }
}