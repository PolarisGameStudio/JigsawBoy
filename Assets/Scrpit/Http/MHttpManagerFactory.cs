using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class MHttpManagerFactory
{
    //steam web API
    public static SteamManagerPartnerImpl getSteamManagerPartner()
    {
        return SteamManagerPartnerImpl.getInstance();
    }
    public static SteamManagerPoweredImpl getSteamManagerPowered()
    {
        return SteamManagerPoweredImpl.getInstance();
    }
}

