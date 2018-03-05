using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class PuzzlesCompleteDSHandle : BaseDataStorageHandle<PuzzlesCompleteStateBean>, IBaseDataStorage<List<PuzzlesCompleteStateBean>, long>
{

    private const string File_Name = "PuzzlesCompleteDS";

    private static IBaseDataStorage<List<PuzzlesCompleteStateBean>, long> handle;

    public static IBaseDataStorage<List<PuzzlesCompleteStateBean>, long> getInstance()
    {
        if (handle == null)
        {
            handle = new PuzzlesCompleteDSHandle();
        }
        return handle;
    }

    public List<PuzzlesCompleteStateBean> getAllData()
    {
        return startLoadDataForList(File_Name);
    }

    public void saveAllData(List<PuzzlesCompleteStateBean> data)
    {
        if (data == null || data.Count == 0)
        {
            LogUtil.log("保存失败-没有数据");
            return;
        }
        startSaveData(File_Name, data);
    }

}

