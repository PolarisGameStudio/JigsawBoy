using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuSelectUIControl : BaseUIControl
{
    public ScrollRect resTypeSelectView;
    public Transform resTypeSelectContentTF;
    public JigsawResTypeSelect resTypeSelectContentSC;

    public ScrollRect jigsawSelectView;
    public Transform jigsawSelectContentTF;
    public JigsawSelect jigsawSelectContentSC;

    public Transform jigsawSelectTiltebar;
    public Button titleBarExitBT;
    public Text titleBarJigsawPointTV;


    public Transform addCustomJigsaw;
    public Button addCustomJigsawBT;

    private JigsawResourcesEnum currentResType;//当前选择数据类型

    private new void Awake()
    {
        base.Awake();

        //初始化拼图选择类型数据
        resTypeSelectView = CptUtil.getCptFormParentByName<Transform, ScrollRect>(transform, "ResTypeSelectView");
        resTypeSelectContentTF = CptUtil.getCptFormParentByName<ScrollRect, Transform>(resTypeSelectView, "Content");
        if (resTypeSelectContentTF != null)
        {
            resTypeSelectContentSC = resTypeSelectContentTF.gameObject.AddComponent<JigsawResTypeSelect>();
            resTypeSelectContentSC.setMenuSelectUIControl(this);
        }

        //初始化拼图选择数据
        jigsawSelectView = CptUtil.getCptFormParentByName<Transform, ScrollRect>(transform, "JigsawSelectView");
        jigsawSelectContentTF = CptUtil.getCptFormParentByName<ScrollRect, Transform>(jigsawSelectView, "Content");
        if (jigsawSelectContentTF != null)
        {
            jigsawSelectContentSC = jigsawSelectContentTF.gameObject.AddComponent<JigsawSelect>();
            jigsawSelectContentSC.setMenuSelectUIControl(this);
        }

        //初始化标题栏
        jigsawSelectTiltebar = CptUtil.getCptFormParentByName<Transform, Transform>(transform, "TitleBar");
        titleBarExitBT = CptUtil.getCptFormParentByName<Transform, Button>(jigsawSelectTiltebar, "ExitBT");
        titleBarJigsawPointTV = CptUtil.getCptFormParentByName<Transform, Text>(jigsawSelectTiltebar, "JigsawPoint");
        if (titleBarExitBT != null) {
            titleBarExitBT.onClick.AddListener(addExitOnClick);
        }

        //按钮增加
        addCustomJigsaw = CptUtil.getCptFormParentByName<Transform, Transform>(transform, "AddCustomJigsaw");
        addCustomJigsawBT = addCustomJigsaw.GetComponent<Button>();
        if (addCustomJigsaw != null)
        {
            addCustomJigsaw.gameObject.SetActive(false);
        }
        if (addCustomJigsaw != null)
        {
            addCustomJigsawBT.onClick.AddListener(addCustomJigsawOnClick);
        }
    }

    /// <summary>
    /// 设置拼图类型选择数据
    /// </summary>
    /// <param name="resTypeSelectView"></param>
    public void setJigsawSelectData(JigsawResourcesEnum resourcesEnum)
    {
        this.currentResType = resourcesEnum;
        if (jigsawSelectContentSC == null)
            return;
        jigsawSelectContentSC.loadJigsaw(resourcesEnum);
    }

    /// <summary>
    /// 刷新当前数据
    /// </summary>
    public void refreshJigsawSelectData()
    {
        setJigsawSelectData(currentResType);
    }

    public override void openUI()
    {
        mUICanvas.enabled = true;
        loadUIData();
    }

    public override void closeUI()
    {
        mUICanvas.enabled = false;
    }

    public override void loadUIData()
    {
        if (resTypeSelectContentSC != null)
            resTypeSelectContentSC.loadResTypeData();
        setJigsawSelectData(JigsawResourcesEnum.Painting);
    }
    
    /// <summary>
    /// 增加自定义拼图按钮
    /// </summary>
    public void addCustomJigsawOnClick()
    {
       mUIMasterControl.openUIByTypeAndCloseOther(UIEnum.MenuCustomUpLoadUI);
    }

    /// <summary>
    /// 增加退出按钮
    /// </summary>
    public void addExitOnClick()
    {
        mUIMasterControl.openUIByTypeAndCloseOther(UIEnum.MenuMainUI);
    }

    /// <summary>
    /// 是否展示自定义按钮
    /// </summary>
    /// <param name="isShow"></param>
    public void showAddCustomJigsaw(bool isShow)
    {
        if (addCustomJigsaw != null)
        {
           addCustomJigsaw.gameObject.SetActive(isShow);
        }
    }
}

