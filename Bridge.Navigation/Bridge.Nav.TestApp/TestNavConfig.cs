using System.Collections.Generic;
using Bridge.jQuery2;
using Bridge.Nav.TestApp.Controllers;
using Bridge.Navigation.Abstraction;
using Bridge.Navigation.Impl;

namespace Bridge.Nav.TestApp
{
    class TestNavConfig : BridgeNavigatorConfigBase
    {
        public override string HomeId => "home";

        public override IList<IPageDescriptor> CreateRoutes()
        {
            return new List<IPageDescriptor>
            {
                new PageDescriptor
                {
                    Key = "home",
                    HtmlLocation = ()=>"home.html"
                },
                new PageDescriptor
                {
                    Key = "page1",
                    HtmlLocation = ()=>"page1.html",
                    PageController = ()=> new Page1Controller(App.Nav)
                },
                new PageDescriptor
                {
                    Key = "page2",
                    HtmlLocation = ()=>"page2.html",
                    PageController = ()=> new Page2Controller()

                },

            };
        }

        public override jQuery Body => jQuery.Select("#myContent");
    }
}
