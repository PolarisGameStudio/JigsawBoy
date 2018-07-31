using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System;

public class PuzzlesUnlockDialog : BaseMonoBehaviour
{
    private Canvas mDialogCanvas;
    private Button mBTBackGround;
    private Image mUnlockPuzzlesImage;
    private Text mUnlockPuzzlesName;

    private string mPuzzlesNameStr;
    private string mPuzzlesImageUrl;

    void Start()
    {
        mDialogCanvas = GetComponent<Canvas>();
        mDialogCanvas.renderMode = RenderMode.ScreenSpaceCamera;
        mDialogCanvas.worldCamera = Camera.main;

        mBTBackGround = CptUtil.getCptFormParentByName<Transform, Button>(transform, "DialogBackGround");
        mUnlockPuzzlesImage = CptUtil.getCptFormParentByName<Transform, Image>(transform, "UnlockPuzzlesImage");
        mUnlockPuzzlesName = CptUtil.getCptFormParentByName<Transform, Text>(transform, "UnlockPuzzlesName");

        mBTBackGround.onClick.AddListener(cancel);
        initData();
    }


    private void initData()
    {
        if (mUnlockPuzzlesName != null)
            mUnlockPuzzlesName.text = mPuzzlesNameStr;
        if (mUnlockPuzzlesImage != null)
           StartCoroutine(ResourcesManager.loadAsyncDataImage(mPuzzlesImageUrl, mUnlockPuzzlesImage)) ;
       
    }

    /// <summary>
    /// 退出Dialog
    /// </summary>
    public void cancel()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// 设置拼图名字
    /// </summary>
    /// <param name="puzzlesName"></param>
    public void setPuzzlesName(string puzzlesName)
    {
        mPuzzlesNameStr = puzzlesName;
        if (mUnlockPuzzlesName != null)
            mUnlockPuzzlesName.text = mPuzzlesNameStr;
    }

    /// <summary>
    /// 设置拼图地址
    /// </summary>
    /// <param name="puzzlesUrl"></param>
    public void setPuzzlesUrl(string puzzlesUrl)
    {
        mPuzzlesImageUrl = puzzlesUrl;
        if (mUnlockPuzzlesImage != null)
            StartCoroutine(ResourcesManager.loadAsyncDataImage(mPuzzlesImageUrl, mUnlockPuzzlesImage));

    }
}