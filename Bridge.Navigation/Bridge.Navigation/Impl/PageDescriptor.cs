﻿using System;
using System.Collections.Generic;

namespace Bridge.Navigation
{
    public class PageDescriptor : IPageDescriptor
    {
        public PageDescriptor()
        {
            this.AutoEnableSpafAnchors = () => true;
        }

        public string Key { get; set; }
        public Func<string> HtmlLocation { get; set; }
        public Func<IAmLoadable> PageController { get; set; }
        public Func<bool> CanBeDirectLoad { get; set; }
        public Action PreparePage { get; set; }
        public Func<string> RedirectRules { get; set; }
        public Func<bool> AutoEnableSpafAnchors { get; set; }
    }
}