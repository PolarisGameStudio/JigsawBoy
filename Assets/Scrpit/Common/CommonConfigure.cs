using Steamworks;
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

    static CommonConfigure()
    {
        refreshData();
    }

    public static void refreshData()
    {
        GameLanguage = GameLanguageEnum.English;
        IsOpenBGM = EnabledEnum.ON;
        IsOpenSound = EnabledEnum.ON;
        SceenMode = ScreenModeEnum.Full;

        PuzzlesShape = JigsawStyleEnum.Def;
        BorderShape = GameWallEnum.Def;
        BorderColor = EquipColorEnum.Def;
        Background = EquipColorEnum.Def;
        GameConfigureBean configureBean = DataStorageManage.getGameConfigureDSHandle().getData(0);
        if (configureBean != null)
        {
            //游戏语言设置
            GameLanguage = (GameLanguageEnum)Enum.ToObject(typeof(GameLanguageEnum), configureBean.gameLanguage);
            //是否开启BGM
            IsOpenBGM = (EnabledEnum)Enum.ToObject(typeof(EnabledEnum), configureBean.isOpenBGM);
            //是否开启音效
            IsOpenSound = (EnabledEnum)Enum.ToObject(typeof(EnabledEnum), configureBean.isOpenSound);
            //屏幕模式
            SceenMode= (ScreenModeEnum)Enum.ToObject(typeof(ScreenModeEnum), configureBean.screenMode);

            PuzzlesShape = (JigsawStyleEnum)Enum.ToObject(typeof(JigsawStyleEnum), configureBean.puzzlesShape);
            BorderShape = (GameWallEnum)Enum.ToObject(typeof(GameWallEnum), configureBean.borderShape);
            BorderColor = (EquipColorEnum)Enum.ToObject(typeof(EquipColorEnum), configureBean.borderColor);
            Background = (EquipColorEnum)Enum.ToObject(typeof(EquipColorEnum), configureBean.background);
        }
    }
}

