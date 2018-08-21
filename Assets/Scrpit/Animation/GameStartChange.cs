using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using DG.Tweening;



public class GameStartChange : BaseAnimation
{
    //游戏控制器
    public GameStartControl gameStartControl;
    //所有的拼图对象
    private List<GameObject> listObj;
    //准备时间
    public float prependTime;
    //变换时间
    public float changeTime;

    //xy方向的分散力大小
    public int xForceMax = 100;
    public int yForceMax = 100;

    public GameStartChange(List<GameObject> listObj, GameStartControl gameStartControl)
    {
        this.listObj = listObj;
        this.gameStartControl = gameStartControl;

        prependTime = 3f;
        changeTime = 3f;
    }

    public void startAnim()
    {
        changeAnim();

    }
    private void changeAnim()
    {
        int listCount = listObj.Count;
        List<Vector3> otherListPosition = new List<Vector3>();
        for (int i = 0; i < listCount; i++)
        {
            otherListPosition.Add(listObj[i].transform.position);
        }
        for (int i = 0; i < listCount; i++)
        {
            GameObject itemObj = listObj[i];
            Transform itemTF = itemObj.transform;

            //设置层级
            JigsawContainerCpt containerCpt = itemTF.GetComponent<JigsawContainerCpt>();
            if (containerCpt == null)
                continue;
            containerCpt.setSortingOrder(listCount - i);

            int changeRandom = DevUtil.getRandomInt(0, otherListPosition.Count - 1);
            Vector3 changePosition = otherListPosition[changeRandom];
            itemTF
                .DOMove(changePosition, changeTime)
                .SetDelay(prependTime)
                .OnComplete(delegate() {
                    JigsawContainerGameObjBuilder.addRigidbody(itemObj);
                    JigsawContainerGameObjBuilder.addCollider(itemObj);
                    Rigidbody2D itemRB = itemTF.GetComponent<Rigidbody2D>();
                    int xForce = DevUtil.getRandomInt(-xForceMax, xForceMax);
                    int yForce = DevUtil.getRandomInt(-yForceMax, yForceMax);
                    itemRB.AddForce(new Vector2(xForce, yForce), ForceMode2D.Impulse);
                });
            otherListPosition.Remove(changePosition);
        }

        Tweener gameStartTweener = gameStartControl.transform
                                                .DOScale(new Vector3(1, 1, 1), changeTime+ prependTime)
                                                .OnComplete(delegate ()
                                                    {
                                                          gameStartControl.gameStart();
                                                    });
    }

}

