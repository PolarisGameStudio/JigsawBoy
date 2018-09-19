using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class PuzzlesProgressDSHandle : BaseDataStorageHandle<PuzzlesProgressBean>, IBaseDataStorage<PuzzlesProgressBean, PuzzlesProgressBean>
{
    private const string File_Name = "PuzzlesProgressDS";

    private static IBaseDataStorage<PuzzlesProgressBean, PuzzlesProgressBean> handle;

    public static IBaseDataStorage<PuzzlesProgressBean, PuzzlesProgressBean> getInstance()
    {
        if (handle == null)
        {
            handle = new PuzzlesProgressDSHandle();
        }
        return handle;
    }

    public List<PuzzlesProgressBean> getAllData()
    {
        return startLoadDataForList(File_Name);
    }

    public PuzzlesProgressBean getData(PuzzlesProgressBean paramsData)
    {
        PuzzlesProgressBean data = null;
        long puzzlesId = paramsData.puzzleId;
        string markFileName = paramsData.markFileName;
        if (markFileName == null) {
            LogUtil.log("查询进度失败，缺少markFileName");
            return data;
        }
        List<PuzzlesProgressBean> allData = getAllData();
        if (allData == null)
            return data;
        foreach (PuzzlesProgressBean itemData in allData)
        {
            if (itemData.puzzleId.Equals(puzzlesId) && itemData.markFileName.Equals(markFileName))
            {
                data = itemData;
                break;
            }
        }
        return data;
    }


    public void saveAllData(List<PuzzlesProgressBean> data)
    {
        if (data == null)
        {
            LogUtil.log("保存失败-没有数据");
            return;
        }
        startSaveDataForList(File_Name, data);
    }

    public void saveData(PuzzlesProgressBean data)
    {
        if (data == null || data.puzzleId == 0)
        {
            LogUtil.log("保存失败-没有数据或没有拼图ID");
            return;
        }
        List<PuzzlesProgressBean> oldAllData = getAllData();
        //如果之前没有数据直接存储
        if (oldAllData == null || oldAllData.Count == 0)
        {
            oldAllData = new List<PuzzlesProgressBean>();
            oldAllData.Add(data);
            startSaveDataForList(File_Name, oldAllData);
            return;
        }
        //如果有数据则遍历之前看是否有相同
        int hasDataBefore = -1;
        for (int i = 0; i < oldAllData.Count; i++)
        {
            if (oldAllData[i].puzzleId.Equals(data.puzzleId))
            {
                hasDataBefore = i;
                break;
            }
        }
        if (hasDataBefore.Equals(-1))
        {
            oldAllData.Add(data);
            saveAllData(oldAllData);
        }
        else
        {
            oldAllData[hasDataBefore] = data;
            saveAllData(oldAllData);
        }
    }

    public void deleteData(PuzzlesProgressBean paramsData)
    {
        long puzzlesId = paramsData.puzzleId;
        string markFileName = paramsData.markFileName;
        if (markFileName == null)
        {
            LogUtil.log("查询进度失败，缺少markFileName");
            return;
        }
        List<PuzzlesProgressBean> listData = getAllData();
        if (listData != null && listData.Count > 0)
        {
            PuzzlesProgressBean removeData=null;
            foreach (PuzzlesProgressBean item in listData)
            {
                if (item.puzzleId.Equals(puzzlesId)&&item.markFileName.Equals(markFileName)) {
                    removeData = item;
                    break;
                }          
            }
            if (removeData != null) {
                listData.Remove(removeData);
                saveAllData(listData);
            }
        }
    }
}