using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ToastDialog : BaseMonoBehaviour
{

    private Text mTVToast;

    private float mToastTime;
    private string mToastText;

    public ToastDialog()
    {
        mToastTime = 1.5f;
        mToastText = "";
    }
    // Use this for initialization
    void Start()
    {
        mTVToast = CptUtil.getCptFormParentByName<Transform, Text>(transform, "ToastText");
        mTVToast.text = mToastText;
        transform
            .DOScale(new Vector3(1, 1, 1), mToastTime)
            .OnComplete(delegate ()
            {
                Destroy(gameObject);
            });
    }


    /// <summary>
    /// 设置弹窗持续时间
    /// </summary>
    /// <param name="time"></param>
    public void setToastTime(int time)
    {
        mToastTime = time;
    }

    public void setToastText(string text)
    {
        mToastText = text;
    }
}
