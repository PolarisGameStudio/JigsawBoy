using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class SteamManagerPartnerImpl : BaseManagerImpl, ISteamLeaderboards
{
    private static SteamManagerPartnerImpl manager;

    private SteamManagerPartnerImpl()
    {
        baseUrl = CommonUrl.Steam_Url_Powered;
    }

    public static SteamManagerPartnerImpl getInstance()
    {
        if (manager == null)
        {
            manager = new SteamManagerPartnerImpl();
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


    public void getLeaderboradEntriesForUser(ulong leaderboardId,HttpResponseHandler<GetLeaderboardEntriesResult> responseHandler)
    {
        GetLeaderboardEntriesParams baseParams = new GetLeaderboardEntriesParams();
        baseParams.leaderboardid = leaderboardId;
        baseParams.steamid = SteamUser.GetSteamID().m_SteamID;
        baseParams.datarequest = "RequestAroundUser";
        getLeaderboardEntries(baseParams, responseHandler);
    }

    public void getLeaderboradEntriesForGlobal(ulong leaderboardId,int rangestart,int rangeend, HttpResponseHandler<GetLeaderboardEntriesResult> responseHandler)
    {
        GetLeaderboardEntriesParams baseParams = new GetLeaderboardEntriesParams();
        baseParams.leaderboardid = leaderboardId;
        baseParams.datarequest = "RequestGlobal";
        baseParams.rangestart = rangestart;
        baseParams.rangeend = rangeend;
        getLeaderboardEntries(baseParams, responseHandler);
    }

    public void getLeaderboardEntries(GetLeaderboardEntriesParams baseParams, HttpResponseHandler<GetLeaderboardEntriesResult> responseHandler)
    {
        baseParams.key = CommonInfo.Steam_Key_All;
        baseParams.appid = CommonInfo.Steam_App_Id;
        requestGet("ISteamLeaderboards/GetLeaderboardEntries/v1", baseParams, responseHandler);
    }

    public void updateLeaderboardData(ulong leaderboardId,int score, HttpResponseHandler<SetLeaderboardScoreResult> responseHandler)
    {
        SetLeaderboardScoreParams baseParams = new SetLeaderboardScoreParams();
        baseParams.key = CommonInfo.Steam_Key_All;
        baseParams.appid = CommonInfo.Steam_App_Id;
        baseParams.leaderboardid = leaderboardId;
        baseParams.steamid = SteamUser.GetSteamID().m_SteamID;
        baseParams.score = score;
        baseParams.scoremethod = "ForceUpdate";
        requestPostForm("ISteamLeaderboards/SetLeaderboardScore/v1", baseParams, responseHandler);
    }
}

