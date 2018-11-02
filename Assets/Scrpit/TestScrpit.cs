using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using Steamworks;
using System.Threading;

public class TestScrpit : BaseMonoBehaviour
{

    void Start()
    {
        AppId_t  mAppId = new AppId_t(uint.Parse(CommonInfo.Steam_App_Id));
        UGCQueryHandle_t handle= SteamUGC.CreateQueryUserUGCRequest(SteamUser.GetSteamID().GetAccountID(),EUserUGCList.k_EUserUGCList_Published,EUGCMatchingUGCType.k_EUGCMatchingUGCType_All,EUserUGCListSortOrder.k_EUserUGCListSortOrder_CreationOrderAsc, mAppId, mAppId,1);

        CallResult<SteamUGCQueryCompleted_t> callResult = CallResult<SteamUGCQueryCompleted_t>.Create(query);
       SteamAPICall_t c= SteamUGC.SendQueryUGCRequest(handle);
        callResult.Set(c);
    }

    public void query(SteamUGCQueryCompleted_t call ,bool bi)
    {
        for(uint i = 0; i < call.m_unNumResultsReturned; i++)
        {
            SteamUGCDetails_t details;
            SteamUGC.GetQueryUGCResult(call.m_handle,i,out details);

            LogUtil.log("item :" + i + " " + details.m_rgchTitle+ " m_hFile" + details.m_hFile+ " m_hPreviewFile" + details.m_hPreviewFile+ " m_pchFileName" + details.m_pchFileName);
            ulong punSizeOnDisk;
            string pchFolder;
            uint cchFolderSize;
            uint punTimeStamp;
           // SteamUGC.GetItemInstallInfo(details.m_nPublishedFileId,out punSizeOnDisk, out  pchFolder, details.m_nFileSize,out punTimeStamp);
     
        }
    
    }

}
