using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class MHttpManagerFactory
{
    //steam web API
    public static SteamManagerImpl getSteamManager()
    {
        return SteamManagerImpl.getInstance();
    }
}

