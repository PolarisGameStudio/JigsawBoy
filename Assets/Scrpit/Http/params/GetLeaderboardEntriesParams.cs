using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class GetLeaderboardEntriesParams : BaseParams
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
    /// 查询起始
    /// </summary>
    public int rangestart { get; set; }

    /// <summary>
    /// 查询结束
    /// </summary>
    public int rangeend { get; set; }

    /// <summary>
    /// 用户SteamId
    /// </summary>
    public ulong steamid { get; set; }

    /// <summary>
    /// 排行榜ID
    /// </summary>
    public ulong leaderboardid { get; set; }

    /// <summary>
    /// 请求类型
    /// </summary>
    public string datarequest { get; set; }
}

