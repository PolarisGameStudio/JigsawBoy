using UnityEngine;
using UnityEngine.UI;

public class GameMusicUIControl : BaseUIControl, IRadioButtonCallBack<Toggle,long>,IButtonCallBack<Button, BGMInfoBean>
{
    public AudioSourceControl audioSourceControl;

    public Transform musicSelectTF;
    public GameMusicDetails musicSelect;

    public ToggleGroup musicSwitchGroup;
    public GameMusicSwitch musicSwitchCpt;
    private new void Awake()
    {
        base.Awake();

        musicSelectTF = CptUtil.getCptFormParentByName<Transform, Transform>(transform, "Content");
        if (musicSelectTF != null)
        {
            musicSelect = musicSelectTF.gameObject.AddComponent<GameMusicDetails>();
            musicSelect.addMusicSelectCallBack(this);
            musicSelect.loadData();
        }

        musicSwitchGroup = CptUtil.getCptFormParentByName<Transform, ToggleGroup>(transform, "GameMusicSwitch");
        if (musicSwitchGroup != null)
        {
            musicSwitchCpt = musicSwitchGroup.gameObject.AddComponent<GameMusicSwitch>();
            musicSwitchCpt.addRadioButtonCallBack(this);
        }

    }

    private new void Start()
    {
        audioSourceControl = CptUtil.getCptFormSceneByName<AudioSourceControl>("Main Camera");
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


    /// <summary>
    /// 是否播放音乐监听
    /// </summary>
    /// <param name="toggle"></param>
    /// <param name="value"></param>
    public void radioBTOnClick(Toggle radioBT, bool value, long data)
    {
        if (radioBT.name.Equals("MusicOn") && value == true)
        {
            if (!audioSourceControl.isPlayBGMClip())
                audioSourceControl.playBeforeBGMClip();
        }
        else if (radioBT.name.Equals("MusicOff") && value == true)
        {
            audioSourceControl.stopBGMClip();
        }
    }

    public void buttonOnClick(Button button, BGMInfoBean data)
    {
            audioSourceControl.playBGMClip(data);
    }
}

