using UnityEngine;


public abstract class BaseUIControl : BaseMonoBehaviour
{
    // 总控制
    public UIMasterControl mUIMasterControl;
    // 当前UI
    public Canvas mUICanvas;

    /// <summary>
    /// 初始化数据
    /// </summary>
    public void Awake()
    {
        mUICanvas = GetComponent<Canvas>();
        closeUI();
    }

    public void Start()
    {
        mUIMasterControl = FindObjectOfType(typeof(UIMasterControl)) as UIMasterControl;
    }

    /// <summary>
    /// UI是否展示
    /// </summary>
    /// <returns></returns>
    public bool isShowUI()
    {
        return mUICanvas.isActiveAndEnabled;
    }

    /// <summary>
    /// 打开UI
    /// </summary>
    public abstract void openUI();
    /// <summary>
    /// 关闭UI
    /// </summary>
    public abstract void closeUI();
    /// <summary>
    /// 加载UI数据
    /// </summary>
    public abstract void loadUIData();
    /// <summary>
    /// 刷新UI元素
    /// </summary>
    public abstract void refreshUI();
}

