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
}

