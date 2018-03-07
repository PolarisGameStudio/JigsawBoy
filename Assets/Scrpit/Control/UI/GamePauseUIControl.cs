using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUIControl : BaseUIControl
{
    public Button restartBT;
    public Button exitBT;

    private new void Awake()
    {
        base.Awake();
        restartBT= CptUtil.getCptFormParentByName<Transform, Button>(transform, "RestartButton");
        exitBT = CptUtil.getCptFormParentByName<Transform, Button>(transform, "ExitButton");

        restartBT.onClick.AddListener(restartOnClick);
        exitBT.onClick.AddListener(exitOnClick);

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
