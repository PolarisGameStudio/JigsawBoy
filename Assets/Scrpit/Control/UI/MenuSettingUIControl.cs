using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Steamworks;
using System;

public class MenuSettingUIControl : BaseUIControl ,SwitchButton.CallBack
{
    public Transform mJigsawSelectTiltebar;
    public Button mTitleBarExitBT;

    public Text mLanguageSelectionTitle;
    public Dropdown mLanguageSelectionDropdown;

    public Text mMusicSelectionTitle;
    public SwitchButton mMusicSelectionSwith;

    public Text mSoundSelectionTitle;
    public SwitchButton mSoundSelectionSwitch;

    public Text mTimeUISelectionTitle;
    public SwitchButton mTimeUISelectionSwith;

    public Text mScreenModeTitle;
    public Dropdown mScreenModeDropdown;

    public Text mScreenResolutionTitle;
    public Dropdown mScreenResolutionDropdown;

    public Slider musicSlider;
    public Slider soundSlider;

    AudioSourceControl audioSourceControl;
    private new void Awake()
    {
        base.Awake();

        //初始化标题栏
        mJigsawSelectTiltebar = CptUtil.getCptFormParentByName<Transform, Transform>(transform, "TitleBar");
        mTitleBarExitBT = CptUtil.getCptFormParentByName<Transform, Button>(mJigsawSelectTiltebar, "ExitBT");
        if (mTitleBarExitBT != null)
        {
            mTitleBarExitBT.onClick.AddListener(addExitOnClick);
        }
        //初始化语言下拉
        mLanguageSelectionTitle = CptUtil.getCptFormParentByName<Transform, Text>(transform, "LanguageSelectionTitle");
        mLanguageSelectionDropdown = CptUtil.getCptFormParentByName<Transform, Dropdown>(transform, "LanguageSelectionDropdown");

        //初始化BGM
        mMusicSelectionTitle = CptUtil.getCptFormParentByName<Transform, Text>(transform, "MusicSelectionTitle");
        mMusicSelectionSwith= CptUtil.getCptFormParentByName<Transform, SwitchButton>(transform, "MusicSelectionSwitch");
        //初始化音效
        mSoundSelectionTitle = CptUtil.getCptFormParentByName<Transform, Text>(transform, "SoundSelectionTitle");
        mSoundSelectionSwitch = CptUtil.getCptFormParentByName<Transform, SwitchButton>(transform, "SoundSelectionSwitch");

        //初始化模式选择
        mScreenModeTitle = CptUtil.getCptFormParentByName<Transform, Text>(transform, "ScreenModeTitle");
        mScreenModeDropdown = CptUtil.getCptFormParentByName<Transform, Dropdown>(transform, "ScreenModeDropdown");
        //初始化分辨率选择
        //mScreenResolutionTitle = CptUtil.getCptFormParentByName<Transform, Text>(transform, "ScreenResolutionTitle");
        //mScreenResolutionDropdown = CptUtil.getCptFormParentByName<Transform, Dropdown>(transform, "ScreenResolutionDropdown");

        musicSlider.onValueChanged.AddListener(musicSliderListener);
        soundSlider.onValueChanged.AddListener(soundSliderListener);

        refreshUI();
    }

    private new void Start()
    {
        base.Start();
        audioSourceControl = Camera.main.GetComponent<AudioSourceControl>();
    }

    public void musicSliderListener(float size)
    {
        CommonConfigure.BGMVolume = size;
        if (audioSourceControl != null)
        {
            audioSourceControl.changeVolume(size);
        }
    }

    public void soundSliderListener(float size)
    {
        CommonConfigure.SoundVolume = size;
    }
    /// <summary>
    /// 增加退出按钮监听
    /// </summary>
    public void addExitOnClick()
    {
        SoundUtil.playSoundClip(AudioButtonOnClickEnum.btn_sound_2);
        CommonConfigure.saveData();
        mUIMasterControl.openUIByTypeAndCloseOther(UIEnum.MenuMainUI);
    }

    public override void closeUI()
    {
        mUICanvas.enabled = false;
    }

    public override void loadUIData()
    {

    }

    public override void openUI()
    {
        mUICanvas.enabled = true;
    }

