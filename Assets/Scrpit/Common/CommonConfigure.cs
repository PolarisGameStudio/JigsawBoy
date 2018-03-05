using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class CommonConfigure
{
    //游戏语言
    public static GameLanguageEnum GameLanguage;

    static CommonConfigure()
    {
        GameLanguage = GameLanguageEnum.Chinese;
        GameConfigureBean configureBean=  DataStorageManage.getGameConfigureDSHandle().getAllData();
        if (configureBean != null)
        {
            //游戏语言设置
            if (configureBean.gameLanguage != 0)
            {
                GameLanguage = (GameLanguageEnum)Enum.ToObject(typeof(GameLanguageEnum), configureBean.gameLanguage);
            }
        }
    }
}

