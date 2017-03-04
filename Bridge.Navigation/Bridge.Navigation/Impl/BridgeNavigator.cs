using System;
using Bridge.jQuery2;
using Bridge.Navigation.Abstraction;
using Bridge.Linq;

namespace Bridge.Navigation.Impl
{
    /// <summary>
    /// INavigator implementation
    /// </summary>
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
                // todo manage error

                if (page.PageController != null) 
                    page.PageController().OnLoad();

                if (page.JsDependencies != null)
                    page.JsDependencies.ForEach(f=> 
                    {
                        jQuery.GetScript(f);
                    });
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