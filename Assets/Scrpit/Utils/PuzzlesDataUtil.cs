using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class PuzzlesDataUtil
{
    public static List<PuzzlesGameInfoBean> MergePuzzlesInfoAndCompleteState(List<PuzzlesInfoBean> listInfo,List<PuzzlesCompleteStateBean> listCompleteState)
    {
        List<PuzzlesGameInfoBean> listData = new List<PuzzlesGameInfoBean>();
        if (listInfo == null)
        {
            LogUtil.log("合并拼图数据和完成数据失败-没有原始数据");
            return listData;
        }
        int listInfoSize = listInfo.Count;

        for(int i = 0; i < listInfoSize; i++)
        {
            PuzzlesGameInfoBean itemData = new PuzzlesGameInfoBean();
            PuzzlesInfoBean tempInfo = listInfo[i];
            itemData.puzzlesInfo = tempInfo;

            if (listCompleteState != null)
            {
                int listCompleteStateSize = listCompleteState.Count;
                for(int f=0;f< listCompleteStateSize; f++)
                {
                    PuzzlesCompleteStateBean tempCompleteState = listCompleteState[f];
                    if (tempInfo.Id.Equals(tempCompleteState.puzzleId) && tempInfo.Data_type.Equals(tempCompleteState.puzzleType))
                    {
                        itemData.completeStateInfo = tempCompleteState;
                    }
                }
            }
                
            listData.Add(itemData);
        }
        return listData;
    }
}

