using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class SetLeaderboardScoreParams : BaseParams
{
    /// <summary>
    /// 社区群组KEY
    /// </summary>
    public string key { get; set; }

    /// <summary>
    /// Appid
    /// </summary>
    public string appid { get; set; }

    /// <summary>
    /// 表ID
    /// </summary>
    public ulong leaderboardid { get; set; }

    /// <summary>
    /// 用户steamID
    /// </summary>
    public ulong steamid { get; set; }

    /// <summary>
    /// 分数
    /// </summary>
    public long score { get; set; }

    /// <summary>
    /// 更新方法
    /// 可以是“KeepBest”或“ForceUpdate”
    /// </summary>
    public string scoremethod { get; set; }

    /// <summary>
    /// 比赛的具体细节为如何赢得比赛。最高可达256个字节。
    /// </summary>
    public string details { get; set; }
}

