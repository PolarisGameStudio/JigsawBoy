using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class MenuSelectUIControl : BaseMonoBehaviour
{
    public Canvas menuSelectUICanvas;

    public ScrollRect resTypeSelectView;
    public Transform resTypeSelectContentTF;
    public JigsawResTypeSelect resTypeSelectContentSC;

    public ScrollRect jigsawSelectView;
    public Transform jigsawSelectContentTF;
    public JigsawSelect jigsawSelectContentSC;

    void Start()
    {
        menuSelectUICanvas = GetComponent<Canvas>();

        //初始化拼图选择类型数据
        resTypeSelectView = CptUtil.getCptFormParentByName<Transform, ScrollRect>(transform, "ResTypeSelectView");
        resTypeSelectContentTF = CptUtil.getCptFormParentByName<ScrollRect, Transform>(resTypeSelectView, "Content");
        if (resTypeSelectContentTF != null)
        {
            resTypeSelectContentSC = resTypeSelectContentTF.gameObject.AddComponent<JigsawResTypeSelect>();
            resTypeSelectContentSC.setMenuSelectUIControl(this);
            resTypeSelectContentSC.loadResTypeData();
        }
        //初始化拼图选择数据
        jigsawSelectView= CptUtil.getCptFormParentByName<Transform, ScrollRect>(transform, "JigsawSelectView");
        jigsawSelectContentTF = CptUtil.getCptFormParentByName<ScrollRect, Transform>(jigsawSelectView, "Content");
        if (jigsawSelectContentTF != null)
        {
            jigsawSelectContentSC = jigsawSelectContentTF.gameObject.AddComponent<JigsawSelect>();
            jigsawSelectContentSC.setMenuSelectUIControl(this);
            jigsawSelectContentSC.loadJigsaw(JigsawResourcesEnum.Painting);
        }

        menuSelectUICanvas.enabled = false;
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
}

