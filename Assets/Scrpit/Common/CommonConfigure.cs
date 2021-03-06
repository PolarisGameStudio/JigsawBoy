﻿using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class CommonConfigure
{
    //游戏语言
    public static GameLanguageEnum GameLanguage;
    //是否开启BGM
    public static EnabledEnum IsOpenBGM;
    //是否开启音效
    public static EnabledEnum IsOpenSound;
    //是否开启即时UI
    public static EnabledEnum IsOpenTimeUI;
    //屏幕状态
    public static ScreenModeEnum SceenMode;
    //拼图形状
    public static JigsawStyleEnum PuzzlesShape;
    //边框形状
    public static GameWallEnum BorderShape;
    //边框颜色
    public static EquipColorEnum BorderColor;
    //背景
    public static EquipColorEnum Background;
    //音乐大小
    public static float BGMVolume;
    //音效大小
    public static float SoundVolume;


    static CommonConfigure()
    {
        refreshData();
    }

    public static void saveData()
    {
        GameConfigureBean data = new GameConfigureBean();
        data.gameLanguage = (int)GameLanguage;
        data.isOpenBGM = (int)IsOpenBGM;
        data.isOpenSound = (int)IsOpenSound;
        data.isOpenTimeUI = (int)IsOpenTimeUI;
        data.screenMode = (int)SceenMode;
        data.puzzlesShape = (int)PuzzlesShape;
        data.borderShape = (int)BorderShape;
        data.borderColor = (int)BorderColor;
        data.background = (int)Background;

        data.bgmVolume = BGMVolume;
        data.soundVolume = SoundVolume;
        DataStorageManage.getGameConfigureDSHandle().saveData(data);
        
    }


    public static void refreshData()
    {
        GameLanguage = GameLanguageEnum.English;
        IsOpenBGM = EnabledEnum.ON;
        IsOpenSound = EnabledEnum.ON;
        IsOpenTimeUI= EnabledEnum.ON;
        SceenMode = ScreenModeEnum.Full;

        PuzzlesShape = JigsawStyleEnum.Def;
        BorderShape = GameWallEnum.Def;
        BorderColor = EquipColorEnum.Def;
        Background = EquipColorEnum.Def;

        SoundVolume = 1f;
        BGMVolume = 1f;
        GameConfigureBean configureBean = DataStorageManage.getGameConfigureDSHandle().getData(0);
        if (configureBean != null)
        {
            //游戏语言设置
            GameLanguage = (GameLanguageEnum)Enum.ToObject(typeof(GameLanguageEnum), configureBean.gameLanguage);
            //是否开启BGM
            IsOpenBGM = (EnabledEnum)Enum.ToObject(typeof(EnabledEnum), configureBean.isOpenBGM);
            //是否开启音效
            IsOpenSound = (EnabledEnum)Enum.ToObject(typeof(EnabledEnum), configureBean.isOpenSound);
            //是否开启计时UI
            IsOpenTimeUI = (EnabledEnum)Enum.ToObject(typeof(EnabledEnum), configureBean.isOpenTimeUI);
            //屏幕模式
            SceenMode = (ScreenModeEnum)Enum.ToObject(typeof(ScreenModeEnum), configureBean.screenMode);

            PuzzlesShape = (JigsawStyleEnum)Enum.ToObject(typeof(JigsawStyleEnum), configureBean.puzzlesShape);
            BorderShape = (GameWallEnum)Enum.ToObject(typeof(GameWallEnum), configureBean.borderShape);
            BorderColor = (EquipColorEnum)Enum.ToObject(typeof(EquipColorEnum), configureBean.borderColor);
            Background = (EquipColorEnum)Enum.ToObject(typeof(EquipColorEnum), configureBean.background);

            SoundVolume = configureBean.soundVolume;
            BGMVolume= configureBean.bgmVolume;
        }
    }
}

