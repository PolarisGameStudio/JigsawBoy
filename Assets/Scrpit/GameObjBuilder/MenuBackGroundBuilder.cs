using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBackGroundBuilder
{

    public static GameObject buildMenuBack(Vector3 startPosition)
    {
        string menuBackName = "MenuBackGround";
        GameObject menuBackGroundObj = new GameObject(menuBackName);
        menuBackGroundObj.transform.position = startPosition;
        MenuBackGroundCpt menuBackGroundCpt = menuBackGroundObj.AddComponent<MenuBackGroundCpt>();

      //  GameObject blurBack = CreateGameBackgroundUtil.setBlurBackground(DevUtil.GetScreenWith(), DevUtil.GetScreenHeight());
      //  GameObject picBack = CreateGameBackgroundUtil.setPicBackground(DevUtil.GetScreenWith(), DevUtil.GetScreenHeight());

     //   blurBack.transform.SetParent(menuBackGroundObj.transform);
      //  picBack.transform.SetParent(menuBackGroundObj.transform);

        return menuBackGroundObj;
    }

}
