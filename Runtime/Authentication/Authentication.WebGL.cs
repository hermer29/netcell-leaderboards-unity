#if UNITY_WEBGL && !UNITY_EDITOR
using System.Runtime.InteropServices;
#endif

namespace NetCellLeaderboards.Runtime
{
    public static partial class Authentication
    {
        private static bool IsFetchOverriden = false;

        private static void OverrideFetch()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            if (IsFetchOverriden)
                return;
            IsFetchOverriden = true;
            var settings = SettingsAsset.CacheFromResources();
            OverrideFetch(settings.ServerUrl);
#endif
        }
        
        
#if UNITY_WEBGL && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void OverrideFetch(string serverUrl);
#endif
    }
}