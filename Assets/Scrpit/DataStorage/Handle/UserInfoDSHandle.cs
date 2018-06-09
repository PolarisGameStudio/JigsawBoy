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



    public UserInfoBean getData(long data)
    {
        UserInfoBean userInfo = startLoadData(File_Name);
        if (userInfo == null)
            userInfo = new UserInfoBean();
        return userInfo;
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

    /// <summary>
    /// 增加用户拼图点数
    /// </summary>
    /// <param name="puzzlesPoint"></param>
    public void increaseUserPuzzlesPoint(long puzzlesPoint)
    {
        UserInfoBean userInfo = getData(0);
        userInfo.puzzlesPoint += puzzlesPoint;
        saveData(userInfo);
    }

    /// <summary>
    /// 减少用户拼图点数
    /// </summary>
    /// <param name="puzzlesPoint"></param>
    public void decreaseUserPuzzlesPoint(long puzzlesPoint)
    {
        UserInfoBean userInfo = getData(0);
        userInfo.puzzlesPoint -= puzzlesPoint;
        saveData(userInfo);
    }
    List<UserInfoBean> IBaseDataStorage<UserInfoBean, long>.getAllData()
    {
        throw new NotImplementedException();
    }

    public List<UserInfoBean> getAllData()
    {
        throw new NotImplementedException();
    }

    public void saveAllData(List<UserInfoBean> data)
    {
        throw new NotImplementedException();
    }
}

