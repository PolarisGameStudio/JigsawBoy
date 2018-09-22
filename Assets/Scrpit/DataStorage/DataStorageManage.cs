using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class DataStorageManage
{
    /// <summary>
    /// 拼图数据完成情况
    /// </summary>
    /// <returns></returns>
    public static IBaseDataStorage<PuzzlesCompleteStateBean, long> getPuzzlesCompleteDSHandle()
    {
        return PuzzlesCompleteDSHandle.getInstance();
    }

    /// <summary>
    /// 游戏配置信息
    /// </summary>
    /// <returns></returns>
    public static IBaseDataStorage<GameConfigureBean, long> getGameConfigureDSHandle()
    {
        return GameConfigureDSHandle.getInstance();
    }

    /// <summary>
    /// 游戏数据
    /// </summary>
    /// <returns></returns>
    public static IBaseDataStorage<UserInfoBean, long> getUserInfoDSHandle()
    {
        return UserInfoDSHandle.getInstance();
    }

    /// <summary>
    /// 自定义拼图
    /// </summary>
    /// <returns></returns>
    public static IBaseDataStorage<PuzzlesInfoBean, long> getCustomPuzzlesInfoDSHandle()
    {
        return CustomPuzzlesInfoDSHandle.getInstance();
    }

    /// <summary>
    /// 拼图进度
    /// </summary>
    /// <returns></returns>
    public static IBaseDataStorage<PuzzlesProgressBean, PuzzlesProgressBean> getPuzzlesProgressDSHandle()
    {
        return PuzzlesProgressDSHandle.getInstance();
    }
}

