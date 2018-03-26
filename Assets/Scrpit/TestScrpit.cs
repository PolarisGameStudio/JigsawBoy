using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using Steamworks;

public class TestScrpit : BaseMonoBehaviour,LeaderboardFindResultCallBack
{
    public void leaderboradFindResult(ulong leaderboardId)
    {
        LeaderboardHandleImpl handle = new LeaderboardHandleImpl();
        handle.uploadLeaderboardScore(leaderboardId);
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

        LeaderboardHandleImpl handle=new LeaderboardHandleImpl();
        handle.findLeaderboard("test",this);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
