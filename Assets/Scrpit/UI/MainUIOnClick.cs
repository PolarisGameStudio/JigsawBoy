using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUIOnClick : BaseMonoBehaviour
{
    //按钮
    private Button[] listBT;
    //画布
    private Canvas mainUICanvas;

    void Start()
    {
        initData();
    }

    /// <summary>
    /// 初始化按钮
    /// </summary>
    private void initData()
    {
        //设置画布
        mainUICanvas= GetComponent<Canvas>();
        //设置按钮
        listBT = gameObject.GetComponentsInChildren<Button>();
        if (listBT != null)
        {
            int listBTSize = listBT.Length;
            for (int listPosition = 0; listPosition < listBTSize; listPosition++)
            {
                Button itemBT = listBT[listPosition];
                if (itemBT.name.Equals("StartGameBT"))
                {
                    itemBT.onClick.AddListener(startGameOnClick);
                }
            }
        }
    }

    /// <summary>
    /// 点击开始游戏按钮
    /// </summary>
    public void startGameOnClick()
    {
        //隐藏当前UI
        mainUICanvas.enabled = false;
        //打开开始游戏UI
        GameObject startGameUI = GameObject.Find("StartGameUI");
        if (startGameUI != null)
        {
            Canvas startGameCanvas= startGameUI.GetComponent<Canvas>();
            if (startGameCanvas != null)
                startGameCanvas.enabled = true;
        }
    }
}
