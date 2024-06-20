#if LEADERBOARD_UNITY
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Leaderboards;
using LeaderboardEntryUnity = Unity.Services.Leaderboards.Models.LeaderboardEntry;
using LeaderboardScoresPageUnity = Unity.Services.Leaderboards.Models.LeaderboardScoresPage;
using UnityEngine;

namespace DNDFramework.Leaderboard
{
    public class LeaderboardsUnity : LeaderboardBase
    {
        public override string playerName => AuthenticationService.Instance.PlayerName;

        protected override async void InitLeaderboardAsync()
        {

            await UnityServices.InitializeAsync();

            await SignInAnonymously();
        }
        async Task SignInAnonymously()
        {
            AuthenticationService.Instance.SignedIn += () =>
            {
                Debug.Log("Signed in as: " + JsonConvert.SerializeObject(AuthenticationService.Instance.PlayerInfo));
                isReady = true;
                callbackSignedIn?.Invoke(true);
                // AuthenticationService.Instance.ChangeUn
            };

            AuthenticationService.Instance.SignInFailed += s =>
            {
                // Take some action here...
                isReady = false;
                Debug.Log(s);
                callbackSignedIn?.Invoke(false);
            };

            // await AuthenticationService.Instance.AddUsernamePasswordAsync("dai1234", "dai12314");
            await AuthenticationService.Instance.SignInAnonymouslyAsync();

        }
        public override async void GetVersions(string LeaderboardId)
        {
            var versionResponse =
                await LeaderboardsService.Instance.GetVersionsAsync(LeaderboardId);

            // As an example, get the ID of the most recently archived Leaderboard version
            // VersionId = versionResponse.Results[0].Id;
            Debug.Log(JsonConvert.SerializeObject(versionResponse));
        }

        public override async void UpdateNamePlayer(string name, Action<string> _callback)
        {
            if (!isReady)
            {
                _callback?.Invoke("");
                return;
            }

            await AuthenticationService.Instance.UpdatePlayerNameAsync(name);
            // Debug.Log(JsonConvert.SerializeObject());
            // AuthenticationService.Instance.get
            _callback?.Invoke(AuthenticationService.Instance.PlayerName);
        }

        // public void GetInfoPlayer()
        // {
        //     Debug.Log(JsonConvert.SerializeObject(AuthenticationService.Instance.PlayerInfo));
        // }
        public override async void AddScore(string LeaderboardId, double _value, Action<LeaderboardEntry> _callback)
        {
            if (!isReady)
            {
                _callback?.Invoke(null);
                return;
            }
            LeaderboardEntryUnity scoreResponse = await LeaderboardsService.Instance.AddPlayerScoreAsync(LeaderboardId, _value);
            _callback?.Invoke(ConventLeaderboardEntryUnity(scoreResponse));
            // Debug.Log(JsonConvert.SerializeObject(scoreResponse));
        }

        public override async void GetScore(string LeaderboardId, Action<LeaderboardEntry> _callback)
        {
            if (!isReady)
            {
                _callback?.Invoke(null);
                return;
            }
            LeaderboardEntryUnity scoreResponse = await LeaderboardsService.Instance.GetPlayerScoreAsync(LeaderboardId);
            _callback?.Invoke(ConventLeaderboardEntryUnity(scoreResponse));
            // Debug.Log(JsonConvert.SerializeObject(scoreResponse));
        }

        public override async void GetScores(string LeaderboardId, Action<LeaderboardScoresPage> _callback, int _offset, int _limmit)
        {
            if (!isReady)
            {
                _callback?.Invoke(null);
                return;
            }
            LeaderboardScoresPageUnity scoresResponse =
                await LeaderboardsService.Instance.GetScoresAsync(LeaderboardId, new GetScoresOptions { Offset = _offset, Limit = _limmit });
            _callback?.Invoke(ConventLeaderboardScoresPageUnity(scoresResponse));
            // Debug.Log(JsonConvert.SerializeObject(scoresResponse));
        }

        LeaderboardEntry ConventLeaderboardEntryUnity(LeaderboardEntryUnity entryUnity)
        {
            return new LeaderboardEntry(entryUnity.PlayerId, entryUnity.PlayerName, entryUnity.Rank, entryUnity.Score, entryUnity.Tier, entryUnity.UpdatedTime, entryUnity.Metadata);
        }
        LeaderboardScoresPage ConventLeaderboardScoresPageUnity(LeaderboardScoresPageUnity scoresPageUnity)
        {
            return new LeaderboardScoresPage(scoresPageUnity.Offset, scoresPageUnity.Limit, scoresPageUnity.Total, scoresPageUnity.Results.ConvertAll(e => ConventLeaderboardEntryUnity(e)));
        }
    }
}
#endif
