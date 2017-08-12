using System;
using System.Collections.Generic;

namespace Bridge.Navigation
{
    [Reflectable]
    public class PageDescriptor : IPageDescriptor
    {
        public string Key { get; set; }
        public Func<string> HtmlLocation { get; set; }
        public Func<IAmLoadable> PageController { get; set; }
        public IEnumerable<string> JsDependencies { get; set; }

        public Func<bool> CanBeDirectLoad { get; set; }
        public Action PreparePage { get; set; }
    }
}