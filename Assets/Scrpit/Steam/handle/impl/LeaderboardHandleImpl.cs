using UnityEngine;
using System.Collections;
using Steamworks;
using System.Threading;
using System.Collections.Generic;

public class LeaderboardHandleImpl : ILeaderboardHandle
{
    private CallResult<LeaderboardFindResult_t> OnLeaderboardFindResultCallResult;
    private LeaderboardFindResultCallBack OnLeaderboardFindResultCallBack;

    private CallResult<LeaderboardScoresDownloaded_t> OnLeaderboardScoresDownloadedCallResult;
    private LeaderboardEntriesFindResultCallBack OnLeaderboardEntriesFindResultCallBack;

    private CallResult<LeaderboardScoreUploaded_t> OnLeaderboardScoreUploadedCallResult;

    private SteamLeaderboard_t m_SteamLeaderboard;
    private SteamLeaderboardEntries_t m_SteamLeaderboardEntries;

    private int mFindDataType = 0;//0 自己 1所有

    /// <summary>
    /// 查询排行榜回调
    /// </summary>
    /// <param name="pCallback"></param>
    /// <param name="bIOFailure"></param>
    void OnLeaderboardFindResult(LeaderboardFindResult_t pCallback, bool bIOFailure)
    {
        if (pCallback.m_bLeaderboardFound != 0)
        {
            m_SteamLeaderboard = pCallback.m_hSteamLeaderboard;
            if (OnLeaderboardFindResultCallBack != null)
            {
                OnLeaderboardFindResultCallBack.leaderboradFindSuccess(m_SteamLeaderboard.m_SteamLeaderboard);
            }
        }
        else
        {
            OnLeaderboardFindResultCallBack.leaderboradFindFail("查询失败");
        }
    }

    /// <summary>
    /// 查询排行榜数据回调
    /// </summary>
    /// <param name="pCallback"></param>
    /// <param name="bIOFailure"></param>
    void OnLeaderboardScoresDownloaded(LeaderboardScoresDownloaded_t pCallback, bool bIOFailure)
    {
        m_SteamLeaderboardEntries = pCallback.m_hSteamLeaderboardEntries;

        List<GetLeaderboardEntriesResult.LeaderboardEntries> listData = new List<GetLeaderboardEntriesResult.LeaderboardEntries>();
        for (int i=0;i < pCallback.m_cEntryCount; i++)
        {
            LeaderboardEntry_t entry_T;
            bool ret = SteamUserStats.GetDownloadedLeaderboardEntry(m_SteamLeaderboardEntries, i, out entry_T, null, 0);
            GetLeaderboardEntriesResult.LeaderboardEntries itemData=new GetLeaderboardEntriesResult.LeaderboardEntries();
            itemData.score = entry_T.m_nScore;
            itemData.rank = entry_T.m_nGlobalRank;
            itemData.steamID = entry_T.m_steamIDUser.ToString();
            listData.Add(itemData);
        }
        if (mFindDataType == 0)
        {
            OnLeaderboardEntriesFindResultCallBack.leaderboradEntriesFindResultForSelf(listData);
        }
        else if (mFindDataType == 1)
        {
            OnLeaderboardEntriesFindResultCallBack.leaderboradEntriesFindResultForAll(listData);
        }

    }

    /// <summary>
    /// 更新数据
    /// </summary>
    /// <param name="pCallback"></param>
    /// <param name="bIOFailure"></param>
    void OnLeaderboardScoreUploaded(LeaderboardScoreUploaded_t pCallback, bool bIOFailure)
    {
    }

    /// <summary>
    /// 创建排行榜
    /// </summary>
    /// <param name="leaderboardName"></param>
    /// <param name="sortMethod"></param>
    /// <param name="displayType"></param>
    private void findOrCreateLeaderboard(string leaderboardName, ELeaderboardSortMethod sortMethod, ELeaderboardDisplayType displayType)
    {
        OnLeaderboardFindResultCallResult = CallResult<LeaderboardFindResult_t>.Create(OnLeaderboardFindResult);
        SteamAPICall_t handle = SteamUserStats.FindOrCreateLeaderboard(leaderboardName, sortMethod, displayType);
        OnLeaderboardFindResultCallResult.Set(handle);
    }

