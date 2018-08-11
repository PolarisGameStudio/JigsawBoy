using UnityEngine;
using UnityEngine.UI;

public class PuzzlesUnlockDialog : BaseMonoBehaviour
{
    private Canvas mDialogCanvas;
    private Button mBTBackGround;
    private Button mBTUnlockPuzzles;
    private Image mUnlockPuzzlesImage;
    private Text mUnlockPuzzlesName;

    private string mPuzzlesNameStr;
    private string mPuzzlesMarkName;
    private string mPuzzlesImageUrl;

    void Start()
    {
        mDialogCanvas = GetComponent<Canvas>();
        mDialogCanvas.renderMode = RenderMode.ScreenSpaceCamera;
        mDialogCanvas.worldCamera = Camera.main;
        mDialogCanvas.sortingLayerName = "UITop";
        mBTBackGround = CptUtil.getCptFormParentByName<Transform, Button>(transform, "DialogBackGround");
        mUnlockPuzzlesImage = CptUtil.getCptFormParentByName<Transform, Image>(transform, "UnlockPuzzlesImage");
        mBTUnlockPuzzles = CptUtil.getCptFormParentByName<Transform, Button>(transform, "UnlockPuzzles");
        mUnlockPuzzlesName = CptUtil.getCptFormParentByName<Transform, Text>(transform, "UnlockPuzzlesName");

        mBTBackGround.onClick.AddListener(cancel);
        mBTUnlockPuzzles.onClick.AddListener(cancel);
        initData();
    }


    private void initData()
    {
        if (mUnlockPuzzlesName != null)
            mUnlockPuzzlesName.text = mPuzzlesNameStr;
        if (mUnlockPuzzlesImage != null) {
            //mUnlockPuzzlesImage.sprite= ResourcesManager.LoadAssetBundlesSpriteForBytes(mPuzzlesImageUrl, mPuzzlesMarkName);
            StartCoroutine( ResourcesManager.LoadAsyncAssetBundlesImageForBytes(mPuzzlesImageUrl, mPuzzlesMarkName, mUnlockPuzzlesImage));
        }
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
    /// 设置拼图名字
    /// </summary>
    /// <param name="puzzlesMarkName"></param>
    public void setPuzzlesMarkName(string puzzlesMarkName)
    {
        mPuzzlesMarkName = puzzlesMarkName;
    }

    /// <summary>
    /// 设置拼图地址
    /// </summary>
    /// <param name="puzzlesUrl"></param>
    public void setPuzzlesUrl(string puzzlesUrl)
    {
        mPuzzlesImageUrl = puzzlesUrl;
    }

    /// <summary>
    /// 动画状态改变
    /// </summary>
    public void AnimChange()
    {
        Animator animator = GetComponent<Animator>();
        animator.SetBool("isShow",true);
    }
}