using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using Steamworks;

public class TestScrpit : BaseMonoBehaviour, LeaderboardFindResultCallBack, LeaderboardEntriesFindResultCallBack
{

    public void leaderboradEntriesFindResult(List<GetLeaderboardEntriesResult.LeaderboardEntries> resultList)
    {
        for (int i = 0; i < resultList.Count; i++) {
            LogUtil.log("resultList Item：i=" + i+" steamId:"+resultList[i].steamID+" rank:"+resultList[i].rank);
            List<ulong> listIds = new List<ulong>();
            listIds.Add(ulong.Parse(resultList[i].steamID));
            MHttpManagerFactory.getSteamManagerPowered().getSteamUserInfo(listIds, new TestCallBack());
        }
    }

    public void leaderboradFindResult(ulong leaderboardId)
    {
        LogUtil.log("leaderboradFindResult：" + leaderboardId);
        LeaderboardHandleImpl handle = new LeaderboardHandleImpl();
        handle.findLeaderboardEntriesForUser(leaderboardId,this);
      // handle.uploadLeaderboardScore(leaderboardId,10);
       // MHttpManagerFactory.getSteamManager().getLeaderboradEntriesForUser(leaderboardId, new TestCallBack());
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

        LeaderboardHandleImpl handle = new LeaderboardHandleImpl();
       handle.findLeaderboard("test", this);

     
    }

    // Update is called once per frame
    void Update()
    {

    }

    public class TestCallBack : HttpResponseHandler<SteamUserInfoResult>
    {
        public override void onError(string message)
        {

        }

        public override void onSuccess(SteamUserInfoResult result)
        {

        }
    }
}
