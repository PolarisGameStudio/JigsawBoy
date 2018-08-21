using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class GameOIUIControl : BaseUIControl
{
    public Button gameCancelBT;

    private new void Awake()
    {
        base.Awake();

        gameCancelBT = CptUtil.getCptFormParentByName<Transform, Button>(transform, "GameCancelBT");
        gameCancelBT.onClick.AddListener(cancelUI);
    }

    /// <summary>
    /// 关闭当前页面
    /// </summary>
    public void cancelUI()
    {
        SoundUtil.playSoundClip(AudioButtonOnClickEnum.btn_sound_3);
        mUIMasterControl.openUIByTypeAndCloseOther(UIEnum.GameMainUI);
    }

    public override void closeUI()
    {
        mUICanvas.enabled = false;
    }

    public override void loadUIData()
    {
        throw new System.NotImplementedException();
    }

    public override void openUI()
    {
        mUICanvas.enabled = true;
    }

    public override void refreshUI()
    {
        throw new System.NotImplementedException();
    }
}