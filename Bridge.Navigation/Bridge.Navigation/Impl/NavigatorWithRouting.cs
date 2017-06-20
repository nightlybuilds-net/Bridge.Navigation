using System;
using System.Collections.Generic;
using Bridge.Html5;
using Bridge.Navigation.Abstraction;

namespace Bridge.Navigation.Impl
{
    [Reflectable]
    public class NavigatorWithRouting : BridgeNavigator
    {

        public NavigatorWithRouting(INavigatorConfigurator configuration) : base(configuration)
        {
            Window.OnPopState += e =>
            {
                var urlInfo = this.ParseUrl();
                this.NavigateWithoutPushState(string.IsNullOrEmpty(urlInfo.PageId) ? configuration.HomeId : urlInfo.PageId, urlInfo.Parameters);
            };
        }

        private void NavigateWithoutPushState(string pageId, Dictionary<string, object> parameters = null)
        {
            base.Navigate(pageId, parameters);
        }
        public override void Navigate(string pageId, Dictionary<string, object> parameters = null)
        {
            base.Navigate(pageId, parameters);
            Window.History.PushState(null, string.Empty,
                parameters != null
                    ? $"http://{Window.Location.Host}#{pageId}={Global.Btoa(JSON.Stringify(parameters))}"
                    : $"http://{Window.Location.Host}#{pageId}");
        }

        public override void InitNavigation()
        {
            var parsed = this.ParseUrl();

            if (string.IsNullOrEmpty(parsed.PageId))
                base.InitNavigation();
            else
            {
                base.SubscribeAnchors();

                var page = this.Configuration.GetPageDescriptorByKey(parsed.PageId);
                if (page == null) throw new Exception($"Page not found with ID {parsed.PageId}");

                // if not null and evaluation is false fallback to home
                if (page.CanBeDirectLoad != null && !page.CanBeDirectLoad.Invoke())
                {
                    Window.History.ReplaceState(null,string.Empty, $"http://{Window.Location.Host}#{this.Configuration.HomeId}");
                    this.NavigateWithoutPushState(this.Configuration.HomeId);
                }
                else
                    this.Navigate(parsed.PageId,parsed.Parameters);
            }
        }

        private UrlDescriptor ParseUrl()
        {
            var res = new UrlDescriptor();

            var hash = Window.Location.Hash;
            hash = hash.Replace("#", "");

            if (string.IsNullOrEmpty(hash)) return res;

            var equalIndex = hash.IndexOf('=');
            if (equalIndex == -1)
            {
                res.PageId = hash;
                return res;
            }

            res.PageId = hash.Substring(0, equalIndex);  

            var doublePointsIndx = equalIndex + 1;
            var parameters = hash.Substring(doublePointsIndx, hash.Length - doublePointsIndx);

            if (string.IsNullOrEmpty(parameters)) return res; // no parameters

            var decoded = Global.Atob(parameters);
            var deserialized = JSON.Parse<Dictionary<string, object>>(decoded);

            res.Parameters = deserialized;
            
            return res;
        }

     
        class UrlDescriptor
        {
            public string PageId { get; set; }

            public Dictionary<string, object> Parameters { get; set; }
        }
    }
}