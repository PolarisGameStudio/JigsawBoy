using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public interface LeaderboardEntriesFindResultCallBack
{
    /// <summary>
    /// 排行榜值查询结果
    /// </summary>
    /// <param name="leaderboardId"></param>
    void leaderboradEntriesFindResult(List<GetLeaderboardEntriesResult.LeaderboardEntries> resultList);
}

