using System;
using System.Collections.Generic;
using Bridge.Html5;
using Bridge.Navigation.Abstraction;

namespace Bridge.Nav.TestApp.Controllers
{
    public class Page2Controller : IAmLoadable
    {
        public void OnLoad(Dictionary<string, object> parameters)
        {
            Global.Alert("Hello from Page 2 Controller");
        }
    }
}