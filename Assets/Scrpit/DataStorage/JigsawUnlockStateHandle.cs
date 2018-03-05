using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class JigsawUnlockStateHandle : BaseDataStorageHandle<JigsawUnlockStateBean>, IBaseDataStorage<List<JigsawUnlockStateBean>, List<JigsawUnlockStateBean>>
{

    private const string File_Name = "JigsawUnlockData";

    private static IBaseDataStorage<List<JigsawUnlockStateBean>, List<JigsawUnlockStateBean>> handle;

    public static IBaseDataStorage<List<JigsawUnlockStateBean>, List<JigsawUnlockStateBean>> getInstance()
    {
        if (handle == null)
        {
            handle = new JigsawUnlockStateHandle();
        }
        return handle;
    }

    public List<JigsawUnlockStateBean> getAllData()
    {
        return startLoadDataForList(File_Name);
    }

    public void saveAllData(List<JigsawUnlockStateBean> data)
    {
        if (data == null || data.Count == 0)
        {
            LogUtil.log("保存失败-没有数据");
            return;
        }
        startSaveData(File_Name, data);
    }

}

