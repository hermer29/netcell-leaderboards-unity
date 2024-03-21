using System.Collections.Generic;
using UnityEngine.Networking;

namespace NetCellLeaderboards.Runtime
{
    internal static class Credentials
    {
        internal static List<string> CookieHeaders = new();

        internal static void AddCookieToRequest(this UnityWebRequest request)
        {
            foreach (string cookieHeader in CookieHeaders)
            {
                request.SetRequestHeader("Set-Cookie",cookieHeader);
            }
        }

        internal static void FetchCookieFromResponse(this UnityWebRequest request)
        {
            foreach ((string key, string value) in request.GetResponseHeaders())
            {
                if (key == "Set-Cookie")
                {
                    CookieHeaders.Add(value);
                }
            }
        }
    }
}