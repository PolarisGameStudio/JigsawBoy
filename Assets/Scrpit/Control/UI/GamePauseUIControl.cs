using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUIControl : BaseUIControl
{
    public Button restartBT;
    public Button exitBT;

    public Button gameCancelBT;
    private new void Awake()
    {
        base.Awake();
        restartBT= CptUtil.getCptFormParentByName<Transform, Button>(transform, "RestartButton");
        exitBT = CptUtil.getCptFormParentByName<Transform, Button>(transform, "ExitButton");

        restartBT.onClick.AddListener(restartOnClick);
        exitBT.onClick.AddListener(exitOnClick);

        gameCancelBT = CptUtil.getCptFormParentByName<Transform, Button>(transform, "GameCancelBT");
        gameCancelBT.onClick.AddListener(cancelUI);
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

    }

    /// <summary>
    /// 退出游戏
    /// </summary>
    private void exitOnClick()
    {
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
}
