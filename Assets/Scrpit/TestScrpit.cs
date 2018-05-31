using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using Steamworks;

public class TestScrpit : BaseMonoBehaviour, LeaderboardFindResultCallBack, LeaderboardEntriesFindResultCallBack
{
    public void leaderboradEntriesFindResult(ulong leaderboardId)
    {
        throw new System.NotImplementedException();
    }

    public void leaderboradFindResult(ulong leaderboardId)
    {
        //LeaderboardHandleImpl handle = new LeaderboardHandleImpl();
        //handle.findLeaderboardEntriesForUser(leaderboardId,this);
        //GetLeaderboardEntriesParams baseParams = new GetLeaderboardEntriesParams();
        //baseParams.key = CommonInfo.Steam_Key_All;
        //baseParams.appid = CommonInfo.Steam_App_Id;

        //baseParams.leaderboardid = leaderboardId;
        //baseParams.steamid = SteamUser.GetSteamID().m_SteamID;
        //baseParams.datarequest = "RequestAroundUser";
        //MHttpManagerFactory.getSteamManager().getLeaderboardEntries(baseParams, new TestCallBack());
    }

    void Start()
    {
        //FindOrCreateLeaderboardParams baseParams = new FindOrCreateLeaderboardParams();
        //baseParams.key = "B0147AEB59B2D274DBF8BF54AAA7C0AB";
        //baseParams.appid = "830620";
        //baseParams.name = "testLeader";
        //baseParams.createifnotfound = true;
        //MHttpManagerFactory.getSteamManager().findOrCreateLeaderboard(baseParams, new TestHandle());

        //DeleteLeaderboardParams baseParams = new DeleteLeaderboardParams();
        //baseParams.key = "B0147AEB59B2D274DBF8BF54AAA7C0AB";
        //baseParams.appid = "830620";
        //baseParams.name = "testLeader";
        //MHttpManagerFactory.getSteamManager().deleteLeaderboard(baseParams, new TestHandle());

        //LeaderboardHandleImpl handle = new LeaderboardHandleImpl();
        //handle.findLeaderboard("test", this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public class TestCallBack : HttpResponseHandler<GetLeaderboardEntriesResult>
    {
        public override void onError(string message)
        {

        }

        public override void onSuccess(GetLeaderboardEntriesResult result)
        {

        }
    }
}
