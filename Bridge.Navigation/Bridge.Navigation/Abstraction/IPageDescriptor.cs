using System;
using System.Collections.Generic;

namespace Bridge.Navigation
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
        Func<string> HtmlLocation { get; set; }

        /// <summary>
        /// Page Controller
        /// </summary>
        Func<IAmLoadable> PageController { get; set; }

        /// <summary>
        /// Add Page JS dependencies.
        /// </summary>
        IEnumerable<string> JsDependencies { get; set; }

        /// <summary>
        /// If null can be direct loaded
        /// else evaluate func
        /// </summary>
        /// <returns></returns>
        Func<bool> CanBeDirectLoad { get; set; }

        /// <summary>
        /// Action for prepare page
        /// </summary>
        Action PreparePage { get; set; }
    }
}