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

            PuzzlesCompleteStateBean tempCompleteBean = new PuzzlesCompleteStateBean();
            tempCompleteBean.puzzleId = puzzlesInfo.Id;
            tempCompleteBean.completeTime = completeTime;
            tempCompleteBean.unlockState = JigsawUnlockEnum.UnLock;
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
                    //存时间更快的
                    if(TimeUtil.isFasterTime(itemCompleteBean.completeTime, completeTime))
                    {
                        itemCompleteBean.unlockState = JigsawUnlockEnum.UnLock;
                        itemCompleteBean.completeTime = completeTime;
                    }
                    break;
                }
            }
            if (!hasData)
            {
                PuzzlesCompleteStateBean tempCompleteBean = new PuzzlesCompleteStateBean();
                tempCompleteBean.puzzleId = puzzlesInfo.Id;
                tempCompleteBean.completeTime = completeTime;
                tempCompleteBean.unlockState = JigsawUnlockEnum.UnLock;
                listCompleteState.Add(completeStateBean);
            }
        }
        DataStorageManage.getPuzzlesCompleteDSHandle().saveAllData(listCompleteState);
    }
}

