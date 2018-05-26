using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class GameConfigureDSHandle : BaseDataStorageHandle<GameConfigureBean>, IBaseDataStorage<GameConfigureBean, long>
{
    private const string File_Name = "GameConfigure";

    private static IBaseDataStorage<GameConfigureBean,long> handle;

    public static IBaseDataStorage<GameConfigureBean,long> getInstance()
    {
        if (handle == null)
        {
            handle = new GameConfigureDSHandle();
        }
        return handle;
    }

    public List<GameConfigureBean> getAllData()
    {
        throw new NotImplementedException();
    }

    public GameConfigureBean getData(long data)
    {
        return startLoadData(File_Name);
    }

    public void saveAllData(List<GameConfigureBean> data)
    {
        throw new NotImplementedException();
    }

    public void saveData(GameConfigureBean data)
    {
        if (data == null)
        {
            LogUtil.log("保存失败-没有数据");
            return;
        }
        startSaveData(File_Name, data);
    }

}

