using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

public class GameStartDecomposeRotate : BaseGameStartAnimation
{
    //移动时间
    public float moveTime;
    //旋转时间
    public float roatateTime;
    //起始位置
    public Vector3 startPosition;

    public GameStartDecomposeRotate(List<GameObject> listObj, GameStartControl gameStartControl) : base(listObj, gameStartControl)
    {
        moveTime = 3f;
        roatateTime = 3f;
        startPosition = new Vector3(0, 0, 0);
    }

    public override void startAnim()
    {
        decomposeAnim();
    }

    private void decomposeAnim()
    {
        if (mListObj == null)
            return;
        int listCount = mListObj.Count;

        float radius = 0;
        if (mGameStartControl == null)
            return;

        if (mGameStartControl.picAllWith > mGameStartControl.picAllHigh)
            radius = mGameStartControl.picAllHigh;
        else
            radius = mGameStartControl.picAllWith;

        List<Vector3> listCircleVec = GeometryUtil.getCircleVertices(startPosition, radius * 1.9f, listCount, true, CircleStartVectorEnum.Left);
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
                 .DOMove(listCircleVec[i], moveTime)
                 .SetDelay(mPrependTime);
            if (i.Equals(listCount - 1))
            {
                tweener.OnComplete(rotateAnim);
            }
        }
    }

    /// <summary>
    /// 旋转动画
    /// </summary>
    private void rotateAnim()
    {
        int listCount = mListObj.Count;
        float radius = 0;
        if (mGameStartControl.picAllWith > mGameStartControl.picAllHigh)
            radius = mGameStartControl.picAllHigh;
        else
            radius = mGameStartControl.picAllWith;

        List<Vector3> listCircleVec = GeometryUtil.getCircleVertices(startPosition, radius * 1.9f, listCount, true, CircleStartVectorEnum.Left);

        for (int i = 0; i < listCount; i++)
        {
            GameObject itemObj = mListObj[i];
            Transform itemTF = itemObj.transform;

            Vector3[] arrayCircleVec = DevUtil.listToArrayFormPosition(listCircleVec, i);
            itemTF.DOPath(arrayCircleVec, roatateTime).OnComplete(delegate ()
            {
                GameStartAnimationManager.PuzzlesStartPre(itemObj);
            });
        }

        Tweener gameStartTweener = mGameStartControl
            .transform
            .DOScale(new Vector3(1, 1, 1), roatateTime)
            .OnComplete(delegate ()
                 {
                  mGameStartControl.gameStart();
                 });

    }
}