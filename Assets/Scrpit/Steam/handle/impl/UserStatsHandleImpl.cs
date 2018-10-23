using UnityEngine;
using UnityEditor;
using Steamworks;

public class UserStatsHandleImpl : IUserAchievementHandle
{
    public void initUserStats()
    {
        CallResult<UserStatsReceived_t> call = CallResult<UserStatsReceived_t>.Create(onUserStatsReceived);
        SteamAPICall_t steamAPICall_T = SteamUserStats.RequestUserStats(SteamUser.GetSteamID());
        call.Set(steamAPICall_T);
    }

    /// <summary>
    /// 初始化回调
    /// </summary>
    /// <param name="pCallback"></param>
    /// <param name="bIOFailure"></param>
    void onUserStatsReceived(UserStatsReceived_t pCallback, bool bIOFailure)
    {
        LogUtil.log("userStatasInit:" + pCallback.m_nGameID + " " + pCallback.m_steamIDUser);
    }

    public void userCompleteNumberChange(int changeNumber)
    {
        string completeNumberApi = "COMPLETE_NUMBER";
        if (SteamManager.Initialized)
        {
            bool isSetStat = SteamUserStats.SetStat(completeNumberApi, changeNumber);
            LogUtil.log("isSetStat:" + isSetStat);

            bool isUpdateStat = SteamUserStats.StoreStats();
            LogUtil.log("isUpdateStat:" + isUpdateStat);
        }
      
    }

    public void resetAllAchievement()
    {
        if (SteamManager.Initialized)
        {
            bool isResetAll = SteamUserStats.ResetAllStats(true);
            LogUtil.log("isResetAll:" + isResetAll);
        }
    }
}