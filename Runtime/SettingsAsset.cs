using UnityEngine;

namespace NetCellLeaderboards.Runtime
{
    public class SettingsAsset : ScriptableObject
    {
        [field: SerializeField] public string ServerUrl { get; set; } = "http://0.0.0.0:3000";
        [field: SerializeField]
        [field: Tooltip("Включите если нужно создать билд поддерживающий ODR")] 
        public bool OnDemandResources { get; set; }
        
        private static SettingsAsset CachedSettings;

        private void OnValidate()
        {
            ServerUrl = ServerUrl.TrimEnd('/');
        }

        public static SettingsAsset CacheFromResources()
        {
            if (CachedSettings == null)
            {
                CachedSettings = Resources.Load<SettingsAsset>("NetCellLeaderboardsSettings");
            }

            return CachedSettings;
        }
    }
}