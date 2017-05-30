﻿using System;
using System.Collections.Generic;
using Bridge.Navigation.Abstraction;

namespace Bridge.Navigation.Impl
{
    [Reflectable]
    public class PageDescriptor : IPageDescriptor
    {
        public string Key { get; set; }
        public string HtmlLocation { get; set; }
        public Func<IAmLoadable> PageController { get; set; }
        public IEnumerable<string> JsDependencies { get; set; }
    }
}