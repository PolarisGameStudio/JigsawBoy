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
        }
    }
}

