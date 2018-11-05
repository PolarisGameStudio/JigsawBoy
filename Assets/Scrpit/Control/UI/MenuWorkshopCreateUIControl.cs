using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class MenuWorkshopCreateUIControl : BaseUIControl
{

    public Button exitBt;

    private new void Awake()
    {
        base.Awake();
        exitBt.onClick.AddListener(ExitOnClick);
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

    private void ExitOnClick()
    {
        SoundUtil.playSoundClip(AudioButtonOnClickEnum.btn_sound_2);
        mUIMasterControl.openUIByTypeAndCloseOther(UIEnum.MenuWorkshop);
    }
}