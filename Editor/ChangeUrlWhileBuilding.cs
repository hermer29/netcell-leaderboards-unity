using NetCellLeaderboards.Runtime;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace NetCellLeaderboards.Editor
{
    public class ChangeUrlWhileBuilding : IPreprocessBuildWithReport, IPostprocessBuildWithReport
    {
        private const string VkCors = "vkcors";
        private const string Https = "https";
        public int callbackOrder { get; }

        private SettingsAsset _asset;
        
        public void OnPreprocessBuild(BuildReport report)
        {
            _asset ??= SettingsAsset.CacheFromResources();
            if (_asset.OnDemandResources) 
                _asset.ServerUrl = _asset.ServerUrl.Replace(Https, VkCors);
        }

        public void OnPostprocessBuild(BuildReport report)
        {
            _asset ??= SettingsAsset.CacheFromResources();
            if (_asset.OnDemandResources) 
                _asset.ServerUrl = _asset.ServerUrl.Replace(VkCors, Https);
        }
    }
}