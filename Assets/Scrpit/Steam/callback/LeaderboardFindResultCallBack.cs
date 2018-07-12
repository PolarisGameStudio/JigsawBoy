using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public interface LeaderboardFindResultCallBack
{
    /// <summary>
    /// 排行榜查询结果成功
    /// </summary>
    /// <param name="leaderboardId"></param>
    void leaderboradFindSuccess(ulong leaderboardId);

    /// <summary>
    /// 排行榜查询结果失败
    /// </summary>
    void leaderboradFindFail(string msg);
}
