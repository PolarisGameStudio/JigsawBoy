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


    public List<UserInfoBean> getAllData()
    {
        throw new NotImplementedException();
    }

    public UserInfoBean getData(long data)
    {
        return startLoadData(File_Name);
    }

    public void saveAllData(List<UserInfoBean> data)
    {
        throw new NotImplementedException();
    }

    public void saveData(UserInfoBean data)
    {
        if (data == null)
        {
            LogUtil.log("保存失败-没有数据");
            return;
        }
        startSaveData(File_Name, data);
    }

    List<UserInfoBean> IBaseDataStorage<UserInfoBean, long>.getAllData()
    {
        throw new NotImplementedException();
    }
}

