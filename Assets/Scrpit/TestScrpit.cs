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
        SteamWorkshopHandle.QueryInstallInfo(this,1,new CallBackTest());
    }

    public class CallBackTest : ISteamWorkshopQueryInstallInfoCallBack
    {
        public void GetInstallInfoFail(SteamWorkshopQueryImpl.SteamWorkshopQueryFailEnum failEnum)
        {
            throw new System.NotImplementedException();
        }

        public void GetInstallInfoSuccess(List<SteamWorkshopQueryInstallInfoBean> listData)
        {
            foreach (SteamWorkshopQueryInstallInfoBean item in listData)
            {
                LogUtil.log("pchFolder:" + item.pchFolder+ "punSizeOnDisk:" + item.punSizeOnDisk);
            }

        }
    }
}
