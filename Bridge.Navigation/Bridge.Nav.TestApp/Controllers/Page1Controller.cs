using System.Collections.Generic;
using Bridge.jQuery2;
using Bridge.Navigation.Abstraction;
using Bridge.Utils;

namespace Bridge.Nav.TestApp.Controllers
{
    public class Page1Controller : IAmLoadable
    {
        public Page1Controller(INavigator nav)
        {
            var button = jQuery.Select("#myButton");
            button.Click(() =>
            {
                nav.Navigate("page2");
            });
        }

        public void OnLoad(Dictionary<string, object> parameters)
        {
            Console.Log("OnLoad() on Page 1 controller");
        }

        public void OnLeave()
        {
            Console.Log("Goodbye page1");
        }
    }
}
