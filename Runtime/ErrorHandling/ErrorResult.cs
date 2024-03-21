using System;
using UnityEngine;
using UnityEngine.Networking;

namespace NetCellLeaderboards.Runtime
{
    public class ErrorResult
    {
        public string Name;
        public string Message;

        public static ErrorResult DeserializeFromBody(UnityWebRequest request, string body)
        {
            var errorResultJson = new ErrorResultJsonInner();
            JsonUtility.FromJsonOverwrite(body, errorResultJson);
            if (errorResultJson.error == null || errorResultJson.error.name == null ||
                errorResultJson.error.message == null)
            {
                errorResultJson.error = new ErrorResultJson();
                errorResultJson.error.name = request.result.ToString();
                errorResultJson.error.message = request.error;
            }
            Debug.Log($"Got network error: {errorResultJson.error.name}, {errorResultJson.error.message}");
            return new ErrorResult
            {
                Name = errorResultJson.error.name,
                Message = errorResultJson.error.message
            };
        }

        [Serializable]
        public class ErrorResultJson
        {
            public string name;
            public string message;
        }

        [Serializable]
        public class ErrorResultJsonInner
        {
            public ErrorResultJson error;
        }
    }
}