using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections.Generic;
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

        refreshUI();
    }

    /// <summary>
    /// 语言选择
    /// </summary>
    /// <param name="position"></param>
    private void languageSelection(int position)
    {
        GameConfigureBean configure= DataStorageManage.getGameConfigureDSHandle().getData(0);
        configure.gameLanguage = position;
        DataStorageManage.getGameConfigureDSHandle().saveData(configure);
        CommonConfigure.refreshData();
        CommonData.refreshData();
   
        mUIMasterControl.refreshAllUI();
    }

    /// <summary>
    /// 增加退出按钮监听
    /// </summary>
    public void addExitOnClick()
    {
        SoundUtil.playSoundClip(AudioButtonOnClickEnum.btn_sound_2);
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
        List<string>  listLanguageList = new List<string>();
        listLanguageList.Add("中文");
        listLanguageList.Add("English");

        if (mLanguageSelectionTitle != null)
            mLanguageSelectionTitle.text = CommonData.getText(28);
        if (mMusicSelectionTitle != null)
            mMusicSelectionTitle.text = CommonData.getText(29);
        if (mSoundSelectionTitle != null)
            mSoundSelectionTitle.text = CommonData.getText(30);

        if (mLanguageSelectionDropdown != null)
        {
            mLanguageSelectionDropdown.ClearOptions();
            mLanguageSelectionDropdown.AddOptions(listLanguageList);
            mLanguageSelectionDropdown.value = (int)CommonConfigure.GameLanguage;
            mLanguageSelectionDropdown.onValueChanged.AddListener(languageSelection);
        }
        if (mMusicSelectionSwith != null) {
            mMusicSelectionSwith.setStatus((int)CommonConfigure.isOpenBGM);
            mMusicSelectionSwith.setCallBack(this);
        }

        if (mSoundSelectionSwitch != null) {
            mSoundSelectionSwitch.setStatus((int)CommonConfigure.isOpenSound);
            mSoundSelectionSwitch.setCallBack(this);
        }
  
    }

    /// <summary>
    /// 开关切换
    /// </summary>
    /// <param name="view"></param>
    /// <param name="status"></param>
    public void onSwitchChange(GameObject view, int status)
    {
        SoundUtil.playSoundClip(AudioButtonOnClickEnum.btn_sound_3);
        GameConfigureBean configure = DataStorageManage.getGameConfigureDSHandle().getData(0);
        if (view == mMusicSelectionSwith.gameObject)
        {
            configure.isOpenBGM = status;
            if (configure.isOpenBGM ==0)
                SoundUtil.stopBGMClip();
            else
                SoundUtil.playBGMClip();
        }
        else if (view == mSoundSelectionSwitch.gameObject)
        {
            configure.isOpenSound = status;
        }
        DataStorageManage.getGameConfigureDSHandle().saveData(configure);
        CommonConfigure.refreshData();
        CommonData.refreshData();
    }
}