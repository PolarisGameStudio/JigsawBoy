using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
public class GameStartAnimationManager
{
    //xy方向的分散力大小
    public static int xForceMax = 100;
    public static int yForceMax = 100;

    public static void StartAnimation(GameStartControl gameStartControl, List<GameObject> listObj)
    {
        int animInt = DevUtil.getRandomInt(6, 6);
        GameStartAnimationEnum animEnum = (GameStartAnimationEnum)animInt;

        BaseGameStartAnimation animation = null;
        if (animEnum.Equals(GameStartAnimationEnum.Closure_Dispersed))
        {
            animation = new GameStartClosureDispersed(listObj, gameStartControl);
        }
        else if (animEnum.Equals(GameStartAnimationEnum.Decompose_Rotate))
        {
            animation = new GameStartDecomposeRotate(listObj, gameStartControl);
        }
        else if (animEnum.Equals(GameStartAnimationEnum.Change))
        {
            animation = new GameStartChange(listObj, gameStartControl);
        }
        else if (animEnum.Equals(GameStartAnimationEnum.Dispersed))
        {
            animation = new GameStartDispersed(listObj, gameStartControl);
        }
        else if (animEnum.Equals(GameStartAnimationEnum.Funnel))
        {
            animation = new GameStartFunnel(listObj, gameStartControl);
        }
        else if (animEnum.Equals(GameStartAnimationEnum.CircularRun))
        {
            animation = new GameStartFunnel(listObj, gameStartControl);
        }
        if (animation != null)
            animation.startAnim();
    }

    /// <summary>
    /// 拼图开始的准备工作  动画结束之后需要调用一次
    /// </summary>
    public static void PuzzlesStartPre(GameObject itemObj)
    {
        int xForce = DevUtil.getRandomInt(-xForceMax, xForceMax);
        int yForce = DevUtil.getRandomInt(-yForceMax, yForceMax);
        PuzzlesStartPre(itemObj, xForce, yForce);
    }
    /// <summary>
    /// 拼图开始的准备工作  动画结束之后需要调用一次
    /// </summary>
    public static void PuzzlesStartPre(GameObject itemObj,int xForce,int yForce)
    {
        JigsawContainerGameObjBuilder.addRigidbody(itemObj);
        JigsawContainerGameObjBuilder.addCollider(itemObj);
        Rigidbody2D itemRB = itemObj.GetComponent<Rigidbody2D>();
        itemRB.AddForce(new Vector2(xForce, yForce), ForceMode2D.Impulse);
    }
}

