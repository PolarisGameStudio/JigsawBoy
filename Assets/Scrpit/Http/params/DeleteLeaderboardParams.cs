using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class DeleteLeaderboardParams : BaseParams
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
    /// 表名
    /// </summary>
    public string name { get; set; }
}
