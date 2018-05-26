using System;
using System.Collections.Generic;

public class CustomPuzzlesInfoDSHandle : BaseDataStorageHandle<PuzzlesInfoBean>, IBaseDataStorage<PuzzlesInfoBean, long>
{

    private const string File_Name = "CustomPuzzlesInfo";

    private static IBaseDataStorage<PuzzlesInfoBean, long> handle;

    public static IBaseDataStorage<PuzzlesInfoBean, long> getInstance()
    {
        if (handle == null)
        {
            handle = new CustomPuzzlesInfoDSHandle();
        }
        return handle;
    }

    /// <summary>
    /// 获取所有数据
    /// </summary>
    /// <returns></returns>
    public List<PuzzlesInfoBean> getAllData()
    {
        return startLoadDataForList(File_Name);
    }

    /// <summary>
    /// 保存所有数据
    /// </summary>
    /// <param name="data"></param>
    public void saveAllData(List<PuzzlesInfoBean> data)
    {
        if (data == null)
        {
            LogUtil.log("保存失败-没有数据");
            return;
        }
        startSaveDataForList(File_Name, data);
    }

    /// <summary>
    /// 删除其中一项数据
    /// </summary>
    /// <param name="infoBean"></param>
    public void removeData(PuzzlesInfoBean infoBean)
    {
        List<PuzzlesInfoBean> allData = getAllData();
        if (allData != null)
        {
            for (int i = 0; i < allData.Count; i++)
            {
                if (allData[i].Mark_file_name.Equals(infoBean.Mark_file_name))
                    allData.Remove(allData[i]);
            }
            saveAllData(allData);
        }
    }

    /// <summary>
    /// 修改数据
    /// </summary>
    /// <param name="oldInfoBean"></param>
    internal void changeData(PuzzlesInfoBean oldInfoBean)
    {
        List<PuzzlesInfoBean> allData = getAllData();
        if (allData != null)
        {
            for (int i = 0; i < allData.Count; i++)
            {
                if (allData[i].Mark_file_name.Equals(oldInfoBean.Mark_file_name))
                    allData[i] = oldInfoBean;
            }
            saveAllData(allData);
        }
    }

    public void saveData(PuzzlesInfoBean data)
    {
        throw new NotImplementedException();
    }

    public PuzzlesInfoBean getData(long data)
    {
        throw new NotImplementedException();
    }
}

