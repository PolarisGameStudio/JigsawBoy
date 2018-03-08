using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class BGMInfoManager
{

    /// <summary>
    /// 查询所有BGM信息
    /// </summary>
    /// <returns></returns>
    public static List<BGMInfoBean> LoadAllBGMInfo()
    {
        List<BGMInfoBean> listData = new List<BGMInfoBean>();
        listData = SQliteHandle.LoadTableData<BGMInfoBean>
          (
          CommonDB.PuzzleInfoDB_Name,
          CommonDB.PuzzleInfoDB_BGMInfo_Table
          );
        return listData;
    }


    /// <summary>
    /// 查询指定BGM信息
    /// </summary>
    /// <param name="bGMEnum"></param>
    /// <returns></returns>
    public static List<BGMInfoBean> LoadBGMInfo(long bgmId)
    {
        List<BGMInfoBean> listData = new List<BGMInfoBean>();
        listData = SQliteHandle.LoadTableData<BGMInfoBean>
          (
          CommonDB.PuzzleInfoDB_Name,
          CommonDB.PuzzleInfoDB_BGMInfo_Table,
          new string[]{ " id "},
          new string[]{ " = "},
          new string[]{ bgmId +" "}
          );
        return listData;
    }
}