    public override void refreshUI()
    {
        musicSlider.value = CommonConfigure.BGMVolume;
        soundSlider.value = CommonConfigure.SoundVolume;

        List<string>  languageList = new List<string>();
        languageList.Add("中文");
        languageList.Add("English");
        languageList.Add("Deutsch");
        languageList.Add("日本語");
        languageList.Add("русский");
        languageList.Add("Français");
        languageList.Add("Polski");

        int count = CommonData.UITextMap.Count;
        if (mLanguageSelectionTitle != null)
            mLanguageSelectionTitle.text = CommonData.getText(28);
        if (mMusicSelectionTitle != null)
            mMusicSelectionTitle.text = CommonData.getText(29);
        if (mSoundSelectionTitle != null)
            mSoundSelectionTitle.text = CommonData.getText(30);
        if (mScreenModeTitle != null)
            mScreenModeTitle.text = CommonData.getText(77);
        if (mTimeUISelectionTitle != null)
            mTimeUISelectionTitle.text = CommonData.getText(133);

        if (mLanguageSelectionDropdown != null)
        {
            mLanguageSelectionDropdown.ClearOptions();
            mLanguageSelectionDropdown.AddOptions(languageList);
            mLanguageSelectionDropdown.onValueChanged.RemoveAllListeners();
            mLanguageSelectionDropdown.value = (int)CommonConfigure.GameLanguage;
            mLanguageSelectionDropdown.onValueChanged.AddListener(languageSelection);
        }
        if (mMusicSelectionSwith != null) {
            mMusicSelectionSwith.setCallBack(null);
            mMusicSelectionSwith.setStatus((int)CommonConfigure.IsOpenBGM);
            mMusicSelectionSwith.setCallBack(this);
        }
        if (mSoundSelectionSwitch != null) {
            mSoundSelectionSwitch.setCallBack(null);
            mSoundSelectionSwitch.setStatus((int)CommonConfigure.IsOpenSound);
            mSoundSelectionSwitch.setCallBack(this);
        }
        if (mTimeUISelectionSwith != null)
        {
            mTimeUISelectionSwith.setCallBack(null);
            mTimeUISelectionSwith.setStatus((int)CommonConfigure.IsOpenTimeUI);
            mTimeUISelectionSwith.setCallBack(this);
        }

        List<string> listScreenMode = new List<string>();
        listScreenMode.Add(CommonData.getText(78));
        listScreenMode.Add(CommonData.getText(79));
        if (mScreenModeDropdown != null) {
            mScreenModeDropdown.ClearOptions();
            mScreenModeDropdown.AddOptions(listScreenMode);
            mScreenModeDropdown.onValueChanged.RemoveAllListeners();
            mScreenModeDropdown.value = (int)CommonConfigure.SceenMode;
            mScreenModeDropdown.onValueChanged.AddListener(screenModeSelection);
        }
    }

    /// <summary>
    /// 屏幕模式
    /// </summary>
    /// <param name="position"></param>
    private void screenModeSelection(int position)
    {
        SoundUtil.playSoundClip(AudioButtonOnClickEnum.btn_sound_3);
        if (position == 0) {
            Screen.fullScreen = true;
        }
        else {
            Screen.fullScreen = false;
        }
        GameConfigureBean configure = DataStorageManage.getGameConfigureDSHandle().getData(0);
        configure.screenMode = position;
        DataStorageManage.getGameConfigureDSHandle().saveData(configure);
    }

    /// <summary>
    /// 语言选择
    /// </summary>
    /// <param name="position"></param>
    private void languageSelection(int position)
    {
        SoundUtil.playSoundClip(AudioButtonOnClickEnum.btn_sound_3);
        GameConfigureBean configure = DataStorageManage.getGameConfigureDSHandle().getData(0);
        configure.gameLanguage = position;
        DataStorageManage.getGameConfigureDSHandle().saveData(configure);
        CommonConfigure.refreshData();
        CommonData.refreshData();
        mUIMasterControl.refreshAllUI();
    }

    /// <summary>
    /// 开关切换
    /// </summary>
    /// <param name="view"></param>
    /// <param name="status"></param>
    public void onSwitchChange(GameObject view, int status)
    {
        SoundUtil.playSoundClip(AudioButtonOnClickEnum.btn_sound_3);
        if (view == mMusicSelectionSwith.gameObject)
        {
            CommonConfigure.IsOpenBGM= (EnabledEnum)Enum.ToObject(typeof(EnabledEnum), status);
            if ((int)CommonConfigure.IsOpenBGM == 0)
                SoundUtil.stopBGMClip();
            else
                SoundUtil.playBGMClip();
        }
        else if (view == mSoundSelectionSwitch.gameObject)
        {
            CommonConfigure.IsOpenSound = (EnabledEnum)Enum.ToObject(typeof(EnabledEnum), status);
        }
        else if( view== mTimeUISelectionSwith.gameObject)
        {
            CommonConfigure.IsOpenTimeUI= (EnabledEnum)Enum.ToObject(typeof(EnabledEnum), status);
        }
        CommonConfigure.saveData();
        CommonConfigure.refreshData();
        CommonData.refreshData();
    }
}