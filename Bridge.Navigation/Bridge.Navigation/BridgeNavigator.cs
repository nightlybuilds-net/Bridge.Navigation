using System;
using Bridge.Navigation.Abstraction;

namespace Bridge.Navigation
{
    [Reflectable]
    public class BridgeNavigator : INavigator 
    {
        private readonly INavigatorConfigurator _configuration;
        public BridgeNavigator(INavigatorConfigurator configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Navigate to a page ID.
        /// The ID must be registered.
        /// </summary>
        /// <param name="pageId"></param>
        public void Navigate(string pageId)
        {
            var page = this._configuration.GetPageDescriptorByKey(pageId);
            if (page == null) throw new Exception($"Page not found with ID {pageId}");

            var body = this._configuration.Body;

            if(body == null)
                throw new Exception("Cannot find navigation body element.");

            this._configuration.Body.Load(page.HtmlLocation,null, (o,s,a) =>
            {
                if (page.PageController == null) return;
                page.PageController().OnLoad();
            }); 
        }

        /// <summary>
        /// Subscribe to anchors click
        /// </summary>
        public void InitNavigation()
        {
            var allAnchors = jQuery.Select("a");
            allAnchors.Click(ev =>
            {
                var anchor = ev.Target;
                var href = anchor.GetAttribute("href");
                var isMyHref = href.StartsWith("@");

                // if is my href
                if (isMyHref)
                {
                    ev.PreventDefault();
                    var pageId = href.TrimStart('@');
                    this.Navigate(pageId);
                }
                // anchor default behaviour
            });

            // go home
            this.Navigate(this._configuration.HomeId);
        }
    }
}