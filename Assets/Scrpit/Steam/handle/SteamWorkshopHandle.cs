using UnityEngine;
using UnityEditor;

public class SteamWorkshopHandle
{
    /// <summary>
    /// 创建创意工坊物品
    /// </summary>
    /// <param name="content"></param>
    /// <param name="updateBean"></param>
    /// <param name="callBack"></param>
    public static void CreateWorkshopItem(BaseMonoBehaviour content, SteamWorkshopUpdateBean updateBean, ISteamWorkshopUpdateCallBack callBack)
    {
        if (SteamManager.Initialized)
        {
            SteamWorkshopUpdateImpl update = new SteamWorkshopUpdateImpl(content);
            update.CreateWorkshopItem(updateBean, callBack);
        }     
    }
}