using System.Collections.Generic;

namespace Bridge.Navigation.Abstraction
{
    public interface INavigatorConfigurator
    {
        jQuery Body { get; }
        string HomeId { get; }

        IList<IPageDescriptor> CreateRoutes(); 
        IPageDescriptor GetPageDescriptorByKey(string key);
    }
}