using System;
using System.Collections.Generic;

namespace Bridge.Navigation.Abstraction
{
    public interface IPageDescriptor
    {
        /// <summary>
        /// Page Key
        /// </summary>
        string Key { get; set; }

        /// <summary>
        /// Html page location
        /// </summary>
        string HtmlLocation { get; set; }

        /// <summary>
        /// Page Controller
        /// </summary>
        Func<IAmLoadable> PageController { get; set; }

        /// <summary>
        /// Add Page JS dependencies.
        /// </summary>
        IEnumerable<string> JsDependencies { get; set; }
    }
}