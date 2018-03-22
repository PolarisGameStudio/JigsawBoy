using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class FindOrCreateLeaderboardParams : BaseParams
{
    private string key;
    private string appid;
    private string name;

    public string Key
    {
        get
        {
            return key;
        }

        set
        {
            key = value;
        }
    }

    public string Appid
    {
        get
        {
            return appid;
        }

        set
        {
            appid = value;
        }
    }

    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
        }
    }
}

