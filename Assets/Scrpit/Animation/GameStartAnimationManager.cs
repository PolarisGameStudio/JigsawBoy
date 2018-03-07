using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
public class GameStartAnimationManager
{
    public static void startAnimation(GameStartControl gameStartControl, List<GameObject> listObj, GameStartAnimationEnum animationEnum)
    {
        BaseAnimation animation = null;
        if (animationEnum.Equals(GameStartAnimationEnum.Closure_Dispersed))
        {
            animation = new GameStartClosureDispersed(listObj, gameStartControl);
        }
        else if (animationEnum.Equals(GameStartAnimationEnum.Decompose_Rotate))
        {
            animation = new GameStartDecomposeRotate(listObj, gameStartControl);
        }
        else if (animationEnum.Equals(GameStartAnimationEnum.Change))
        {
            animation = new GameStartChange(listObj, gameStartControl);
        }
        if (animation != null)
            animation.startAnim();
    }
}

