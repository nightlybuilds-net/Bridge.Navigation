using System.Collections.Generic;
using Bridge.Html5;

namespace Bridge.Navigation
{
    public static class NavigationUtility
    {
        /// <summary>
        /// Push state on history
        /// </summary>
        /// <param name="pageId"></param>
        /// <param name="parameters"></param>
        public static void PushState(string pageId, Dictionary<string, object> parameters = null)
        {
            Window.History.PushState(null, string.Empty,
                parameters != null
                    ? $"{Window.Location.Protocol}//{Window.Location.Host}#{pageId}={Global.Btoa(JSON.Stringify(parameters))}"
                    : $"{Window.Location.Protocol}//{Window.Location.Host}#{pageId}");
        }

        /// <summary>
        /// replace state on history
        /// </summary>
        /// <param name="pageId"></param>
        /// <param name="parameters"></param>
        public static void ReplaceState(string pageId, Dictionary<string, object> parameters = null)
        {
            Window.History.PushState(null, string.Empty,
                parameters != null
                    ? $"{Window.Location.Protocol}//{Window.Location.Host}#{pageId}={Global.Btoa(JSON.Stringify(parameters))}"
                    : $"{Window.Location.Protocol}//{Window.Location.Host}#{pageId}");
        }
    }
}
