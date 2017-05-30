using System;
using System.Collections.Generic;
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
        private static IAmLoadable _actualController;

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
        public void Navigate(string pageId, Dictionary<string,object> parameters = null)
        {
            var page = this._configuration.GetPageDescriptorByKey(pageId);
            if (page == null) throw new Exception($"Page not found with ID {pageId}");

            var body = this._configuration.Body;

            if(body == null)
                throw new Exception("Cannot find navigation body element.");

            this._configuration.Body.Load(page.HtmlLocation,null, (o,s,a) =>
            {
                // inject dependencies
                if (page.JsDependencies != null)
                    page.JsDependencies.ForEach(f =>
                    {
                        jQuery.GetScript(f);
                    });

                if (page.PageController != null)
                {
                    // leave actual controlelr
                    if (this.LastNavigateController != null)
                        this.LastNavigateController.OnLeave();

                    // load new controller
                    var controller = page.PageController();
                    controller.OnLoad(parameters);

                    _actualController = controller;
                }
                
            }); 
        }

        public IAmLoadable LastNavigateController => _actualController;

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
                var isMyHref = href.StartsWith("spaf:");

                // if is my href
                if (isMyHref)
                {
                    ev.PreventDefault();
                    var pageId = href.Replace("spaf:", "");
                    this.Navigate(pageId);
                }
                // anchor default behaviour
            });

            // go home
            this.Navigate(this._configuration.HomeId);
        }
    }
}