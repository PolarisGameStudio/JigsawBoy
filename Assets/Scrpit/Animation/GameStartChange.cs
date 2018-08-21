using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using DG.Tweening;



public class GameStartChange : BaseGameStartAnimation
{
    //变换时间
    public float changeTime;

    public GameStartChange(List<GameObject> listObj, GameStartControl startControl) : base(listObj, startControl)
    {
        changeTime = 3f;
    }

    public override void startAnim()
    {
        changeAnim();
    }

    private void changeAnim()
    {
        int listCount = mListObj.Count;
        List<Vector3> otherListPosition = new List<Vector3>();
        for (int i = 0; i < listCount; i++)
        {
            otherListPosition.Add(mListObj[i].transform.position);
        }
        for (int i = 0; i < listCount; i++)
        {
            GameObject itemObj = mListObj[i];
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
                .SetDelay(mPrependTime)
                .OnComplete(delegate() {
                    GameStartAnimationManager.PuzzlesStartPre(itemObj);
                });
            otherListPosition.Remove(changePosition);
        }

        Tweener gameStartTweener = mGameStartControl.transform
                                                .DOScale(new Vector3(1, 1, 1), changeTime+ mPrependTime)
                                                .OnComplete(delegate ()
                                                    {
                                                        mGameStartControl.gameStart();
                                                    });
    }

 
}

