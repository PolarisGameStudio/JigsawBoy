using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using DG.Tweening;

public class GameStartDecomposeRotate : BaseAnimation
{
    //所有的拼图对象
    private List<GameObject> listObj;
    //准备时间
    public float prependTime;
    //移动时间
    public float moveTime;
    //旋转时间
    public float roatateTime;
    //起始位置
    public Vector3 startPosition;
    //xy方向的分散力大小
    public int xForceMax = 100;
    public int yForceMax = 100;

    //游戏控制器
    public GameStartControl gameStartControl;

    public GameStartDecomposeRotate(List<GameObject> listObj, GameStartControl gameStartControl)
    {
        this.listObj = listObj;
        this.gameStartControl = gameStartControl;

        prependTime = 1f;
        moveTime = 3f;
        roatateTime = 3f;
        startPosition = new Vector3(0, 0, 0);
    }


    public void startAnim()
    {
        decomposeAnim();

    }


    private void decomposeAnim()
    {
        if (listObj == null)
            return;
        int listCount = listObj.Count;

        float radius = 0;
        if (gameStartControl == null)
            return;

        if (gameStartControl.picAllWith > gameStartControl.picAllHigh)
            radius = gameStartControl.picAllHigh;
        else
            radius = gameStartControl.picAllWith;

        List<Vector3> listCircleVec = GeometryUtil.getCircleVertices(startPosition, radius * 1.9f, listCount, true, CircleStartVectorEnum.Left);
        for (int i = 0; i < listCount; i++)
        {
            GameObject itemObj = listObj[i];
            Transform itemTF = itemObj.transform;

            //设置层级
            JigsawContainerCpt containerCpt = itemTF.GetComponent<JigsawContainerCpt>();
            if (containerCpt == null)
                continue;
            containerCpt.setSortingOrder(listCount - i);

            //设置动画
            Tweener tweener = itemTF
                 .DOMove(listCircleVec[i], moveTime)
                 .SetDelay(prependTime);
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
        int listCount = listObj.Count;
        float radius = 0;
        if (gameStartControl.picAllWith > gameStartControl.picAllHigh)
            radius = gameStartControl.picAllHigh;
        else
            radius = gameStartControl.picAllWith;

        List<Vector3> listCircleVec = GeometryUtil.getCircleVertices(startPosition, radius * 1.9f, listCount, true, CircleStartVectorEnum.Left);

        for (int i = 0; i < listCount; i++)
        {
            GameObject itemObj = listObj[i];
            Transform itemTF = itemObj.transform;

            Vector3[] arrayCircleVec = DevUtil.listToArrayFormPosition(listCircleVec, i);
            itemTF.DOPath(arrayCircleVec, roatateTime).OnComplete(delegate ()
            {
                JigsawContainerGameObjBuilder.addRigidbody(itemObj);
                JigsawContainerGameObjBuilder.addCollider(itemObj);
                Rigidbody2D itemRB = itemTF.GetComponent<Rigidbody2D>();
                int xForce = DevUtil.getRandomInt(-xForceMax, xForceMax);
                int yForce = DevUtil.getRandomInt(-yForceMax, yForceMax);
                itemRB.AddForce(new Vector2(xForce, yForce), ForceMode2D.Impulse);
            });
        }

        Tweener gameStartTweener = gameStartControl
            .transform
            .DOScale(new Vector3(1, 1, 1), roatateTime)
            .OnComplete(delegate ()
                 {
                  gameStartControl.gameStart();
                 });

    }
}