using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGameBackgroundUtil 
{
    //背景缩放大小
    public static float backgroundScale = 2.5f;
    //背景位置
    public static Vector3 backgroundVector = new Vector3(0, 0, 3);
    public static Vector3 particleGroundVector = new Vector3(0, 0, 2);
    public static Vector3 blurGroundVector = new Vector3(0, 0, 1);

    public static void createBackground(EquipColorEnum equipColor, float picAllW, float picAllH)
    {
        backgroundScale = CreateGameWallUtil.wallScale * 1.5f;
        setPicBackground(equipColor,picAllW, picAllH);
        setBlurBackground(picAllW, picAllH);
        setParticleBackground(picAllW, picAllH);
    }

    //设置背景
    public static GameObject setPicBackground(EquipColorEnum equipColor, float picAllW, float picAllH)
    {
        if (picAllW > picAllH)
        {
            picAllH = picAllW;
        }
        else
        {
            picAllW = picAllH;
        }
        GameObject picBackgroundObj =GameObject.Instantiate(ResourcesManager.LoadData<GameObject>("Prefab/Game/PicBackgroundGameObj"));
        picBackgroundObj.name = "GamePicBackground";
        picBackgroundObj.transform.position = backgroundVector;
        picBackgroundObj.transform.localScale = new Vector3(picAllW * backgroundScale, picAllH * backgroundScale, 3);
        setBackgroundColor(equipColor, picBackgroundObj);
        return picBackgroundObj;
    }

    //设置高斯模糊
    public static GameObject setBlurBackground(float picAllW, float picAllH)
    {
        if (picAllW > picAllH)
        {
            picAllH = picAllW;
        }
        else
        {
            picAllW = picAllH;
        }
        GameObject blurBackgroundObj = GameObject.Instantiate(ResourcesManager.LoadData<GameObject>("Prefab/Game/BlurBackgroundGameObj"));
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
    private static void setBackgroundColor(EquipColorEnum equipColorEnum, GameObject backgroundObj)
    {
        Renderer render = backgroundObj.GetComponent<Renderer>();
        if (render == null)
            return;
        EquipInfoBean infoBean = new EquipInfoBean();
        EnumUtil.getEquipColor(infoBean, equipColorEnum);
        Color equipColor = ColorUtil.getColor(infoBean.equipImageColor);
        render.material.color = equipColor;
    }

}
