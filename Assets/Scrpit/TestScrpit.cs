using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using Steamworks;

public class TestScrpit : BaseMonoBehaviour
{
    private CallResult<LeaderboardFindResult_t> OnLeaderboardFindResultCallResult;
    private SteamLeaderboard_t m_SteamLeaderboard;
    // Use this for initialization
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
        OnLeaderboardFindResultCallResult = CallResult<LeaderboardFindResult_t>.Create(OnLeaderboardFindResult);

        SteamAPICall_t handle = SteamUserStats.FindOrCreateLeaderboard("Feet Traveled", ELeaderboardSortMethod.k_ELeaderboardSortMethodAscending, ELeaderboardDisplayType.k_ELeaderboardDisplayTypeNumeric);
        OnLeaderboardFindResultCallResult.Set(handle);
        print("SteamUserStats.FindOrCreateLeaderboard(" + "\"Feet Traveled\"" + ", " + ELeaderboardSortMethod.k_ELeaderboardSortMethodAscending + ", " + ELeaderboardDisplayType.k_ELeaderboardDisplayTypeNumeric + ") : " + handle);


    }
    void OnLeaderboardFindResult(LeaderboardFindResult_t pCallback, bool bIOFailure)
    {
        Debug.Log("[" + LeaderboardFindResult_t.k_iCallback + " - LeaderboardFindResult] - " + pCallback.m_hSteamLeaderboard + " -- " + pCallback.m_bLeaderboardFound);

        if (pCallback.m_bLeaderboardFound != 0)
        {
            m_SteamLeaderboard = pCallback.m_hSteamLeaderboard;
        }
    }
    // Update is called once per frame
    void Update()
    {

    }

    public class TestHandle : HttpResponseHandler<DeleteLeaderboardResult>
    {
        public override void onError(string message)
        {
          
        }

        public override void onSuccess(DeleteLeaderboardResult result)
        {
            
        }
    }
}
