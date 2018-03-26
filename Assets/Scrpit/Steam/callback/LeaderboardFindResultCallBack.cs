using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public interface LeaderboardFindResultCallBack
{
    /// <summary>
    /// 排行榜查询结果
    /// </summary>
    /// <param name="leaderboardId"></param>
    void leaderboradFindResult(ulong leaderboardId);
}
