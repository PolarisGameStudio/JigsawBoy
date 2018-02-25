using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGameBackgroundUtil : MonoBehaviour
{
    //背景缩放大小
    public static float backgroundScale = 3f;
    //背景位置
    public static Vector3 backgroundVector = new Vector3(0, 0, 1);

    public static void createBackground(float picAllW, float picAllH)
    {
        setPicBackground(picAllW, picAllH);
        setBlurBackground(picAllW, picAllH);

    }

    //设置背景
    private static void setPicBackground(float picAllW, float picAllH)
    {
        GameObject picBackgroundObj = Instantiate(Resources.Load("Prefab/Game/PicBackgroundGameObj") as GameObject);
        picBackgroundObj.name = "GamePicBackground";
        picBackgroundObj.transform.position = backgroundVector;
        picBackgroundObj.transform.localScale = new Vector3(picAllW * backgroundScale, picAllH * backgroundScale, 3);

    }

    //设置高斯模糊
    private static void setBlurBackground(float picAllW, float picAllH)
    {
        GameObject blurBackgroundObj = Instantiate(Resources.Load("Prefab/Game/BlurBackgroundGameObj") as GameObject);
        blurBackgroundObj.name = "GameBlurBackground";
        blurBackgroundObj.transform.position = backgroundVector;
        blurBackgroundObj.transform.localScale = new Vector3(picAllW * backgroundScale, picAllH * backgroundScale, 1);
    }
}
