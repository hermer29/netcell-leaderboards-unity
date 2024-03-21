using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace NetCellLeaderboards.Runtime.Requests
{
    public static class UnityWebRequestExtensions
    {
        public static void AttachJsonBody(this UnityWebRequest request, string json)
        {
            var utf8EncodedJson = Encoding.UTF8.GetBytes(json);
            var handler = new UploadHandlerRaw(utf8EncodedJson);
            handler.contentType = "text/plain";
            request.uploadHandler = handler;
        }

        public static UnityWebRequestAsyncOperation SendWebRequestVerbose(this UnityWebRequest request)
        {
            Debug.Log("Making request: " +
                      $"method - \"{request.method}\", " +
                      $"url - \"{request.url}\", " +
                      $"uri - \"{request.uri}\", " +
                      $"content-type - \"{request.uploadHandler.contentType}, " +
                      $"body - \"{Encoding.UTF8.GetString(request.uploadHandler.data)}");
            return request.SendWebRequest();
        }
    }
}