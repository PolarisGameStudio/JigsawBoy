using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class FindOrCreateLeaderboardParams : BaseParams
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

    /// <summary>
    /// 可选
    /// 排序方式默认为升序
    /// </summary>
    public string sortmethod { get; set; }

    /// <summary>
    /// 可选
    /// 该排行榜的显示类型(默认为数字)
    /// </summary>
    public string displaytype { get; set; }

    /// <summary>
    /// 可选
    /// 如果这是真的，那么在不存在的情况下就会创建排行榜。默认值为true。
    /// </summary>
    public bool createifnotfound{ get; set; }

    /// <summary>
    /// 可选
    /// 如果这是真的，那么排行榜的分数不能由客户来设定，只能由出版商通过SetLeaderboardScore WebAPI来设置。默认值为false。
    /// </summary>
    public bool onlytrustedwrites { get; set; }

    /// <summary>
    /// 可选
    /// 如果这是真的，排行榜的分数只能由客户为朋友阅读，分数可以一直由出版商来阅读。默认值为false。
    /// </summary>
    public bool onlyfriendsreads { get; set; }
    
}

