using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class SteamManagerPoweredImpl : BaseManagerImpl, ISteamUserInfo
{
    private static SteamManagerPoweredImpl manager;

    private SteamManagerPoweredImpl()
    {
        baseUrl = CommonUrl.Steam_Url_Powered;
    }

    public static SteamManagerPoweredImpl getInstance()
    {
        if (manager == null)
        {
            manager = new SteamManagerPoweredImpl();
        }
        return manager;
    }


    public void getSteamUserInfo(List<ulong> userId, HttpResponseHandler<SteamUserInfoResult> responseHandler)
    {
        SteamUserInfoParams baseParams = new SteamUserInfoParams();
        string steamIds= DevUtil.listToStringBySplit(userId,",");
        baseParams.steamids = steamIds;
        baseParams.key = CommonInfo.Steam_Key_All;
        baseParams.appid = CommonInfo.Steam_App_Id;
        requestGet("ISteamUser/GetPlayerSummaries/v2", baseParams, responseHandler);
    }


}

