using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class PuzzlesCompleteDSHandle : BaseDataStorageHandle<PuzzlesCompleteStateBean>, IBaseDataStorage<PuzzlesCompleteStateBean, long>
{

    private const string File_Name = "PuzzlesCompleteDS";

    private static IBaseDataStorage<PuzzlesCompleteStateBean, long> handle;

    public static IBaseDataStorage<PuzzlesCompleteStateBean, long> getInstance()
    {
        if (handle == null)
        {
            handle = new PuzzlesCompleteDSHandle();
        }
        return handle;
    }

    /// <summary>
    /// 获取所有数据
    /// </summary>
    /// <returns></returns>
    public List<PuzzlesCompleteStateBean> getAllData()
    {
        return startLoadDataForList(File_Name);
    }

    /// <summary>
    /// 通过ID获取完成拼图数据
    /// </summary>
    /// <param name="puzzlesId"></param>
    /// <returns></returns>
    public PuzzlesCompleteStateBean getData(long puzzlesId)
    {
        PuzzlesCompleteStateBean data = null;
        List<PuzzlesCompleteStateBean> allData = getAllData();
        foreach (PuzzlesCompleteStateBean itemData in allData)
        {
            if (itemData.puzzleId.Equals(puzzlesId))
            {
                data = itemData;
                break;
            }
        }
        return data;
    }

    /// <summary>
    /// 通过名字获取完成拼图数据
    /// </summary>
    /// <param name="puzzlesName"></param>
    /// <returns></returns>
    public PuzzlesCompleteStateBean getDataByName(string puzzlesName) {
        PuzzlesCompleteStateBean data = null;
        List<PuzzlesCompleteStateBean> allData= getAllData();
        foreach (PuzzlesCompleteStateBean itemData in allData)
        {
            if (itemData.puzzleName!=null&&itemData.puzzleName.Equals(puzzlesName))
            {
                data = itemData;
                break;
            }
        }
        return data;
    }

    /// <summary>
    /// 保存所有数据
    /// </summary>
    /// <param name="data"></param>
    public void saveAllData(List<PuzzlesCompleteStateBean> data)
    {
        if (data == null || data.Count == 0)
        {
            LogUtil.log("保存失败-没有数据");
            return;
        }
        startSaveDataForList(File_Name, data);
    }

    /// <summary>
    /// 保存数据
    /// </summary>
    /// <param name="data"></param>
    public void saveData(PuzzlesCompleteStateBean data)
    {
        if (data == null || data.puzzleId == 0) {
            LogUtil.log("保存失败-没有数据或没有拼图ID");
            return;
        }
        List<PuzzlesCompleteStateBean> oldAllData = getAllData();
        //如果之前没有数据直接存储
        if (oldAllData == null || oldAllData.Count == 0)
        {
            oldAllData = new List<PuzzlesCompleteStateBean>();
            oldAllData.Add(data);
            startSaveDataForList(File_Name, oldAllData);
            return;
        }
        //如果有数据则遍历之前看是否有相同
        int hasDataBefore = -1;
        for (int i = 0; i < oldAllData.Count; i++) {
            if (oldAllData[i].puzzleId.Equals(data.puzzleId)) {
                hasDataBefore = i;
                break;
            }
        }
        if (hasDataBefore.Equals(-1))
        {
            oldAllData.Add(data);
            saveAllData(oldAllData);
        }
        else {
            oldAllData[hasDataBefore] = data;
            saveAllData(oldAllData);
        }
    }
}

