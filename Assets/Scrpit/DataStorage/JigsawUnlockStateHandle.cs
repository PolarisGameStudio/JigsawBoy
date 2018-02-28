using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class JigsawUnlockStateHandle : BaseDataStorageHandle<JigsawUnlockStateBean, JigsawUnlockStateBean>
{
    //解锁状态关键字
    private const string Unlock_Key = "Unlock_Key";

    private static BaseDataStorageHandle<JigsawUnlockStateBean, JigsawUnlockStateBean> handle;

    public static BaseDataStorageHandle<JigsawUnlockStateBean, JigsawUnlockStateBean> getInstance()
    {
        if (handle == null)
        {
            handle = new JigsawUnlockStateHandle();
        }
        return handle;
    }

    public JigsawUnlockStateBean getData(JigsawUnlockStateBean markData)
    {
        JigsawUnlockStateBean dataBean = new JigsawUnlockStateBean();
        if (markData == null)
        {
            LogUtil.log("查询数据为null");
            return dataBean;
        }
        if (markData.puzzleId == 0)
        {
            LogUtil.log("标记ID为NULL");
            return dataBean;
        }
        long puzzleId = markData.puzzleId;
        int puzzleType = (int)markData.puzzleType;

        string saveKeyMark = DataStorageKeyMarks.Jigsaw_Unlock_State + "_" + puzzleType + "_" + puzzleId;
        string unlockKey = saveKeyMark + "_" + Unlock_Key;
        JigsawUnlockEnum unlockState = (JigsawUnlockEnum)PlayerPrefsUtil.getIntData(unlockKey);

        dataBean.puzzleId = puzzleId;
        dataBean.puzzleType = markData.puzzleType;
        dataBean.unlockState = unlockState;

        return dataBean;
    }

    public List<JigsawUnlockStateBean> getListData(List<JigsawUnlockStateBean> listMarkId)
    {
        if (listMarkId == null || listMarkId.Count == 0)
        {
            LogUtil.log("保存失败，列表数据为NULL");
            return null;
        }
        List<JigsawUnlockStateBean> listData = new List<JigsawUnlockStateBean>();
        int listMarkSize = listMarkId.Count;
        for (int i = 0; i < listMarkSize; i++)
        {
            JigsawUnlockStateBean itemData = getData(listMarkId[i]);
            listData.Add(itemData);
        }
        return listData;
    }

    public void saveData(JigsawUnlockStateBean data)
    {
        if (data == null || data.puzzleId == 0)
        {
            LogUtil.log("保存失败，缺少数据或ID为NULL");
            return;
        }
        long puzzleId = data.puzzleId;
        int puzzleType = (int)data.puzzleType;
        int unlockState = (int)data.unlockState;

        string saveKeyMark = DataStorageKeyMarks.Jigsaw_Unlock_State + "_" + puzzleType + "_" + puzzleId;

        string unlockKey = saveKeyMark + "_" + Unlock_Key;
        PlayerPrefsUtil.setIntData(unlockKey, unlockState);
    }

    public void saveListData(List<JigsawUnlockStateBean> listData)
    {
        if (listData == null || listData.Count == 0)
        {
            LogUtil.log("保存失败，列表数据为NULL");
            return;
        }
        int listDataSize = listData.Count;
        for (int i = 0; i < listDataSize; i++)
        {
            saveData(listData[i]);
        }
    }

}

