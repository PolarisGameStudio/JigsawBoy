using System;
using System.Collections.Generic;


public class CustomPuzzlesInfoDSHandle : BaseDataStorageHandle<PuzzlesInfoBean>, IBaseDataStorage<List<PuzzlesInfoBean>, long>
{

    private const string File_Name = "CustomPuzzlesInfo";

    private static IBaseDataStorage<List<PuzzlesInfoBean>, long> handle;

    public static IBaseDataStorage<List<PuzzlesInfoBean>, long> getInstance()
    {
        if (handle == null)
        {
            handle = new CustomPuzzlesInfoDSHandle();
        }
        return handle;
    }

    public List<PuzzlesInfoBean> getAllData()
    {
        return startLoadDataForList(File_Name);
    }

    public void saveAllData(List<PuzzlesInfoBean> data)
    {
        if (data == null || data.Count == 0)
        {
            LogUtil.log("保存失败-没有数据");
            return;
        }
        startSaveData(File_Name, data);
    }
}

