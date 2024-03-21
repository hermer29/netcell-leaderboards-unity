using System;
using UnityEngine;

namespace NetCellLeaderboards.Runtime
{
    public class LoginSuccessResult
    {
        public string Username;
        public string DisplayName;
        
        public static LoginSuccessResult DeserializeFromBody(string body)
        {
            var anonymous = new LoginSuccessResultJson();
            JsonUtility.FromJsonOverwrite(body, anonymous);
            return new LoginSuccessResult
            {
                Username = anonymous.username,
                DisplayName = anonymous.displayName
            };
        }

        [Serializable]
        private class LoginSuccessResultJson
        {
            public string username;
            public string displayName;
        }
    }
}