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
    }

    private void showGameInfoUI(bool isShow)
    {
        baseShowUI(isShow, UIEnum.GameInfoUI);
    }

    private void showGamePauseUI(bool isShow)
    {
        baseShowUI(isShow, UIEnum.GamePauseUI);
    }

    private void baseShowUI(bool isShow,UIEnum uIEnum)
    {
        if (isShow)
            uiMasterControl.openUIByType(uIEnum);
        else
            uiMasterControl.closeUIByType(uIEnum);
    }
}

