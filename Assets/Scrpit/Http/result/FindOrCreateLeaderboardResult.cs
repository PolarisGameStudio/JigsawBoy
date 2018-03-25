using System;

[Serializable]
public class FindOrCreateLeaderboardResult
{
    public Result result;

    [Serializable]
    public class Result
    {
        public int result;
        public Leaderboard leaderboard;
    }

    [Serializable]
    public class Leaderboard
    {
        public string leaderboardName;
        public long leaderBoardID;
        public int leaderBoardEntries;
        public string leaderBoardSortMethod;
        public string leaderBoardDisplayType;
        public bool onlytrustedwrites;
        public bool onlyfriendsreads;
    }
}

