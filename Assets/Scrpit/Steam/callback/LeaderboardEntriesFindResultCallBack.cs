using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public interface LeaderboardEntriesFindResultCallBack
{
    /// <summary>
    /// 排行榜值查询结果 自己
    /// </summary>
    /// <param name="leaderboardId"></param>
    void leaderboradEntriesFindResultForSelf(List<GetLeaderboardEntriesResult.LeaderboardEntries> resultList);

    /// <summary>
    /// 排行榜值查询结果 所有用户
    /// </summary>
    /// <param name="leaderboardId"></param>
    void leaderboradEntriesFindResultForAll(List<GetLeaderboardEntriesResult.LeaderboardEntries> resultList);
}

