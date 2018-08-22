using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using DG.Tweening;

public class GameStartFunnel : BaseGameStartAnimation
{

    private float mBuildFunnelTime;//创建漏斗时间
    private float mMoveToMouthTime;//移动到发射口时间
    private float mLaunchOffTime;//发射延迟时间
    public GameStartFunnel(List<GameObject> listObj, GameStartControl startControl) : base(listObj, startControl)
    {
        mBuildFunnelTime = 2f;
        mLaunchOffTime = 0.02f;
        mMoveToMouthTime = 1f;
    }

    public override void startAnim()
    {
        buildFunnel();
    }
    /// <summary>
    /// 创建漏斗
    /// </summary>
    private void buildFunnel()
    {
        int listCount = mListObj.Count;
        for (int i = 0; i < listCount; i++)
        {
            GameObject itemObj = mListObj[i];
            //设置层级
            JigsawContainerCpt containerCpt = itemObj.GetComponent<JigsawContainerCpt>();
            if (containerCpt == null)
                continue;
            containerCpt.setSortingOrder(listCount - i);

            float picW = mGameStartControl.picAllWith;
            float picH = mGameStartControl.picAllHigh;

            float moveX = DevUtil.getRandomFloat(-picW / 2f, picW / 2f);
            float rangY = (picH / 2f) / ((picW / 2f) / ((picW / 2f) - (Mathf.Abs(moveX))));
            float moveY = DevUtil.getRandomFloat((picH / 2f) - rangY, picH / 2f);
            itemObj
                .transform
                .DOMove(new Vector3(moveX, moveY), mBuildFunnelTime)
                .SetDelay(mPrependTime);
        }
        mGameStartControl.transform
           .DOScale(new Vector3(1, 1, 1), mPrependTime + mBuildFunnelTime)
           .OnComplete(delegate ()
             {
                 moveToMouth();
             });
    }

    /// <summary>
    /// 移动到发射口
    /// </summary>
    private void moveToMouth()
    {
        int listCount = mListObj.Count;
        for (int i = 0; i < listCount; i++)
        {
            GameObject itemObj = mListObj[i];
            itemObj
               .transform
               .DOMove(new Vector3(0, 0), mMoveToMouthTime)
               .SetDelay(mLaunchOffTime * i)
               .OnComplete(delegate ()
               {
                   int xF = DevUtil.getRandomInt(-10, 10);
                   int yF = DevUtil.getRandomInt(-100, -10);
                   GameStartAnimationManager.PuzzlesStartPre(itemObj, xF, yF);
               });
        }
        mGameStartControl.transform
             .DOScale(new Vector3(1, 1, 1), mLaunchOffTime * listCount)
             .OnComplete(delegate ()
                {
                     mGameStartControl.gameStart();
                });
    }
}