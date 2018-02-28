using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class DataStorageManage
{
    public static BaseDataStorageHandle<JigsawUnlockStateBean, JigsawUnlockStateBean> getJigsawUnlockStateHandle()
    {
        return JigsawUnlockStateHandle.getInstance();
    }
}

