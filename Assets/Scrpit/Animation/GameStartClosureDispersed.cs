using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using DG.Tweening;

public class GameStartClosureDispersed : BaseGameStartAnimation
{
    //起始位置
    public Vector3 startPosition;
    //聚拢时间
    public float closeureTime;
    //分散动画执行间隔
    public float dispersedOffsetTime;
    //执行间距
    public float animOffsetTime;

    public GameStartClosureDispersed(List<GameObject> listObj, GameStartControl gameStartControl) : base(listObj, gameStartControl)
    {
        startPosition = new Vector3(0, 0, 0);
        closeureTime = 0.5f;
        animOffsetTime = 0.01f;
        dispersedOffsetTime = 0.1f;
    }

    public override void startAnim()
    {
        closureAnim();
    }

    /// <summary>
    /// 靠拢动画
    /// </summary>
    private void closureAnim()
    {
        int listCount = mListObj.Count;
        for (int i = 0; i < listCount; i++)
        {
            GameObject itemObj = mListObj[i];
            Transform itemTF = itemObj.transform;

            //设置层级
            JigsawContainerCpt containerCpt = itemTF.GetComponent<JigsawContainerCpt>();
            if (containerCpt == null)
                continue;
            containerCpt.setSortingOrder(listCount - i);
            //设置动画
            Tweener tweener = itemTF
                 .DOMove(startPosition, closeureTime)
                 .SetDelay(mPrependTime + animOffsetTime * i);
            if (i.Equals(listCount - 1))
            {
                tweener.OnComplete(dispersedAnim);
            }
        }
    }

    /// <summary>
    /// 分散动画
    /// </summary>
    private void dispersedAnim()
    {
        int listCount = mListObj.Count;
        for (int i = 0; i < listCount; i++)
        {
            GameObject itemObj = mListObj[i];
            Transform itemTF = itemObj.transform;

            //设置动画
            Tweener tweener = itemTF
                 .DOMove(startPosition, dispersedOffsetTime)
                 .SetDelay(animOffsetTime * i)
                 .OnComplete(delegate ()
                 {
                     GameStartAnimationManager.PuzzlesStartPre(itemObj);
                 });
        }
        Tweener gameStartTweener = mGameStartControl.transform
            .DOScale(new Vector3(1, 1, 1), dispersedOffsetTime + listCount * animOffsetTime)
            .OnComplete(delegate ()
             {
                 mGameStartControl.gameStart();
             });
    }


}

