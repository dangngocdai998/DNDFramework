using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Scripting;

namespace DNDFramework.Leaderboard
{
    //Demo 
    // public void Init()
    //         {
    //     #if LEADERBOARD_UNITY
    //             leaderboards = new LeaderboardsUnity();
    // #else
    //             leaderboards = new LeaderboardBase();
    // #endif
    //             leaderboards.Initialization(CompletedSignedIn);
    //         }

    public abstract class LeaderboardBase
    {
        public virtual string playerName => "";
        // #endif

        public bool isReady { get; protected set; } = false;

        protected Action<bool> callbackSignedIn;

        public void Initialization()
        {
            isReady = false;
            callbackSignedIn = null;
            InitLeaderboardAsync();
        }

        public void Initialization(Action<bool> _callback)
        {
            isReady = false;
            callbackSignedIn = _callback;
            InitLeaderboardAsync();
        }

        protected abstract void InitLeaderboardAsync();
        public abstract void GetVersions(string LeaderboardId);

        public abstract void UpdateNamePlayer(string name, Action<string> _callback);

        public abstract void AddScore(string LeaderboardId, double _value, Action<LeaderboardEntry> _callback);

        public abstract void GetScore(string LeaderboardId, Action<LeaderboardEntry> _callback);
        public abstract void GetScores(string LeaderboardId, Action<LeaderboardScoresPage> _callback, int _offset, int _limmit);

    }
    [Serializable]
    public class LeaderboardEntry
    {
        /// <summary>
        /// Creates an instance of LeaderboardEntry.
        /// </summary>
        /// <param name="playerId">playerId param</param>
        /// <param name="playerName">playerName param</param>
        /// <param name="rank">rank param</param>
        /// <param name="score">score param</param>
        /// <param name="tier">tier param</param>
        /// <param name="updatedTime">updatedTime param</param>
        /// <param name="metadata">metadata param</param>

        public LeaderboardEntry(string playerId, string playerName, int rank, double score, string tier = default, DateTime updatedTime = default, string metadata = null)
        {
            PlayerId = playerId;
            PlayerName = playerName;
            Rank = rank;
            Score = score;
            Tier = tier;
            UpdatedTime = updatedTime;
            Metadata = metadata;
        }


        // internal LeaderboardEntry(Internal.Models.LeaderboardEntry entry)
        // {
        //     PlayerId = entry.PlayerId;
        //     PlayerName = entry.PlayerName;
        //     Rank = entry.Rank;
        //     Score = entry.Score;
        //     Tier = entry.Tier;
        //     UpdatedTime = entry.UpdatedTime;
        //     Metadata = entry.Metadata == null ? null : JsonConvert.SerializeObject(entry.Metadata);
        // }

        /// <summary>
        /// Parameter playerId of LeaderboardEntry
        /// </summary>
        [Preserve]
        [DataMember(Name = "playerId", IsRequired = true, EmitDefaultValue = true)]
        public string PlayerId { get; }

        /// <summary>
        /// Parameter playerName of LeaderboardEntry
        /// </summary>
        [Preserve]
        [DataMember(Name = "playerName", IsRequired = true, EmitDefaultValue = true)]
        public string PlayerName { get; }

        /// <summary>
        /// Parameter rank of LeaderboardEntry
        /// </summary>
        [Preserve]
        [DataMember(Name = "rank", IsRequired = true, EmitDefaultValue = true)]
        public int Rank { get; }

        /// <summary>
        /// Parameter score of LeaderboardEntry
        /// </summary>
        [Preserve]
        [DataMember(Name = "score", IsRequired = true, EmitDefaultValue = true)]
        public double Score { get; }

        /// <summary>
        /// Parameter tier of LeaderboardEntry
        /// </summary>
        [Preserve]
        [DataMember(Name = "tier", EmitDefaultValue = false)]
        public string Tier { get; }

        /// <summary>
        /// Parameter updatedTime of LeaderboardEntry
        /// </summary>
        [Preserve]
        [DataMember(Name = "updatedTime", EmitDefaultValue = false)]
        public DateTime UpdatedTime { get; }

        /// <summary>
        /// Parameter metadata of LeaderboardEntry
        /// </summary>
        [Preserve]
        [DataMember(Name = "metadata", EmitDefaultValue = false)]
        public string Metadata { get; }
    }

    [Preserve]
    [DataContract(Name = "LeaderboardScoresPage")]
    public class LeaderboardScoresPage
    {
        /// <summary>
        /// Creates an instance of LeaderboardScoresPage.
        /// </summary>
        /// <param name="offset">offset param</param>
        /// <param name="limit">limit param</param>
        /// <param name="total">total param</param>
        /// <param name="results">results param</param>
        [Preserve]
        public LeaderboardScoresPage(int offset = default, int limit = default, int total = default, List<LeaderboardEntry> results = default)
        {
            Offset = offset;
            Limit = limit;
            Total = total;
            Results = results;
        }

        // [Preserve]
        // internal LeaderboardScoresPage(Internal.Models.LeaderboardScoresPage page)
        // {
        //     Offset = page.Offset;
        //     Limit = page.Limit;
        //     Total = page.Total;
        //     Results = page.Results.ConvertAll(e => new LeaderboardEntry(e));
        // }

        /// <summary>
        /// Parameter offset of LeaderboardScoresPage
        /// </summary>
        [Preserve]
        [DataMember(Name = "offset", EmitDefaultValue = false)]
        public int Offset { get; }

        /// <summary>
        /// Parameter limit of LeaderboardScoresPage
        /// </summary>
        [Preserve]
        [DataMember(Name = "limit", EmitDefaultValue = false)]
        public int Limit { get; }

        /// <summary>
        /// Parameter total of LeaderboardScoresPage
        /// </summary>
        [Preserve]
        [DataMember(Name = "total", EmitDefaultValue = false)]
        public int Total { get; }

        /// <summary>
        /// Parameter results of LeaderboardScoresPage
        /// </summary>
        [Preserve]
        [DataMember(Name = "results", EmitDefaultValue = false)]
        public List<LeaderboardEntry> Results { get; }
    }
}
