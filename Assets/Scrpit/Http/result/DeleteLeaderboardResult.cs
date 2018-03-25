using System;

[Serializable]
public class DeleteLeaderboardResult
{
    public Result result;

    [Serializable]
    public class Result
    {
        public int result;
    }
}

