﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUIControl : BaseUIControl
{
    //重新开始
    public Button restartBT;
    public Text restartText;

    //离开
    public Button exitBT;
    public Text exitText;

    //退出观看
    public Button replayBT;
    public Text replayText;

    public Button gameCancelBT;

    private new void Awake()
    {
        base.Awake();
        restartBT= CptUtil.getCptFormParentByName<Transform, Button>(transform, "RestartButton");
        restartText = CptUtil.getCptFormParentByName<Transform, Text>(transform, "RestartText");

        exitBT = CptUtil.getCptFormParentByName<Transform, Button>(transform, "ExitButton");
        exitText = CptUtil.getCptFormParentByName<Transform, Text>(transform, "ExitText");

        replayBT = CptUtil.getCptFormParentByName<Transform, Button>(transform, "ReplayButton");
        replayText= CptUtil.getCptFormParentByName<Transform, Text>(transform, "ReplayText");

        restartBT.onClick.AddListener(restartOnClick);
        exitBT.onClick.AddListener(exitOnClick);
        replayBT.onClick.AddListener(replayOnClick);

        gameCancelBT = CptUtil.getCptFormParentByName<Transform, Button>(transform, "GameCancelBT");
        gameCancelBT.onClick.AddListener(cancelUI);

        restartText.text = CommonData.getText(38);
        exitText.text = CommonData.getText(39);
        replayText.text = CommonData.getText(40);
    }

    /// <summary>
    /// 完成拼图
    /// </summary>
    public void replayOnClick()
    {
        if (CommonData.GameStatus == 1)
        {
            mUIMasterControl.openUIByTypeAndCloseOther(UIEnum.GameMainUI);
            GameUtil.CompletePuzzles(this);
        }
        else {
            DialogManager.createToastDialog().setToastText("不能进行此操作");
        }
    }


    /// <summary>
    /// 关闭当前页面
    /// </summary>
    public void cancelUI()
    {
        mUIMasterControl.openUIByTypeAndCloseOther(UIEnum.GameMainUI);
    }


    /// <summary>
    /// 重新开始游戏
    /// </summary>
    private void restartOnClick()
    {
        SoundUtil.playSoundClip(AudioButtonOnClickEnum.btn_sound_1);
        SceneUtil.jumpGameScene();
    }

    /// <summary>
    /// 退出游戏
    /// </summary>
    private void exitOnClick()
    {
        SoundUtil.playSoundClip(AudioButtonOnClickEnum.btn_sound_3);
        SceneUtil.jumpMainScene();
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
    public override void refreshUI()
    {
    }
}
