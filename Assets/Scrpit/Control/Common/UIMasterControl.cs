using System;
using System.Collections.Generic;
using UnityEngine;

public class UIMasterControl : BaseMonoBehaviour
{
    //所有的UI
    public BaseUIControl[] listCanvas;

    private void Awake()
    {
        listCanvas = FindObjectsOfType(typeof(BaseUIControl)) as BaseUIControl[];
    }


    /// <summary>
    /// 获取UI
    /// </summary>
    /// <param name="uiName"></param>
    public T getUIByType<T>(UIEnum uiEnum) where T : BaseUIControl
    {
        if (listCanvas == null || listCanvas.Length == 0)
            return null; 
        int canvasSize = listCanvas.Length;
        for (int i = 0; i < canvasSize; i++)
        {
            BaseUIControl itemCanvas = listCanvas[i];
            string uiName = uiEnum.ToString();
            if (itemCanvas.name.Equals(uiName))
            {
                return (T)itemCanvas;
            }
        }
        return null;
    }

    /// <summary>
    /// 打开指定UI 并关闭其它UI
    /// </summary>
    /// <param name="uiEnum"></param>
    public void openUIByTypeAndCloseOther(UIEnum uiEnum)
    {
        if (listCanvas == null || listCanvas.Length == 0)
            return;
        int canvasSize = listCanvas.Length;
        for (int i = 0; i < canvasSize; i++)
        {
            BaseUIControl itemControl = listCanvas[i];
            string uiName = uiEnum.ToString();
            if (itemControl.name.Equals(uiName))
                itemControl.openUI();
            else
                itemControl.closeUI();
        }
    }

    /// <summary>
    /// 打开指定UI
    /// </summary>
    /// <param name="uiEnum"></param>
    public void openUIByType(UIEnum uiEnum)
    {
        if (listCanvas == null || listCanvas.Length == 0)
            return;
        int canvasSize = listCanvas.Length;
        for (int i = 0; i < canvasSize; i++)
        {
            BaseUIControl itemControl = listCanvas[i];
            string uiName = uiEnum.ToString();
            if (itemControl.name.Equals(uiName))
                itemControl.openUI();
        }
    }


    /// <summary>
    /// 关闭指定UI
    /// </summary>
    /// <param name="uiEnum"></param>
    public void closeUIByType(UIEnum uiEnum)
    {
        if (listCanvas == null || listCanvas.Length == 0)
            return;
        int canvasSize = listCanvas.Length;
        for (int i = 0; i < canvasSize; i++)
        {
            BaseUIControl itemControl = listCanvas[i];
            string uiName = uiEnum.ToString();
            if (itemControl.name.Equals(uiName))
                itemControl.closeUI();
        }
    }

    /// <summary>
    /// UI是否展示
    /// </summary>
    public bool isShowUI(UIEnum uiEnum)
    {
        if (listCanvas == null || listCanvas.Length == 0)
            return false;
        int canvasSize = listCanvas.Length;
        for (int i = 0; i < canvasSize; i++)
        {
            BaseUIControl itemControl = listCanvas[i];
            string uiName = uiEnum.ToString();
            if (itemControl.name.Equals(uiName))
                return itemControl.isShowUI();
        }
        return false;
    }
}

