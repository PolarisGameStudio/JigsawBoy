using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class MenuMainUIControl : BaseMonoBehaviour
{

    public Canvas menuMainUICanvas;
    //开始按钮
    public Button startGameBT;
    public Text startGameText;
    //自定义按钮
    public Button customBT;
    public Text customText;

    void Start()
    {
        menuMainUICanvas = GetComponent<Canvas>();

        startGameBT = CptUtil.getCptFormParentByName<Canvas, Button>(menuMainUICanvas, "StartGameBT");
        startGameText = CptUtil.getCptFormParentByName<Button, Text>(startGameBT, "StartGameText");

        customBT = CptUtil.getCptFormParentByName<Canvas, Button>(menuMainUICanvas, "CustomBT");
        customText = CptUtil.getCptFormParentByName<Button, Text>(customBT, "CustomText");

        menuMainUICanvas.enabled = true;

        startGameBT.onClick.AddListener(startGameOnClick);
        customBT.onClick.AddListener(customOnClick);
    }


    private void startGameOnClick()
    {

    }

    private void customOnClick()
    {

    }
}
