using System;
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

        /// <summary>
        /// Get parameter key from parameters dictionary
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters"></param>
        /// <param name="paramKey"></param>
        /// <returns></returns>
        public static T GetParameter<T>(this Dictionary<string, object> parameters, string paramKey)
        {
            if (parameters == null)
                throw new Exception("Parameters is null!");

            if (!parameters.ContainsKey(paramKey))
                throw new Exception($"No parameter with key {paramKey} found!");

            var value = parameters[paramKey];
            return (T)value;
        }
    }
}
