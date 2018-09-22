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

        if (listCompleteState == null || listCompleteState.Count == 0)
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
            for (int i = 0; i < listCompleteSize; i++)
            {
                PuzzlesCompleteStateBean itemCompleteBean = listCompleteState[i];
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
                    if (itemCompleteBean.puzzleId.Equals(puzzlesInfo.Id))
                    {
                        isThisPuzzles = true;
                    }
                }
                if (isThisPuzzles)
                {
                    hasData = true;
                    if (itemCompleteBean.completeTime.totalSeconds != 0
                        && !TimeUtil.isFasterTime(itemCompleteBean.completeTime, completeTime))
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
    /// 读取拼图进度
    /// </summary>
    /// <param name="content"></param>
    /// <param name="progressBean"></param>
    public static void setGameProgress(BaseMonoBehaviour content, PuzzlesProgressBean progressBean)
    {
        JigsawContainerCpt[] cptList = UnityEngine.Object.FindObjectsOfType<JigsawContainerCpt>();
        if (cptList == null || cptList.Length == 0)
            return;

        List<PuzzlesProgressItemBean> progress = progressBean.progress;
        foreach (PuzzlesProgressItemBean itemProgress in progress)
        {
            JigsawContainerCpt tempCpt = null;

            //其次需要合并的子对象
            List<JigsawContainerCpt> tempListCpt = new List<JigsawContainerCpt>();
            foreach (JigsawContainerCpt itemCpt in cptList)
            {
                //首先获取父对象
                if (itemCpt.listJigsaw[0].MarkLocation == itemProgress.markPostion)
                {
                    tempCpt = itemCpt;
                }

                foreach (Vector2 listPuzzleItem in itemProgress.listPuzzles)
                {
                    if (listPuzzleItem == itemCpt.listJigsaw[0].MarkLocation)
                    {
                        tempListCpt.Add(itemCpt);
                    }
                }
            }
            JigsawContainerCpt[] tempArraryCpt = DevUtil.listToArray(tempListCpt);
            if (tempCpt != null)
            {
                //设置不可在拖拽
                CommonData.IsDargMove = false;
                content.StartCoroutine(delayComplete(content, tempArraryCpt, tempCpt, 1f));
            }
        }
    }

    /// <summary>
    /// 获取拼图进度
    /// </summary>
    /// <returns></returns>
    public static List<PuzzlesProgressItemBean> getGameProgress()
    {
        JigsawContainerCpt[] cptList = UnityEngine.Object.FindObjectsOfType<JigsawContainerCpt>();
        List<PuzzlesProgressItemBean> progress = new List<PuzzlesProgressItemBean>();
        foreach (JigsawContainerCpt itemCpt in cptList)
        {
            List<JigsawBean> itemListJigsaw = itemCpt.listJigsaw;
            //要子拼图再1个以上时才保存
            if (itemListJigsaw != null && itemListJigsaw.Count > 1)
            {
                PuzzlesProgressItemBean itemProgress = new PuzzlesProgressItemBean();
                List<Vector2> listCenter = new List<Vector2>();
                foreach (JigsawBean itemBean in itemListJigsaw)
                {
                    listCenter.Add(itemBean.MarkLocation);
                }
                itemProgress.markPostion = itemListJigsaw[0].MarkLocation;
                itemProgress.listPuzzles = listCenter;
                progress.Add(itemProgress);
            }
        }
        return progress;
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

        content.StartCoroutine(delayComplete(content, cptList, tempCpt, 10f));
    }

    static IEnumerator delayComplete(BaseMonoBehaviour content, JigsawContainerCpt[] cptList, JigsawContainerCpt tempCpt, float mergeTime)
    {
        foreach (JigsawContainerCpt itemCpt in cptList )
        {
            Rigidbody2D itemRB = itemCpt.GetComponent<Rigidbody2D>();
            if (itemCpt != null)
            {
                //设置质量为0 防止动画时错位
                itemRB.velocity = Vector3.zero;
                //顺便冻结缸体
                itemRB.constraints = RigidbodyConstraints2D.FreezeAll;
            }
            CompositeCollider2D itemCollider = itemCpt.GetComponent<CompositeCollider2D>();
            if (itemCollider != null)
                UnityEngine.Object.Destroy(itemCollider);
        }
        yield return new WaitForEndOfFrame();

        for (int i = 0; i < cptList.Length; i++)
        {
            if (mergeTime > 5)
            {
               tempCpt.transform.position = tempCpt.startPosition;
               tempCpt.transform.localRotation = tempCpt.startRotation;
            }
            JigsawContainerCpt itemCpt = cptList[i];
            itemCpt.isSelect = false;
            // 添加拼图碎片到容器里
            if (cptList[i] != tempCpt)
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

