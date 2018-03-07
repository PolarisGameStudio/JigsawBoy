using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class MenuMainUIControl : BaseUIControl
{
    //开始按钮
    public Button startGameBT;
    public Text startGameText;
    //自定义按钮
    public Button customBT;
    public Text customText;

    private new void Awake()
    {
        base.Awake();
      
        startGameBT = CptUtil.getCptFormParentByName<Canvas, Button>(mUICanvas, "StartGameBT");
        startGameText = CptUtil.getCptFormParentByName<Button, Text>(startGameBT, "StartGameText");

        customBT = CptUtil.getCptFormParentByName<Canvas, Button>(mUICanvas, "CustomBT");
        customText = CptUtil.getCptFormParentByName<Button, Text>(customBT, "CustomText");

        startGameBT.onClick.AddListener(startGameOnClick);
        customBT.onClick.AddListener(diyOnClick);

    }

    /// <summary>
    /// 进入拼图选择界面
    /// </summary>
    private void startGameOnClick()
    {
        if (mUIMasterControl == null)
            return;
        mUIMasterControl.openUIByTypeAndCloseOther(UIEnum.MenuSelectUI);
    }

    private void diyOnClick()
    {

    }

    private void gameConfigureOnClick()
    {

    }

    private void exitGameOnClick()
    {
        SystemUtil.exitGame();
    }

    public override void openUI()
    {
        mUICanvas.enabled = true;
        loadUIData();
    }

    public override void closeUI()
    {
        mUICanvas.enabled = false;
    }

    public override void loadUIData()
    {
      
    }
}
