using System.IO;
using NetCellLeaderboards.Runtime;
using UnityEditor;
using UnityEngine;

namespace NetCellLeaderboards.Editor
{
    public static class EditorControls
    {
        private const string AssetCreationPath = "Assets/Resources/NetCellLeaderboardsSettings.asset";
        
        [MenuItem("NetCellLeaderboards/Create settings")]
        public static void CreateSettings()
        {
            var resourcesPath = "Assets/Resources";
            if (!Directory.Exists(resourcesPath))
            {
                Directory.CreateDirectory(resourcesPath);
            }
            AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<SettingsAsset>(), 
            AssetCreationPath);
        }
    }
}