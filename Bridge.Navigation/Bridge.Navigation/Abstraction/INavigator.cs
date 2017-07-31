using System.Collections.Generic;

namespace Bridge.Navigation
{
    public interface INavigator
    {
        IAmLoadable LastNavigateController { get; }

        /// <summary>
        /// Init the navigation. THis will subscribe to all anchors click
        /// HRef anchor is @pageid
        /// </summary>
        void InitNavigation();

        /// <summary>
        /// Navigate to a pageid
        /// </summary>
        /// <param name="pageId"></param>
        /// <param name="parameters"></param>
        void Navigate(string pageId, Dictionary<string, object> parameters = null);
    }
}