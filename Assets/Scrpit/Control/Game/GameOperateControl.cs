using System.Collections.Generic;
using UnityEngine;

public class GameOperateControl : BaseMonoBehaviour
{
    public UIMasterControl uiMasterControl;

    private void Start()
    {
        uiMasterControl = GetComponent<UIMasterControl>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            bool isShow = uiMasterControl.isShowUI(UIEnum.GameInfoUI);
            showGameInfoUI(!isShow);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            bool isShow = uiMasterControl.isShowUI(UIEnum.GamePauseUI);
            showGamePauseUI(!isShow);
        };
        if (Input.GetKeyDown(KeyCode.M))
        {
            bool isShow = uiMasterControl.isShowUI(UIEnum.GameMusicUI);
            showGameMusicUI(!isShow);
        };
    }

    private void showGameInfoUI(bool isShow)
    {
        baseShowUI(isShow, UIEnum.GameInfoUI);
    }
    private void showGamePauseUI(bool isShow)
    {
        baseShowUI(isShow, UIEnum.GamePauseUI);
    }
    private void showGameMusicUI(bool isShow)
    {
        baseShowUI(isShow, UIEnum.GameMusicUI);
    }

    private void baseShowUI(bool isShow,UIEnum uIEnum)
    {
        if (isShow) {
            uiMasterControl.openUIByTypeAndCloseOther(uIEnum);
        }
        else
            uiMasterControl.openUIByTypeAndCloseOther(UIEnum.GameMainUI);
    }
}

