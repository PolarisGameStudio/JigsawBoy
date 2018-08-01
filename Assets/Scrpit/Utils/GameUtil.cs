using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class GameUtil
{
    /// <summary>
    /// 记录完成时间
    /// </summary>
    /// <param name="selectItem"></param>
    /// <param name="completeTime"></param>
    public static void FinshSaveCompleteData(PuzzlesGameInfoBean selectItem, TimeBean completeTime)
    {
        PuzzlesInfoBean puzzlesInfo = selectItem.puzzlesInfo;
        PuzzlesCompleteStateBean completeStateBean = selectItem.completeStateInfo;

        List<PuzzlesCompleteStateBean> listCompleteState = DataStorageManage.getPuzzlesCompleteDSHandle().getAllData();

        if (listCompleteState == null|| listCompleteState.Count==0)
        {
            listCompleteState = new List<PuzzlesCompleteStateBean>();
            completeStateBean.puzzleId = puzzlesInfo.Id;
            completeStateBean.puzzleType = puzzlesInfo.Data_type;
            completeStateBean.puzzleName = puzzlesInfo.mark_file_name;
            completeStateBean.completeTime = completeTime;
            completeStateBean.unlockState = JigsawUnlockEnum.UnLock;
            listCompleteState.Add(completeStateBean);
        }
        else
        {
            int listCompleteSize = listCompleteState.Count;
            bool hasData = false;
            for(int i = 0; i < listCompleteSize; i++)
            {
                PuzzlesCompleteStateBean itemCompleteBean= listCompleteState[i];
                if (itemCompleteBean.puzzleId.Equals(puzzlesInfo.Id))
                {
                    hasData = true;
                    if (itemCompleteBean.completeTime.totalSeconds != 0
                        &&!TimeUtil.isFasterTime(itemCompleteBean.completeTime, completeTime))
                    {
                        //存时间更快的
                    }
                    else
                    {
                        itemCompleteBean.puzzleId = puzzlesInfo.Id;
                        itemCompleteBean.puzzleType = puzzlesInfo.Data_type;
                        itemCompleteBean.puzzleName = puzzlesInfo.mark_file_name;
                        itemCompleteBean.unlockState = JigsawUnlockEnum.UnLock;
                        itemCompleteBean.completeTime = completeTime;
                        completeStateBean = itemCompleteBean;
                    }
                    break;
                }
            }
            if (!hasData)
            {
                completeStateBean.puzzleId = puzzlesInfo.Id;
                completeStateBean.puzzleType = puzzlesInfo.Data_type;
                completeStateBean.puzzleName = puzzlesInfo.mark_file_name;
                completeStateBean.completeTime = completeTime;
                completeStateBean.unlockState = JigsawUnlockEnum.UnLock;
                listCompleteState.Add(completeStateBean);
            }
        }
        DataStorageManage.getPuzzlesCompleteDSHandle().saveAllData(listCompleteState);
    }
}

