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

    /// <summary>
    /// 查询排行榜数据
    /// 全球
    /// </summary>
    /// <param name="leaderboardId"></param>
    /// <param name="startRange"></param>
    /// <param name="endRange"></param>
    /// <param name="callBack"></param>
    void findLeaderboardEntriesForAll(ulong leaderboardId, int startRange, int endRange, LeaderboardEntriesFindResultCallBack callBack);

    /// <summary>
    /// 查询排行榜数据
    /// 用户
    /// </summary>
    /// <param name="leaderboardId"></param>
    /// <param name="callBack"></param>
    void findLeaderboardEntriesForUser(ulong leaderboardId, LeaderboardEntriesFindResultCallBack callBack);

}

