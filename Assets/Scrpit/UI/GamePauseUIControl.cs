using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUIControl : MonoBehaviour
{

    public Canvas pauseUICanvas;
    public Button restartBT;
    public Button exitBT;

    // Use this for initialization
    void Start()
    {
        pauseUICanvas = GetComponent<Canvas>();
        Button[] buttonList = GetComponentsInChildren<Button>();
        if (buttonList != null)
        {
            int buttonListSize = buttonList.Length;
            for (int i = 0; i < buttonListSize; i++)
            {
                Button itemBT = buttonList[i];
                if (itemBT.name.Equals("RestartButton"))
                {
                    restartBT = itemBT;
                    restartBT.onClick.AddListener(restartOnClick);
                }
                else if (itemBT.name.Equals("ExitButton"))
                {
                    exitBT = itemBT;
                    exitBT.onClick.AddListener(exitOnClick);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseUIEnabled();
        };
    }

    /// <summary>
    /// 是否开启暂停UI
    /// </summary>
    private void pauseUIEnabled()
    {
        if (pauseUICanvas == null)
            return;
        bool isEnabled = pauseUICanvas.isActiveAndEnabled;
        pauseUICanvas.enabled = !isEnabled;
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
}
