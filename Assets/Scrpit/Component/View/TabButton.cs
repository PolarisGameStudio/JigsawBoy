using UnityEngine;

public class TabButton : BaseMonoBehaviour
{
    private bool mIsSelect;
    private Animator mAnimaotr;
    private JigsawResourcesEnum mResType;


    private void Awake()
    {
        initData();
        initView();
    }

    private void initData()
    {
        mIsSelect = false;
    }

    private void initView()
    {
        mAnimaotr = GetComponent<Animator>();
        setSelect(false);
    }

    /// <summary>
    /// 设置类型
    /// </summary>
    /// <param name="resType"></param>
    public void setResType(JigsawResourcesEnum resType)
    {
        mResType = resType;
    }

    /// <summary>
    /// 获取类型
    /// </summary>
    /// <returns></returns>
    public JigsawResourcesEnum getResType()
    {
        return mResType;
    }

    /// <summary>
    /// 设置选中状态
    /// </summary>
    /// <param name="isSelect"></param>
    public void setSelect(bool isSelect)
    {
        mIsSelect = isSelect;
        if(mAnimaotr!=null)
        mAnimaotr.SetBool("Select", isSelect);
    }
}