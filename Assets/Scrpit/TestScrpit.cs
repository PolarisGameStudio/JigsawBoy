using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;


public class TestScrpit : BaseMonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        FindOrCreateLeaderboardParams baseParams = new FindOrCreateLeaderboardParams();
        baseParams.Key = "B0147AEB59B2D274DBF8BF54AAA7C0AB";
        baseParams.Appid = "830620";
        baseParams.Name = "testLeader";
        MHttpManagerFactory.getSteamManager().findOrCreateLeaderboard(baseParams, new TestHandle());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public class TestHandle : HttpResponseHandler<FindOrCreateLeaderboardResult>
    {
        public override void onError(string message)
        {
          
        }

        public override void onSuccess(FindOrCreateLeaderboardResult result)
        {
           
        }
    }
}
