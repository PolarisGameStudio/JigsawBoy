using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class LeaderBoardUtil 
{

    /// <summary>
    /// 创建升序秒 排行榜
    /// </summary>
    /// <param name="leaderBoardName"></param>
    public static void CreateLeaderBoard(string leaderBoardName) {
        LeaderboardHandleImpl handleImpl = new LeaderboardHandleImpl();
        handleImpl.findOrCreateLeaderboardForTimeSeconds(leaderBoardName);
    }

    /// <summary>
    /// 创建一个类型的 排行榜
    /// </summary>
    /// <param name="type"></param>
    public static void CreateLeaderBoardByPuzzlesType(JigsawResourcesEnum type)
    {
        List<PuzzlesInfoBean> listData= PuzzlesInfoManager.LoadAllPuzzlesDataByType(type);
        foreach (PuzzlesInfoBean itemData in listData) {
            CreateLeaderBoard(itemData.Id+"_"+itemData.Mark_file_name);
        }
    }

    /// <summary>
    /// 通过ID创建排行榜
    /// </summary>
    /// <param name="id"></param>
    public static void CreateLeaderBoardById(long id)
    {
        List<PuzzlesInfoBean> listData = PuzzlesInfoManager.LoadBasePuzzlesDataById(id);
        foreach (PuzzlesInfoBean itemData in listData)
        {
            CreateLeaderBoard(itemData.Id + "_" + itemData.Mark_file_name);
        }
    }
   
}