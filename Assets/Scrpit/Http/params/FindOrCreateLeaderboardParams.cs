using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class FindOrCreateLeaderboardParams : BaseParams
{
    public string key { get; set; }
    public string appid { get; set; }
    public string name { get; set; }
}

