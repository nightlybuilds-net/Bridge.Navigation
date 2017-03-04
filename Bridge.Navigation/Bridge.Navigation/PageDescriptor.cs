using System;
using Bridge.Navigation.Abstraction;

namespace Bridge.Navigation
{
    [Reflectable]
    public class PageDescriptor : IPageDescriptor
    {
        
        public string Key { get; set; }
        public string HtmlLocation { get; set; }
        public string JsLocation { get; set; }
        public Func<IAmLoadable> PageController { get; set; }
        // todo script?
    }
}