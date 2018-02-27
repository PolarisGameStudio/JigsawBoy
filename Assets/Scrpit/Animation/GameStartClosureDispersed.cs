using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using DG.Tweening;

public class GameStartClosureDispersed : BaseAnimation
{
    //所有的拼图对象
    private List<GameObject> listObj;
    //准备时间
    public float prependTime;
    //起始位置
    public Vector3 startPosition;
    //游戏控制器
    public GameStartControl gameStartControl;

    //聚拢时间
    public float closeureTime;

    //分散动画执行间隔
    public float dispersedOffsetTime;
    //执行间距
    public float animOffsetTime;

    //xy方向的分散力大小
    public int xForceMax = 100;
    public int yForceMax = 100;

    public GameStartClosureDispersed(List<GameObject> listObj, GameStartControl gameStartControl)
    {
        this.listObj = listObj;
        this.gameStartControl = gameStartControl;
        prependTime = 1f;
        startPosition = new Vector3(0, 0, 0);
        closeureTime = 0.5f;
        animOffsetTime = 0.01f;
        dispersedOffsetTime = 0.1f;
    }
    public void startAnim()
    {

        closureAnim();

    }

    /// <summary>
    /// 靠拢动画
    /// </summary>
    private void closureAnim()
    {
        int listCount = listObj.Count;
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
                 .DOMove(startPosition, closeureTime)
                 .SetDelay(prependTime + animOffsetTime * i);
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
        int listCount = listObj.Count;

        for (int i = 0; i < listCount; i++)
        {
            GameObject itemObj = listObj[i];
            Transform itemTF = itemObj.transform;

            //设置动画
            Tweener tweener = itemTF
                 .DOMove(startPosition, dispersedOffsetTime)
                 .SetDelay(animOffsetTime * i)
                 .OnComplete(delegate ()
                 {
                     JigsawContainerGameObjBuilder.addRigidbody(itemObj);
                     JigsawContainerGameObjBuilder.addCollider(itemObj);
                     Rigidbody2D itemRB = itemTF.GetComponent<Rigidbody2D>();
                     int xForce = DevUtil.getRandomInt(-xForceMax, xForceMax);
                     int yForce = DevUtil.getRandomInt(-yForceMax, yForceMax);
                     itemRB.AddForce(new Vector2(xForce, yForce), ForceMode2D.Impulse);
                 });
        }
        Tweener gameStartTweener = gameStartControl.transform
            .DOScale(new Vector3(1, 1, 1), dispersedOffsetTime + listCount * animOffsetTime)
            .OnComplete(delegate ()
             {
                 gameStartControl.gameStart();
             });
    }
}

