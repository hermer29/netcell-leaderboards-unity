using System;
using NetCellLeaderboards.Runtime.Requests;
using UnityEngine;
using UnityEngine.Networking;

namespace NetCellLeaderboards.Runtime
{
    public static class Leaderboards
    {
        public static void WriteScore(string leaderboardsName, int score, 
            Action onSuccess, Action<ErrorResult> onError)
        {
            var settings = SettingsAsset.CacheFromResources();
            var request = UnityWebRequest.Post(
                settings.ServerUrl + $"/leaderboards/{leaderboardsName}", 
                "");
            var json = JsonUtility.ToJson(new WriteScoreJson
            {
                score = score
            });
            request.AttachJsonBody(json);
            request.AddCookieToRequest();
            var asyncOperationRequest = request.SendWebRequest();
            asyncOperationRequest.completed += operation =>
            {
                string body = asyncOperationRequest.webRequest.downloadHandler.text;
                if (asyncOperationRequest.webRequest.responseCode != 200)
                {
                    onError.Invoke(ErrorResult.DeserializeFromBody(request, body));
                    asyncOperationRequest.webRequest.Dispose();
                    return;
                }
                onSuccess.Invoke();
                asyncOperationRequest.webRequest.Dispose();
            };
        }

        [Serializable]
        private class WriteScoreJson
        {
            public int score;
        }

        public static void GetBoard(string leaderboardsName, 
            Action<GetBoardResult> onSuccess, Action<ErrorResult> onError, out Func<float> progressProvider)
        {
            var settings = SettingsAsset.CacheFromResources();
            var request = UnityWebRequest.Get(settings.ServerUrl + $"/leaderboards/{leaderboardsName}");
            request.AddCookieToRequest();
            var asyncOperationRequest = request.SendWebRequest();
            progressProvider = () => asyncOperationRequest.progress;
            asyncOperationRequest.completed += operation =>
            {
                var body = asyncOperationRequest.webRequest.downloadHandler.text;
                if (asyncOperationRequest.webRequest.responseCode != 200)
                {
                    onError.Invoke(ErrorResult.DeserializeFromBody(request, body));
                    asyncOperationRequest.webRequest.Dispose();
                    return;
                }
                onSuccess.Invoke(new DeserializedLeaderboardsData(body).Value);
                asyncOperationRequest.webRequest.Dispose();
            };
        }
    }

    [Serializable]
    public class GetBoardResult
    {
        public GetBoardRecord[] entries;
    }
    
    [Serializable]
    public class GetBoardRecord
    {
        public string username;
        public string displayName;
        public int score;
    }
}