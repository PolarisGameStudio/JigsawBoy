using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public interface ILeaderboardHandle
{
    /// <summary>
    /// 创建时间排行榜
    /// 毫秒
    /// </summary>
    /// <param name="leaderboardName"></param>
    void findOrCreateLeaderboardForTimeSeconds(string leaderboardName);

    /// <summary>
    /// 创建时间排行榜
    /// 秒
    /// </summary>
    /// <param name="leaderboardName"></param>
    void findOrCreateLeaderboardForTimeMilliSeconds(string leaderboardName);

    /// <summary>
    /// 创建分数排行榜
    /// </summary>
    /// <param name="leaderboardName"></param>
    void findOrCreateLeaderboardForNumeric(string leaderboardName);

    /// <summary>
    /// 查询排行榜
    /// </summary>
    /// <param name="leaderboardName"></param>
    void findLeaderboard(string leaderboardName, LeaderboardFindResultCallBack callBack);

}

