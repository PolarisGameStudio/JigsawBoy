using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class GameConfigureBean
{
    public int gameLanguage = 1;//默认英文
    public int isOpenBGM = 1;//默认开启
    public int isOpenSound = 1;//默认开启
    public int isOpenTimeUI = 1;//默认开启
    public int screenMode = 0;//全屏

    //拼图形状
    public int puzzlesShape;
    //边框形状
    public int borderShape;
    //边框颜色
    public int borderColor;
    //背景
    public int background;

    public float soundVolume=1;
    public float bgmVolume=1;
}

