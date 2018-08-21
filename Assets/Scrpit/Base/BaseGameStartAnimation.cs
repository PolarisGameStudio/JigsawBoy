using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class BaseGameStartAnimation
{
    //游戏控制器
    public GameStartControl mGameStartControl;
    //所有的拼图对象
    public List<GameObject> mListObj;
    //准备时间
    public float mPrependTime;

    public BaseGameStartAnimation(List<GameObject> listObj,GameStartControl startControl)
    {
        mGameStartControl = startControl;
        mListObj = listObj;
        mPrependTime = 3f;
    }

    /// <summary>
    /// 开始动画
    /// </summary>
    public abstract void startAnim();
}