    /// <summary>
    /// 查询排行榜数据
    /// </summary>
    /// <param name="leaderboardId"></param>
    /// <param name="startRange"></param>
    /// <param name="endRange"></param>
    /// <param name="type"></param>
    /// <param name="callBack"></param>
    private void findLeaderboardEntries(ulong leaderboardId, int startRange, int endRange, ELeaderboardDataRequest type, LeaderboardEntriesFindResultCallBack callBack)
    {
        OnLeaderboardEntriesFindResultCallBack = callBack;
        m_SteamLeaderboard = new SteamLeaderboard_t();
        m_SteamLeaderboard.m_SteamLeaderboard = leaderboardId;
        OnLeaderboardScoresDownloadedCallResult = CallResult<LeaderboardScoresDownloaded_t>.Create(OnLeaderboardScoresDownloaded);
        SteamAPICall_t handle = SteamUserStats.DownloadLeaderboardEntries(m_SteamLeaderboard, type, startRange, endRange);
        //TODO  必须要延迟才能设置回调
        Thread.Sleep(1000);
        OnLeaderboardScoresDownloadedCallResult.Set(handle);
    }
    //--------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// 创建排行榜（已秒作为排名单位）
    /// </summary>
    /// <param name="leaderboardName"></param>
    public void findOrCreateLeaderboardForTimeSeconds(string leaderboardName)
    {
        findOrCreateLeaderboard(leaderboardName, ELeaderboardSortMethod.k_ELeaderboardSortMethodDescending, ELeaderboardDisplayType.k_ELeaderboardDisplayTypeTimeSeconds);
    }

    /// <summary>
    /// 创建排行榜（已毫秒作为排名单位）
    /// </summary>
    /// <param name="leaderboardName"></param>
    public void findOrCreateLeaderboardForTimeMilliSeconds(string leaderboardName)
    {
        findOrCreateLeaderboard(leaderboardName, ELeaderboardSortMethod.k_ELeaderboardSortMethodDescending, ELeaderboardDisplayType.k_ELeaderboardDisplayTypeTimeMilliSeconds);
    }

    /// <summary>
    /// 创建排行榜（已数字作为排名单位）
    /// </summary>
    /// <param name="leaderboardName"></param>
    public void findOrCreateLeaderboardForNumeric(string leaderboardName)
    {
        findOrCreateLeaderboard(leaderboardName, ELeaderboardSortMethod.k_ELeaderboardSortMethodDescending, ELeaderboardDisplayType.k_ELeaderboardDisplayTypeNumeric);
    }

    /// <summary>
    /// 根据排行榜姓名查询ID
    /// </summary>
    /// <param name="leaderboardName"></param>
    /// <param name="callBack"></param>
    public void findLeaderboard(string leaderboardName, LeaderboardFindResultCallBack callBack)
    {
        OnLeaderboardFindResultCallResult = CallResult<LeaderboardFindResult_t>.Create(OnLeaderboardFindResult);
        OnLeaderboardFindResultCallBack = callBack;
        SteamAPICall_t handle = SteamUserStats.FindLeaderboard(leaderboardName);
        OnLeaderboardFindResultCallResult.Set(handle);
    }

    /// <summary>
    /// 根据排行榜ID更新排行榜该用户的分数
    /// </summary>
    /// <param name="leaderboardId"></param>
    /// <param name="score"></param>
    public void uploadLeaderboardScore(ulong leaderboardId, int score)
    {
        m_SteamLeaderboard = new SteamLeaderboard_t();
        m_SteamLeaderboard.m_SteamLeaderboard = leaderboardId;
        OnLeaderboardScoreUploadedCallResult = CallResult<LeaderboardScoreUploaded_t>.Create(OnLeaderboardScoreUploaded);
        SteamAPICall_t handle = SteamUserStats.UploadLeaderboardScore(m_SteamLeaderboard, ELeaderboardUploadScoreMethod.k_ELeaderboardUploadScoreMethodForceUpdate, score, null, 0);
        OnLeaderboardScoreUploadedCallResult.Set(handle);
    }

    
    public void findLeaderboardEntriesForAll(ulong leaderboardId, int startRange, int endRange, LeaderboardEntriesFindResultCallBack callBack)
    {
        mFindDataType = 1;
        findLeaderboardEntries(leaderboardId, startRange, endRange, ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobal, callBack);
    }

    public void findLeaderboardEntriesForUser(ulong leaderboardId, LeaderboardEntriesFindResultCallBack callBack)
    {
        mFindDataType = 0;
        OnLeaderboardEntriesFindResultCallBack = callBack;
        m_SteamLeaderboard = new SteamLeaderboard_t();
        m_SteamLeaderboard.m_SteamLeaderboard = leaderboardId;
        OnLeaderboardScoresDownloadedCallResult = CallResult<LeaderboardScoresDownloaded_t>.Create(OnLeaderboardScoresDownloaded);
        CSteamID[] Users = { SteamUser.GetSteamID() };
        SteamAPICall_t handle = SteamUserStats.DownloadLeaderboardEntriesForUsers(m_SteamLeaderboard, Users, Users.Length);
        //TODO  必须要延迟才能设置回调
        Thread.Sleep(1000);
        OnLeaderboardScoresDownloadedCallResult.Set(handle);
    }
}

