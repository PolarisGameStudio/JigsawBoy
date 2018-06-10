using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class GameMainUIControl : BaseUIControl
{

    public Transform gameTimerTF;
    public GameTimerControlCpt gameTimerControlCpt;

    public Button gameInfoBT;
    public Button gamePauseBT;
    public Button gameMusicBT;
    private new void Awake()
    {
        base.Awake();
        gameTimerTF = CptUtil.getCptFormParentByName<Transform, Transform>(transform, "GameTimer");
        if (gameTimerTF != null)
        {
            gameTimerControlCpt = gameTimerTF.gameObject.AddComponent<GameTimerControlCpt>();
        }
        gameInfoBT = CptUtil.getCptFormParentByName<Transform, Button>(transform, "GameInfoBT");
        gamePauseBT = CptUtil.getCptFormParentByName<Transform, Button>(transform, "GamePauseBT");
        gameMusicBT = CptUtil.getCptFormParentByName<Transform, Button>(transform, "GameMusicBT");

        gameInfoBT.onClick.AddListener(openGameInfo);
        gamePauseBT.onClick.AddListener(openPauseBT);
        gameMusicBT.onClick.AddListener(openGameMusic);
    }

    /// <summary>
    /// 打开游戏信息
    /// </summary>
    public void openGameInfo()
    {
        mUIMasterControl.openUIByTypeAndCloseOther(UIEnum.GameInfoUI);
    }

    /// <summary>
    /// 打开游戏音乐
    /// </summary>
    public void openGameMusic() {
        mUIMasterControl.openUIByTypeAndCloseOther(UIEnum.GameMusicUI);
    }

    /// <summary>
    /// 打开暂停菜单
    /// </summary>
    public void openPauseBT()
    {
        mUIMasterControl.openUIByTypeAndCloseOther(UIEnum.GamePauseUI);
    }

    /// <summary>
    /// 开始游戏时间
    /// </summary>
    public void startTimer()
    {
        if (gameTimerControlCpt != null)
            gameTimerControlCpt.startTimer();
    }
    
    /// <summary>
    /// 结束游戏时间
    /// </summary>
    public void endTimer()
    {
        if (gameTimerControlCpt != null)
            gameTimerControlCpt.endTimer();
    }

    /// <summary>
    /// 获取游戏时间
    /// </summary>
    /// <returns></returns>
    public TimeBean getGameTimer()
    {
        if (gameTimerControlCpt != null) 
            return gameTimerControlCpt.getGameTimer();
        else
            return null;   
    }

    public override void openUI()
    {
        mUICanvas.enabled = true;
    }

    public override void closeUI()
    {
        mUICanvas.enabled = false;
    }

    public override void loadUIData()
    {

    }


}

