using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using DG.Tweening;

public class GameStartDispersed : BaseGameStartAnimation
{
    /// <summary>
    /// 动画间隔时间
    /// </summary>
    private float animOffsetTime;

    public GameStartDispersed(List<GameObject> listObj, GameStartControl startControl) : base(listObj, startControl)
    {
        animOffsetTime = 0.1f;
    }

    public override void startAnim()
    {
        dispersedAnim();
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
            //设置层级
            JigsawContainerCpt containerCpt = itemTF.GetComponent<JigsawContainerCpt>();
            if (containerCpt == null)
                continue;
            containerCpt.setSortingOrder(listCount - i);

            //设置动画
            Tweener tweener = itemTF
                 .DOScale(new Vector3(1, 1, 1), animOffsetTime*i)
                 .SetDelay(mPrependTime)
                 .OnComplete(delegate ()
                 {
                     GameStartAnimationManager.PuzzlesStartPre(itemObj);
                 });
        }
        Tweener gameStartTweener = mGameStartControl.transform
            .DOScale(new Vector3(1, 1, 1), mPrependTime + listCount * animOffsetTime)
            .OnComplete(delegate ()
            {
                mGameStartControl.gameStart();
            });
    }
}