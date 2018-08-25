using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using DG.Tweening;

public class GameStartCircularRun : BaseGameStartAnimation
{
    //起始位置
    private Vector3 mStartPosition;

    private float mRunTime;
    private int mListMoveCount;


    public GameStartCircularRun(List<GameObject> listObj, GameStartControl startControl) : base(listObj, startControl)
    {
        mStartPosition = new Vector3(0,0,0);
        mRunTime = 6f;
        mListMoveCount = 10;
    }

    public override void startAnim()
    {
        float radius = 0;
        if (mGameStartControl.picAllWith > mGameStartControl.picAllHigh)
            radius = mGameStartControl.picAllHigh;
        else
            radius = mGameStartControl.picAllWith;

        int listCount = mListObj.Count;
        List<Vector3> listCircleVec = GeometryUtil.getCircleVertices(mStartPosition, radius * 1.9f, listCount, true, CircleStartVectorEnum.Left);
        int circleCount = listCircleVec.Count;
        for (int i = 0; i < listCount; i++) {
            GameObject itemObj= mListObj[i];
            //设置层级
            JigsawContainerCpt containerCpt = itemObj.transform.GetComponent<JigsawContainerCpt>();
            if (containerCpt == null)
                continue;
            containerCpt.setSortingOrder(listCount - i);

            //  int mListMoveCount=  DevUtil.getRandomInt(1, circleCount);
            Vector3[] listMove = new Vector3[mListMoveCount];
            for (int f = 0; f < mListMoveCount; f++) {
                int randomPosition = DevUtil.getRandomInt(0, circleCount-1);
                listMove[f]=listCircleVec[randomPosition];
            }
            //开始run
            itemObj.transform
                .DOPath(listMove, mRunTime)
                .SetDelay(mPrependTime)
                .OnComplete(delegate() {
                    GameStartAnimationManager.PuzzlesStartPre(itemObj);
                });
        }

        Tweener gameStartTweener = mGameStartControl.transform
                                         .DOScale(new Vector3(1, 1, 1), mPrependTime + mRunTime)
                                         .OnComplete(delegate ()
                                         {
                                             mGameStartControl.gameStart();
                                         });
    }
}