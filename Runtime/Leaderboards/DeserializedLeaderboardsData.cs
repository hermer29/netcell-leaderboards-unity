using UnityEngine;

namespace NetCellLeaderboards.Runtime
{
    public class DeserializedLeaderboardsData
    {
        private string _json;
        private GetBoardResult _cache;
        
        public DeserializedLeaderboardsData(string json)
        {
            _json = json;
        }

        public GetBoardResult Value
        {
            get
            {
                if(_cache == null)
                    DeserializeResult();
                return _cache;
            }
        }

        private void DeserializeResult()
        {
            _json = "{\"entries\" :" + _json + "}";
            _cache = JsonUtility.FromJson<GetBoardResult>(_json);
        }
    }
}