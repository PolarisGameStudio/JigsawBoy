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
    public static IBaseDataStorage<List<PuzzlesCompleteStateBean>, long> getPuzzlesCompleteDSHandle()
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
}

