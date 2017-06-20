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

        protected readonly INavigatorConfigurator Configuration;
        public BridgeNavigator(INavigatorConfigurator configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Navigate to a page ID.
        /// The ID must be registered.
        /// </summary>
        /// <param name="pageId"></param>
        public virtual void Navigate(string pageId, Dictionary<string,object> parameters = null)
        {
            var page = this.Configuration.GetPageDescriptorByKey(pageId);
            if (page == null) throw new Exception($"Page not found with ID {pageId}");

            var body = this.Configuration.Body;

            if(body == null)
                throw new Exception("Cannot find navigation body element.");

            this.Configuration.Body.Load(page.HtmlLocation.Invoke(),null, (o,s,a) =>
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
        public virtual void InitNavigation()
        {
            this.SubscribeAnchors();

            // go home
            this.Navigate(this.Configuration.HomeId);
        }

        /// <summary>
        /// Subscribe all anchors of spaf
        /// </summary>
        protected void SubscribeAnchors()
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
        }
    }
}