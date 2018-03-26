using UnityEngine;
using System.Collections;
using Steamworks;

public class LeaderboardHandleImpl : ILeaderboardHandle
{
    private CallResult<LeaderboardFindResult_t> OnLeaderboardFindResultCallResult;
    private LeaderboardFindResultCallBack OnLeaderboardFindResultCallBack;

    private CallResult<LeaderboardScoresDownloaded_t> OnLeaderboardScoresDownloadedCallResult;

    private CallResult<LeaderboardScoreUploaded_t> OnLeaderboardScoreUploadedCallResult;

    private SteamLeaderboard_t m_SteamLeaderboard;
    private SteamLeaderboardEntries_t m_SteamLeaderboardEntries;


    /// <summary>
    /// 查询排行榜回调
    /// </summary>
    /// <param name="pCallback"></param>
    /// <param name="bIOFailure"></param>
    private void OnLeaderboardFindResult(LeaderboardFindResult_t pCallback, bool bIOFailure)
    {
        if (pCallback.m_bLeaderboardFound != 0)
        {
            m_SteamLeaderboard = pCallback.m_hSteamLeaderboard;
            if (OnLeaderboardFindResultCallBack != null)
            {
                OnLeaderboardFindResultCallBack.leaderboradFindResult(m_SteamLeaderboard.m_SteamLeaderboard);
            }
        }
    }

    /// <summary>
    /// 查询排行榜数据回调
    /// </summary>
    /// <param name="pCallback"></param>
    /// <param name="bIOFailure"></param>
    private void OnLeaderboardScoresDownloaded(LeaderboardScoresDownloaded_t pCallback, bool bIOFailure)
    {
        m_SteamLeaderboardEntries = pCallback.m_hSteamLeaderboardEntries;
    }

    /// <summary>
    /// 更新数据
    /// </summary>
    /// <param name="pCallback"></param>
    /// <param name="bIOFailure"></param>
    void OnLeaderboardScoreUploaded(LeaderboardScoreUploaded_t pCallback, bool bIOFailure)
    {
   }

    private void findOrCreateLeaderboard(string leaderboardName, ELeaderboardSortMethod sortMethod, ELeaderboardDisplayType displayType)
    {
        OnLeaderboardFindResultCallResult = CallResult<LeaderboardFindResult_t>.Create(OnLeaderboardFindResult);
        SteamAPICall_t handle = SteamUserStats.FindOrCreateLeaderboard(leaderboardName, sortMethod, displayType);
        OnLeaderboardFindResultCallResult.Set(handle);
    }

    //--------------------------------------------------------------------------------------------------------------------------------

    public void findOrCreateLeaderboardForTimeSeconds(string leaderboardName)
    {
        findOrCreateLeaderboard(leaderboardName, ELeaderboardSortMethod.k_ELeaderboardSortMethodDescending, ELeaderboardDisplayType.k_ELeaderboardDisplayTypeTimeSeconds);
    }

    public void findOrCreateLeaderboardForTimeMilliSeconds(string leaderboardName)
    {
        findOrCreateLeaderboard(leaderboardName, ELeaderboardSortMethod.k_ELeaderboardSortMethodDescending, ELeaderboardDisplayType.k_ELeaderboardDisplayTypeTimeMilliSeconds);
    }

    public void findOrCreateLeaderboardForNumeric(string leaderboardName)
    {
        findOrCreateLeaderboard(leaderboardName, ELeaderboardSortMethod.k_ELeaderboardSortMethodDescending, ELeaderboardDisplayType.k_ELeaderboardDisplayTypeNumeric);
    }

    public void findLeaderboard(string leaderboardName, LeaderboardFindResultCallBack callBack)
    {
        OnLeaderboardFindResultCallResult = CallResult<LeaderboardFindResult_t>.Create(OnLeaderboardFindResult);
        OnLeaderboardFindResultCallBack = callBack;
        SteamAPICall_t handle = SteamUserStats.FindLeaderboard(leaderboardName);
        OnLeaderboardFindResultCallResult.Set(handle);
    }

    public void findLeaderboardEntries(ulong leaderboardId,int startRange,int endRange)
    {
        m_SteamLeaderboard = new SteamLeaderboard_t();
        m_SteamLeaderboard.m_SteamLeaderboard = leaderboardId;
        OnLeaderboardScoresDownloadedCallResult = CallResult<LeaderboardScoresDownloaded_t>.Create(OnLeaderboardScoresDownloaded);
        SteamAPICall_t handle = SteamUserStats.DownloadLeaderboardEntries(m_SteamLeaderboard, ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobal, startRange, endRange);
        OnLeaderboardScoresDownloadedCallResult.Set(handle);
    }

    public void uploadLeaderboardScore(ulong leaderboardId)
    {
        m_SteamLeaderboard = new SteamLeaderboard_t();
        m_SteamLeaderboard.m_SteamLeaderboard = leaderboardId;
        OnLeaderboardScoreUploadedCallResult = CallResult<LeaderboardScoreUploaded_t>.Create(OnLeaderboardScoreUploaded);
        SteamAPICall_t handle = SteamUserStats.UploadLeaderboardScore(m_SteamLeaderboard, ELeaderboardUploadScoreMethod.k_ELeaderboardUploadScoreMethodNone, 1, null, 0);
        OnLeaderboardScoreUploadedCallResult.Set(handle);
    }
}

