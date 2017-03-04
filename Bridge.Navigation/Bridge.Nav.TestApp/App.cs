using Bridge.Navigation.Abstraction;
using Bridge.Navigation.Impl;

namespace Bridge.Nav.TestApp
{
    public class App
    {
        public static INavigator Nav;
        public static void Main()
        {
            var navConfig = new TestNavConfig();
            Nav = new BridgeNavigator(navConfig);

            Nav.InitNavigation();
        }
    }
}