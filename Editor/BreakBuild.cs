using System;
using System.IO;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace NetCellLeaderboards.Editor
{
    public class BreakBuild : IPreprocessBuildWithReport
    {
        public int callbackOrder => 0;
        
        public void OnPreprocessBuild(BuildReport report)
        {
            if (!File.Exists("Assets/Resources/NetCellLeaderboardsSettings.asset"))
                throw new InvalidOperationException(
                    "NetCellLeaderboards settings file not created! Consider adding by clicking \"NetCellLeaderboards/Create settings\" button in menu above");
        }
    }
}