using System;

[Serializable]
public class FindOrCreateLeaderboardResult
{
    public Result result;

    [Serializable]
    public class Result
    {
        public int result;
    }
}

