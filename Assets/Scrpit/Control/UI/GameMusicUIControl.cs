using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class GameMusicUIControl : BaseUIControl, IRadioButtonCallBack
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
    public void radioBTOnClick(Toggle toggle, bool value)
    {
        if (toggle.name.Equals("MusicOn") && value == true)
        {
            if (!audioSourceControl.isPlayBGMClip())
                audioSourceControl.playBGMClip(AudioBGMEnum.Op9_No2);
        }
        else if (toggle.name.Equals("MusicOff") && value == true)
        {
            audioSourceControl.stopBGMClip();
        }
    }
}

