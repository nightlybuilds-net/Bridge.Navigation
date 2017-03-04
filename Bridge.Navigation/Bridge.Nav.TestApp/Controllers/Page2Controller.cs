using System;
using Bridge.Html5;
using Bridge.Navigation.Abstraction;

namespace Bridge.Nav.TestApp.Controllers
{
    public class Page2Controller : IAmLoadable
    {
        public void OnLoad()
        {
            Global.Alert("Hello from Page 2 Controller");
        }
    }
}