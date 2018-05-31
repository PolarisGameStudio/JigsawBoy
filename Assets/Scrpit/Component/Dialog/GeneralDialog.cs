using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneralDialog : BaseMonoBehaviour
{
    private Button mBTBackGround;
    private Button mBTSubmit;
    private Button mBTCancel;

    private Text mTXTitle;
    private Text mTXContent;
    private Text mTXSubmit;
    private Text mTXCancel;

    private CallBack mCallBack;

    private string mTitleStr;
    private string mContentStr;
    private string mSubmitStr;
    private string mCancelStr;

    public GeneralDialog()
    {
        mTitleStr = "提示";
        mContentStr = "";
        mSubmitStr = "确定";
        mCancelStr = "取消";
    }

    void Start()
    {
        mBTBackGround = CptUtil.getCptFormParentByName<Transform, Button>(transform, "DialogBackGround");
        mBTSubmit = CptUtil.getCptFormParentByName<Transform, Button>(transform, "SubmitBT");
        mBTCancel = CptUtil.getCptFormParentByName<Transform, Button>(transform, "CancelBT");
        mTXTitle = CptUtil.getCptFormParentByName<Transform, Text>(transform, "TitleText");
        mTXContent = CptUtil.getCptFormParentByName<Transform, Text>(transform, "ContentText");
        mTXCancel = CptUtil.getCptFormParentByName<Transform, Text>(transform, "CancelText");
        mTXSubmit = CptUtil.getCptFormParentByName<Transform, Text>(transform, "SubmitText");

        mTXTitle.text = mTitleStr;
        mTXContent.text = mContentStr;
        mTXCancel.text = mCancelStr;
        mTXSubmit.text = mSubmitStr;

        mBTBackGround.onClick.AddListener(cancel);
        mBTCancel.onClick.AddListener(cancel);
        mBTSubmit.onClick.AddListener(submit);
    }

    /// <summary>
    /// 设置标题
    /// </summary>
    /// <param name="title"></param>
    public GeneralDialog setTitle(string title)
    {
        mTitleStr = title;
        if (mTXTitle != null)
            mTXTitle.text = mTitleStr;
        return this;
    }

    /// <summary>
    /// 设置内容
    /// </summary>
    /// <param name="content"></param>
    public GeneralDialog setContent(string content)
    {
        mContentStr = content;
        if (mTXContent!=null)
        mTXContent.text = mContentStr;
        return this;
    }
    
    /// <summary>
    /// 设置确认文字
    /// </summary>
    /// <param name="submitText"></param>
    /// <returns></returns>
    public GeneralDialog setSubmitText(string submitText) {
        mSubmitStr = submitText;
        if (mTXSubmit != null)
            mTXSubmit.text = mSubmitStr;
        return this;
    }

    /// <summary>
    /// 设置取消文字
    /// </summary>
    /// <param name="cancelText"></param>
    /// <returns></returns>
    public GeneralDialog setCancelText(string cancelText) {
        mCancelStr = cancelText;
        if (mTXCancel != null)
            mTXCancel.text = mCancelStr;
        return this;
    }

    /// <summary>
    /// 设置回调
    /// </summary>
    /// <param name="callBack"></param>
    public GeneralDialog setCallBack(CallBack callBack)
    {
        this.mCallBack = callBack;
        return this;
    }

    /// <summary>
    /// 退出Dialog
    /// </summary>
    public void cancel()
    {
        if (mCallBack != null)
            mCallBack.cancelClick();
        Destroy(gameObject);
    }

    /// <summary>
    /// 确认按钮点击
    /// </summary>
    private void submit()
    {
        if (mCallBack != null)
            mCallBack.submitClick();
        Destroy(gameObject);
    }

    public interface CallBack
    {
        void submitClick();

        void cancelClick();
    }
}
