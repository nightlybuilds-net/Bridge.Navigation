using System;
using System.Collections.Generic;
using Bridge.Html5;

namespace Bridge.Navigation
{
    public static class NavigationUtility
    {
        /// <summary>
        /// Define virtual directory for something like:
        /// protocol://awesomesite.io/somedirectory
        /// </summary>
        public static string VirtualDirectory = null;

        
        /// <summary>
        /// Push state on history
        /// </summary>
        /// <param name="pageId"></param>
        /// <param name="parameters"></param>
        public static void PushState(string pageId, Dictionary<string, object> parameters = null)
        {
            var baseUrl = BuildBaseUrl(pageId);

            Window.History.PushState(null, string.Empty,
                parameters != null
                    ? $"{baseUrl}={Global.Btoa(JSON.Stringify(parameters))}" : baseUrl);
        }

        /// <summary>
        /// replace state on history
        /// </summary>
        /// <param name="pageId"></param>
        /// <param name="parameters"></param>
        public static void ReplaceState(string pageId, Dictionary<string, object> parameters = null)
        {
            PushState(pageId,parameters);
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
        
        /// <summary>
        /// Build base url using page id and virtual directory
        /// </summary>
        /// <param name="pageId"></param>
        /// <returns></returns>
        private static string BuildBaseUrl(string pageId)
        {
            var baseUrl = $"{Window.Location.Protocol}//{Window.Location.Host}";
            baseUrl = string.IsNullOrEmpty(VirtualDirectory)
                ? $"{baseUrl}#{pageId}"
                : $"{baseUrl}/{VirtualDirectory}#{pageId}";
            return baseUrl;
        }
    }
    
   
}
