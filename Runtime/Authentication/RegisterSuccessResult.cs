using System;
using UnityEngine;

namespace NetCellLeaderboards.Runtime
{
    public class RegisterSuccessResult
    {
        public string Username;
        public string DisplayName;
        
        public static RegisterSuccessResult DeserializeFromBody(string body)
        {
            var anonymous = new RegisterSuccessResultJson();
            JsonUtility.FromJsonOverwrite(body, anonymous);
            return new RegisterSuccessResult
            {
                Username = anonymous.username,
                DisplayName = anonymous.displayName
            };
        }

        [Serializable]
        private class RegisterSuccessResultJson
        {
            public string username;
            public string displayName;
        }
    }
}