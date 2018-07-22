using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class CommonConfigure
{
    //游戏语言
    public static GameLanguageEnum GameLanguage;
    //是否开启BGM
    public static EnabledEnum isOpenBGM;
    //是否开启音效
    public static EnabledEnum isOpenSound;

    static CommonConfigure()
    {
        refreshData();
    }

    public static void refreshData()
    {
        GameLanguage = GameLanguageEnum.English;
        isOpenBGM = EnabledEnum.ON;
        isOpenSound= EnabledEnum.ON;
        GameConfigureBean configureBean = DataStorageManage.getGameConfigureDSHandle().getData(0);
        if (configureBean != null)
        {
            //游戏语言设置
            GameLanguage = (GameLanguageEnum)Enum.ToObject(typeof(GameLanguageEnum), configureBean.gameLanguage);
            //是否开启BGM
            isOpenBGM = (EnabledEnum)Enum.ToObject(typeof(EnabledEnum), configureBean.isOpenBGM);
            //是否开启音效
            isOpenSound = (EnabledEnum)Enum.ToObject(typeof(EnabledEnum), configureBean.isOpenSound);
        }
    }
}

