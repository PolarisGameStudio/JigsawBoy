using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class PuzzlesDataUtil
{
    public static List<PuzzlesGameInfoBean> MergePuzzlesInfo(List<PuzzlesInfoBean> listInfo, List<PuzzlesCompleteStateBean> listCompleteState, List<PuzzlesProgressBean> listProgressInfo)
    {
        List<PuzzlesGameInfoBean> listData = new List<PuzzlesGameInfoBean>();
        if (listInfo == null)
        {
            LogUtil.log("合并拼图数据和完成数据失败-没有原始数据");
            return listData;
        }
        int listInfoSize = listInfo.Count;

        for (int i = 0; i < listInfoSize; i++)
        {
            PuzzlesGameInfoBean itemData = new PuzzlesGameInfoBean();
            PuzzlesInfoBean tempInfo = listInfo[i];
            itemData.puzzlesInfo = tempInfo;

            //合并完成状态
            if (listCompleteState != null)
            {
                int listCompleteStateSize = listCompleteState.Count;
                for (int f = 0; f < listCompleteStateSize; f++)
                {
                    PuzzlesCompleteStateBean tempCompleteState = listCompleteState[f];
                    if (tempInfo.data_type.Equals((int)JigsawResourcesEnum.Custom))
                    {
                        if (tempInfo.mark_file_name.Equals(tempCompleteState.puzzleMarkName) && tempInfo.Data_type.Equals(tempCompleteState.puzzleType))
                        {
                            itemData.completeStateInfo = tempCompleteState;
                        }
                    }
                    else
                    {
                        if (tempInfo.Id.Equals(tempCompleteState.puzzleId) && tempInfo.Data_type.Equals(tempCompleteState.puzzleType))
                        {
                            itemData.completeStateInfo = tempCompleteState;
                        }
                    }
                }

            }
            //合并进度信息
            if (listProgressInfo != null)
            {
                int listProgressSize = listProgressInfo.Count;
                for (int f = 0; f < listProgressSize; f++)
                {
                    PuzzlesProgressBean tempProgressInfo = listProgressInfo[f];
                    if (tempInfo.id.Equals(tempProgressInfo.puzzleId) && tempInfo.mark_file_name.Equals(tempProgressInfo.markFileName))
                    {
                        itemData.progressInfo = tempProgressInfo;
                    }
                }
            }
            listData.Add(itemData);
        }
        return listData;
    }
}

