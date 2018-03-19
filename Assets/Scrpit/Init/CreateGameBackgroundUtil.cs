using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGameBackgroundUtil : BaseMonoBehaviour
{
    //背景缩放大小
    public static float backgroundScale = 2f;
    //背景位置
    public static Vector3 backgroundVector = new Vector3(0, 0, 3);
    public static Vector3 particleGroundVector = new Vector3(0, 0, 2);
    public static Vector3 blurGroundVector = new Vector3(0, 0, 1);

    public static void createBackground(float picAllW, float picAllH)
    {
        setPicBackground(picAllW, picAllH);
        setBlurBackground(picAllW, picAllH);
        setParticleBackground(picAllW, picAllH);
    }

    //设置背景
    public static GameObject setPicBackground(float picAllW, float picAllH)
    {
        GameObject picBackgroundObj = Instantiate(ResourcesManager.loadData<GameObject>("Prefab/Game/PicBackgroundGameObj"));
        picBackgroundObj.name = "GamePicBackground";
        picBackgroundObj.transform.position = backgroundVector;
        picBackgroundObj.transform.localScale = new Vector3(picAllW * backgroundScale, picAllH * backgroundScale, 3);
        return picBackgroundObj;
    }

    //设置高斯模糊
    public static GameObject setBlurBackground(float picAllW, float picAllH)
    {
        GameObject blurBackgroundObj = Instantiate(ResourcesManager.loadData<GameObject>("Prefab/Game/BlurBackgroundGameObj"));
        blurBackgroundObj.name = "GameBlurBackground";
        blurBackgroundObj.transform.position = blurGroundVector;
        blurBackgroundObj.transform.localScale = new Vector3(picAllW * backgroundScale, picAllH * backgroundScale, 1);
        return blurBackgroundObj;
    }

    //设置粒子背景
    public static void setParticleBackground(float picAllW, float picAllH)
    {
      // CreateParticleUtil.createBackParticle(particleGroundVector, picAllW * backgroundScale, picAllH * backgroundScale, BackParticleEnum.Def);
    }
}
