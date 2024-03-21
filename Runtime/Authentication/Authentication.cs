using System;
using NetCellLeaderboards.Runtime.Requests;
using UnityEngine;
using UnityEngine.Networking;

namespace NetCellLeaderboards.Runtime
{
    public static partial class Authentication
    {
        private const string LoginPath = "/auth/login/";
        private const string RegisterPath = "/auth/register/";
        
        public static void LoginUser(string username, string password, 
            LoginSuccessCallback onSuccess, LoginFailedCallback onError, out Func<float> progressGetter)
        {
            ThrowIf.StringParameterIsEmpty(nameof(username), username);
            ThrowIf.StringParameterIsEmpty(nameof(password), password);
            OverrideFetch();
            var settings = SettingsAsset.CacheFromResources();
            var body = JsonUtility.ToJson(new LoginRequestJson
            {
                password = password,
                username = username
            });
            var request = UnityWebRequest.Post(settings.ServerUrl + LoginPath, "");
            request.AttachJsonBody(body);
            var asyncOperationRequest = request.SendWebRequestVerbose();
            progressGetter = () => asyncOperationRequest.progress;
            asyncOperationRequest.completed += operation =>
            {
                var body = asyncOperationRequest.webRequest.downloadHandler.text;
                if (asyncOperationRequest.webRequest.responseCode != 200)
                {
                    onError.Invoke(ErrorResult.DeserializeFromBody(request, body));
                    asyncOperationRequest.webRequest.Dispose();
                    return;
                }
                onSuccess.Invoke(LoginSuccessResult.DeserializeFromBody(body));
                asyncOperationRequest.webRequest.FetchCookieFromResponse();
                asyncOperationRequest.webRequest.Dispose();
            };
        }

        [Serializable]
        private class LoginRequestJson
        {
            public string username;
            public string password;
        }

        public static void RegisterUser(string username, string password, string displayName, 
            RegisterSuccessCallback onSuccess, RegisterFailedCallback onError, out Func<float> progressGetter)
        {
            ThrowIf.StringParameterIsEmpty(nameof(username), username);
            ThrowIf.StringParameterIsEmpty(nameof(password), password);
            ThrowIf.StringParameterIsEmpty(nameof(displayName), displayName);
            OverrideFetch();
            var settings = SettingsAsset.CacheFromResources();
            var requestBody = JsonUtility.ToJson(new RegisterRequestJson
            {
                displayName = displayName,
                password = password,
                username = username
            });
            UnityWebRequest request = UnityWebRequest.Post(settings.ServerUrl + RegisterPath, "");
            request.AttachJsonBody(requestBody);
            var asyncOperationRequest = request.SendWebRequestVerbose();
            progressGetter = () => asyncOperationRequest.progress;
            asyncOperationRequest.completed += operation =>
            {
                var body = asyncOperationRequest.webRequest.downloadHandler.text;
                if (asyncOperationRequest.webRequest.responseCode != 200)
                {
                    onError.Invoke(ErrorResult.DeserializeFromBody(request, body));
                    asyncOperationRequest.webRequest.Dispose();
                    return;
                }
                onSuccess.Invoke(RegisterSuccessResult.DeserializeFromBody(body));
                asyncOperationRequest.webRequest.FetchCookieFromResponse();
                asyncOperationRequest.webRequest.Dispose();
            };
        }

        [Serializable]
        private class RegisterRequestJson
        {
            public string username;
            public string password;
            public string displayName;
        }
    }

    public delegate void LoginSuccessCallback(LoginSuccessResult successResult);
    public delegate void LoginFailedCallback(ErrorResult errorResult);
    public delegate void RegisterSuccessCallback(RegisterSuccessResult successResult);
    public delegate void RegisterFailedCallback(ErrorResult errorResult);
}