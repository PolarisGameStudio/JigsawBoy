using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class UserInfoDSHandle : BaseDataStorageHandle<UserInfoBean>, IBaseDataStorage<UserInfoBean, long>
{
    private const string File_Name = "UserInfoDSHandle";

    private static IBaseDataStorage<UserInfoBean, long> handle;

    public static IBaseDataStorage<UserInfoBean, long> getInstance()
    {
        if (handle == null)
        {
            handle = new UserInfoDSHandle();
        }
        return handle;
    }


    public UserInfoBean getAllData()
    {
        return startLoadData(File_Name);
    }

    public void saveAllData(UserInfoBean data)
    {
        if (data == null)
        {
            LogUtil.log("保存失败-没有数据");
            return;
        }
        startSaveData(File_Name, data);
    }
}

