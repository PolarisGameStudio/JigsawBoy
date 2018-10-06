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
    public Text titleBarTitleName;

   

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
        titleBarJigsawPointTV = CptUtil.getCptFormParentByName<Transform, Text>(jigsawSelectTiltebar, "PuzzlesPointText");
        titleBarTitleName = CptUtil.getCptFormParentByName<Transform, Text>(jigsawSelectTiltebar,"TitleName");
        if (titleBarExitBT != null)
        {
            titleBarExitBT.onClick.AddListener(addExitOnClick);
        }

        refreshUI();
    }

    /// <summary>
    /// 设置拼图类型选择数据
    /// </summary>
    /// <param name="resTypeSelectView"></param>
    public void setJigsawSelectData(JigsawResourcesEnum resourcesEnum)
    {
        titleBarTitleName.text = EnumUtil.getResTypeName(resourcesEnum);
        this.currentResType = resourcesEnum;
        if (jigsawSelectContentSC == null)
            return;
        jigsawSelectContentSC.loadJigsaw(resourcesEnum);
        if (resTypeSelectContentSC == null)
            return;
        TabButton[] tabList= resTypeSelectContentSC.GetComponentsInChildren<TabButton>();
        foreach (TabButton item in tabList) {
            if (item.getResType().Equals(resourcesEnum))
                item.setSelect(true);
            else
                item.setSelect(false);
        }
    }

    /// <summary>
    /// 刷新当前数据
    /// </summary>
    public void refreshJigsawSelectData()
    {
        setJigsawSelectData(currentResType);
    }

    /// <summary>
    /// 刷新制定ITEM
    /// </summary>
    public void refreshItemJigsawSelectData(int position,GameObject itemObj, PuzzlesGameInfoBean itemInfo)
    {
        GameObject newItem= jigsawSelectContentSC.createNormalItem(itemInfo);
        newItem.transform.SetParent(jigsawSelectContentTF);
        newItem.transform.SetSiblingIndex(position);
        Destroy(itemObj);
   
    }

    /// <summary>
    /// 刷新拼图点数
    /// </summary>
    public void refreshPuzzlesPoint()
    {
        titleBarJigsawPointTV.text = "x" + DataStorageManage.getUserInfoDSHandle().getData(0).puzzlesPoint + " PP";
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
        if (titleBarJigsawPointTV != null)
            refreshPuzzlesPoint();

        if (resTypeSelectContentSC != null)
            resTypeSelectContentSC.loadResTypeData();
   
        setJigsawSelectData(JigsawResourcesEnum.Painting);

        refreshPuzzlesPoint();
    }

    public override void refreshUI()
    {
        if (titleBarJigsawPointTV != null)
        {
            refreshPuzzlesPoint();
        }
    }

    /// <summary>
    /// 增加退出按钮监听
    /// </summary>
    public void addExitOnClick()
    {
        SoundUtil.playSoundClip(AudioButtonOnClickEnum.btn_sound_2);
        StopAllCoroutines();
        mUIMasterControl.openUIByTypeAndCloseOther(UIEnum.MenuMainUI);
    }


}

