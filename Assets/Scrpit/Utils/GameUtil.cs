using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

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
        if (completeStateBean == null)
            completeStateBean = new PuzzlesCompleteStateBean();
        List<PuzzlesCompleteStateBean> listCompleteState = DataStorageManage.getPuzzlesCompleteDSHandle().getAllData();

        if (listCompleteState == null|| listCompleteState.Count==0)
        {
            listCompleteState = new List<PuzzlesCompleteStateBean>();
            completeStateBean.puzzleId = puzzlesInfo.id;
            completeStateBean.puzzleType = puzzlesInfo.data_type;
            completeStateBean.puzzleName = puzzlesInfo.name;
            completeStateBean.puzzleMarkName = puzzlesInfo.mark_file_name;
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
                bool isThisPuzzles = false;
                if (itemCompleteBean.puzzleType.Equals((int)JigsawResourcesEnum.Custom))
                {
                    if (itemCompleteBean.puzzleMarkName.Equals(puzzlesInfo.mark_file_name))
                    {
                        isThisPuzzles = true;
                    }
                }
                else
                {
                    if (itemCompleteBean.puzzleId.Equals(puzzlesInfo.Id)) {
                        isThisPuzzles = true;
                    }
                }
                if (isThisPuzzles)
                {
                    hasData = true;
                    if (itemCompleteBean.completeTime.totalSeconds != 0
                        &&!TimeUtil.isFasterTime(itemCompleteBean.completeTime, completeTime))
                    {
                        //存时间更快的
                    }
                    else
                    {
                        itemCompleteBean.puzzleId = puzzlesInfo.id;
                        itemCompleteBean.puzzleType = puzzlesInfo.data_type;
                        completeStateBean.puzzleName = puzzlesInfo.name;
                        completeStateBean.puzzleMarkName = puzzlesInfo.mark_file_name;
                        itemCompleteBean.unlockState = JigsawUnlockEnum.UnLock;
                        itemCompleteBean.completeTime = completeTime;
                        completeStateBean = itemCompleteBean;
                    }
                    break;
                }
            }
            if (!hasData)
            {
                completeStateBean.puzzleId = puzzlesInfo.id;
                completeStateBean.puzzleType = puzzlesInfo.data_type;
                completeStateBean.puzzleName = puzzlesInfo.name;
                completeStateBean.puzzleMarkName = puzzlesInfo.mark_file_name;
                completeStateBean.completeTime = completeTime;
                completeStateBean.unlockState = JigsawUnlockEnum.UnLock;
                listCompleteState.Add(completeStateBean);
            }
        }
        DataStorageManage.getPuzzlesCompleteDSHandle().saveAllData(listCompleteState);
    }

    /// <summary>
    /// 自动完成拼图
    /// </summary>
    public static void CompletePuzzles(BaseMonoBehaviour content)
    {
        CommonData.GameStatus = 3;
        CommonData.IsCheating = true;
        JigsawContainerCpt[] cptList = UnityEngine.Object.FindObjectsOfType<JigsawContainerCpt>();
        if (cptList == null || cptList.Length == 0)
            return;
        //设置不可在拖拽
        CommonData.IsDargMove = false;
        JigsawContainerCpt tempCpt = cptList[0];

        content.StartCoroutine(delayComplete(content,cptList, tempCpt));
    }

    static IEnumerator delayComplete(BaseMonoBehaviour content,JigsawContainerCpt[] cptList, JigsawContainerCpt tempCpt)
    {
        float mergeTime = 10f;
        Rigidbody2D itemRB = tempCpt.GetComponent<Rigidbody2D>();
        if (itemRB != null)
        {
            //设置质量为0 防止动画时错位
            itemRB.velocity = Vector3.zero;
            //顺便冻结缸体
            itemRB.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        CompositeCollider2D itemCollider = tempCpt.GetComponent<CompositeCollider2D>();
        UnityEngine.Object.Destroy(itemCollider);
        yield return new WaitForEndOfFrame();

        for (int i = 0; i < cptList.Length; i++)
        {
            tempCpt.transform.position = tempCpt.startPosition;
            tempCpt.transform.localRotation = tempCpt.startRotation;
            JigsawContainerCpt itemCpt = cptList[i];
            itemCpt.isSelect = false;
            // 添加拼图碎片到容器里
            if (i > 0)
            {
                tempCpt.addJigsawList(itemCpt.listJigsaw);
                //位置纠正
                tempCpt.jigsawLocationCorrect(mergeTime, itemCpt.listJigsaw);
                // 最后删除当前容器
                UnityEngine.Object.Destroy(itemCpt.gameObject);
            }
            yield return new WaitForSeconds(0.1f);
        }
        tempCpt.mergeDeal(mergeTime);
    }


    /// <summary>
    /// 根据秒获取具体时间
    /// </summary>
    /// <param name="score"></param>
    /// <returns></returns>
    public static string GetTimeStr(int score)
    {
        TimeSpan timeSpan = new TimeSpan(0, 0, score);
        return
            timeSpan.Hours + CommonData.getText(24) + " " +
            timeSpan.Minutes + CommonData.getText(25) + " " +
            timeSpan.Seconds + CommonData.getText(26) + " ";
    }
}

