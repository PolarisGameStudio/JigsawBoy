using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public interface ISteamLeaderboards
{
    /// <summary>
    /// 查询创建排行表
    /// </summary>
    /// <param name="baseParams"></param>
    /// <param name="responseHandler"></param>
    void findOrCreateLeaderboard(FindOrCreateLeaderboardParams baseParams, HttpResponseHandler<FindOrCreateLeaderboardResult> responseHandler);

    /// <summary>
    /// 删除排行榜
    /// </summary>
    /// <param name="baseParams"></param>
    /// <param name="responseHandler"></param>
    void deleteLeaderboard(DeleteLeaderboardParams baseParams, HttpResponseHandler<DeleteLeaderboardResult> responseHandler);


    /// <summary>
    /// 根据获取自己的排行榜数据
    /// </summary>
    /// <param name="leaderboardId"></param>
    /// <param name="responseHandler"></param>
    void getLeaderboradEntriesForUser(ulong leaderboardId, HttpResponseHandler<GetLeaderboardEntriesResult> responseHandler);


    /// <summary>
    /// 获取全球排名
    /// </summary>
    /// <param name="leaderboardId"></param>
    /// <param name="responseHandler"></param>
    void getLeaderboradEntriesForGlobal(ulong leaderboardId, int rangestart, int rangeend, HttpResponseHandler<GetLeaderboardEntriesResult> responseHandler);

    /// <summary>
    /// 获取排行榜数据
    /// </summary>
    /// <param name="baseParams"></param>
    /// <param name="responseHandler"></param>
    void getLeaderboardEntries(GetLeaderboardEntriesParams baseParams, HttpResponseHandler<GetLeaderboardEntriesResult> responseHandler);

    /// <summary>
    /// 更新排行榜数据
    /// </summary>
    /// <param name="leaderboardId"></param>
    /// <param name="score"></param>
    /// <param name="responseHandler"></param>
    void updateLeaderboardData(ulong leaderboardId, int score, HttpResponseHandler<SetLeaderboardScoreResult> responseHandler);

}

