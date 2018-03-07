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
    }

    /// <summary>
    /// 设置拼图类型选择数据
    /// </summary>
    /// <param name="resTypeSelectView"></param>
    public void setJigsawSelectData(JigsawResourcesEnum resourcesEnum)
    {
        if (jigsawSelectContentSC == null)
            return;
        jigsawSelectContentSC.loadJigsaw(resourcesEnum);
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
        if (jigsawSelectContentSC != null)
            jigsawSelectContentSC.loadJigsaw(JigsawResourcesEnum.Painting);
    }
}

