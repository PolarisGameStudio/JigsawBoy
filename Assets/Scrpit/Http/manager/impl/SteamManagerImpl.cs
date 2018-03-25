using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class SteamManagerImpl : BaseManagerImpl, ISteamLeaderboards
{
    private static SteamManagerImpl manager;

    private SteamManagerImpl()
    {
        baseUrl = CommonInfo.Base_Steam_Url;
    }

    public static SteamManagerImpl getInstance()
    {
        if (manager == null)
        {
            manager = new SteamManagerImpl();
        }
        return manager;
    }

    public void deleteLeaderboard(DeleteLeaderboardParams baseParams, HttpResponseHandler<DeleteLeaderboardResult> responseHandler)
    {
        requestPostForm("ISteamLeaderboards/DeleteLeaderboard/v1", baseParams, responseHandler);
    }

    public void findOrCreateLeaderboard(FindOrCreateLeaderboardParams baseParams,HttpResponseHandler<FindOrCreateLeaderboardResult> responseHandler)
    {
        requestPostForm("ISteamLeaderboards/FindOrCreateLeaderboard/v2", baseParams, responseHandler);
    }
}

