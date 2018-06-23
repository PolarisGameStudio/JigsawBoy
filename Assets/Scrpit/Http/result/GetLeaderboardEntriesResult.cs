using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class GetLeaderboardEntriesResult
{
    public LeaderboardEntryInformation leaderboardEntryInformation;

    [Serializable]
    public class LeaderboardEntryInformation
    {
        public string appID;//appid
        public long leaderboardID;//排行榜ID
        public int totalLeaderBoardEntryCount;//条数
        public List<LeaderboardEntries> leaderboardEntries;//数据
    }

    [Serializable]
    public  class LeaderboardEntries
    {
        public string steamID;//steamID
        public int score;//分数
        public int rank;//排名 
        public string ugcid;
    }
}

