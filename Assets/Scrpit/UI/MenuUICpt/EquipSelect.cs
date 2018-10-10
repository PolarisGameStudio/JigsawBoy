using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;

public class EquipSelect : BaseMonoBehaviour
{

    private static string s_ResTypeSelectItemPath = "Prefab/UI/Menu/EquipSelectItem";
    private MenuEquipUIControl m_EquipUIControl;
    private EquipTypeEnum m_CurrentType;
    /// <summary>
    /// 设置UI控制器
    /// </summary>
    /// <param name="menuSelectUIControl"></param>
    public void setMenuSelectUIControl(MenuEquipUIControl equipUIControl)
    {
        this.m_EquipUIControl = equipUIControl;
    }

    /// <summary>
    /// 拼图形状选择
    /// </summary>
    public void selectPuzzlesShape(Transform content)
    {
        m_CurrentType = EquipTypeEnum.PuzzlesShape;

        EquipInfoBean defaultEquip = new EquipInfoBean();
        defaultEquip.equipName = CommonData.getText(91);
        defaultEquip.equipType = (int)m_CurrentType;
        defaultEquip.equipTypeId = (int)JigsawStyleEnum.Def;
        defaultEquip.equipImageUrl = "Texture/UI/icon_equip_puzzlesshape_def";
        createEquipItem(content, defaultEquip);

        EquipInfoBean smoothEquip = new EquipInfoBean();
        smoothEquip.equipName = CommonData.getText(92);
        smoothEquip.equipType = (int)m_CurrentType;
        smoothEquip.equipTypeId = (int)JigsawStyleEnum.Smooth;
        smoothEquip.unlockPoint = 998;
        smoothEquip.equipImageUrl = "Texture/UI/icon_equip_puzzlesshape_smooth";
        createEquipItem(content, smoothEquip);

        EquipInfoBean triangleEquip = new EquipInfoBean();
        triangleEquip.equipName = CommonData.getText(93);
        triangleEquip.equipType = (int)m_CurrentType;
        triangleEquip.equipTypeId = (int)JigsawStyleEnum.Triangle;
        triangleEquip.unlockPoint = 998;
        triangleEquip.equipImageUrl = "Texture/UI/icon_equip_puzzlesshape_triangle";
        createEquipItem(content, triangleEquip);

        //EquipInfoBean trapezoidEquip = new EquipInfoBean();
        //trapezoidEquip.equipName = "梯形";
        //trapezoidEquip.equipType = (int)m_CurrentType;
        //trapezoidEquip.equipTypeId = (int)JigsawStyleEnum.Trapezoid;
        //trapezoidEquip.unlockPoint = 998;
        //trapezoidEquip.equipImageUrl = "Texture/UI/icon_equip_puzzlesshape_triangle";
        //createEquipItem(content, trapezoidEquip);
    }

    /// <summary>
    /// 拼图边框选择
    /// </summary>
    public void selectBorderShape(Transform content)
    {
        m_CurrentType = EquipTypeEnum.BorderShape;

        EquipInfoBean defaultEquip = new EquipInfoBean();
        defaultEquip.equipName = CommonData.getText(91);
        defaultEquip.equipType = (int)m_CurrentType;
        defaultEquip.equipTypeId = 0;
        defaultEquip.equipImageUrl = "Texture/UI/icon_equip_bordershape_def";
        createEquipItem(content, defaultEquip);

        EquipInfoBean circleEquip = new EquipInfoBean();
        circleEquip.equipName = CommonData.getText(94);
        circleEquip.equipType = (int)m_CurrentType;
        circleEquip.equipTypeId = 1;
        circleEquip.unlockPoint = 998;
        circleEquip.equipImageUrl = "Texture/UI/icon_equip_bordershape_circle";
        createEquipItem(content, circleEquip);

        EquipInfoBean squareEquip = new EquipInfoBean();
        squareEquip.equipName = CommonData.getText(95);
        squareEquip.equipType = (int)m_CurrentType;
        squareEquip.equipTypeId = 2;
        squareEquip.unlockPoint = 998;
        squareEquip.equipImageUrl = "Texture/UI/icon_equip_bordershape_square";
        createEquipItem(content, squareEquip);
    }

    /// <summary>
    /// 边框颜色选择
    /// </summary>
    public void selectBorderColor(Transform content)
    {
        m_CurrentType = EquipTypeEnum.BorderColor;
        string equipImage= "Texture/UI/icon_equip_bordercolor";
        initColorEquip(content, m_CurrentType, equipImage);
    }

    /// <summary>
    /// 背景样式选择
    /// </summary>
    public void selectBackground(Transform content)
    {
        m_CurrentType = EquipTypeEnum.Background;
        string equipImage = "Texture/UI/icon_equip_background";
        initColorEquip(content, m_CurrentType, equipImage);
    }

    /// <summary>
    /// 创建obj
    /// </summary>
    /// <param name="content"></param>
    /// <param name="data"></param>
    public void createEquipItem(Transform content, EquipInfoBean data)
    {
        GameObject equipObj = Instantiate(ResourcesManager.LoadData<GameObject>(s_ResTypeSelectItemPath));

        //设置大小
        RectTransform itemRect = equipObj.GetComponent<RectTransform>();
        float itemWith = content.GetComponent<RectTransform>().rect.height * 0.52f;
        float itemHight = content.GetComponent<RectTransform>().rect.height * 0.8f;
        itemRect.sizeDelta = new Vector2(itemWith, itemHight);

        TabButton tabButton = equipObj.GetComponent<TabButton>();
        equipObj.transform.SetParent(content);
        equipObj.transform.SetSiblingIndex((int)data.equipTypeId);
        equipObj.name = data.equipName;
        equipObj.transform.localScale = new Vector3(1, 1, 1);

        //背景
        Image backgroundImage = CptUtil.getCptFormParentByName<Transform, Image>(equipObj.transform, "Background");
        //设置名字
        Text equipName = CptUtil.getCptFormParentByName<Transform, Text>(equipObj.transform, "EquipName");
        equipName.text = data.equipName;

        //设置资源图片
        Image equipImage = CptUtil.getCptFormParentByName<Transform, Image>(equipObj.transform, "EquipImage");
        StartCoroutine(ResourcesManager.LoadAsyncDataImage(data.equipImageUrl, equipImage));
        if (data.equipImageColor != null && data.equipImageColor.Length != 0)
        {
            if (data.equipTypeId == 0)
                data.equipImageColor = "#EFEFEF";
            ColorUtil.setImageColor(equipImage,data.equipImageColor);
        }

        //获取是否锁定
        ((EquipDSHandle)DataStorageManage.getEquipDSHandle()).getData(data);
        Image equipLock = CptUtil.getCptFormParentByName<Transform, Image>(equipObj.transform, "EquipLock");
        Button equipButton = CptUtil.getCptFormParentByName<Transform, Button>(equipObj.transform, "EquipButton");
        Image equipButtonImage= CptUtil.getCptFormParentByName<Transform, Image>(equipObj.transform, "EquipButton");
        Text equipButtonText = CptUtil.getCptFormParentByName<Transform, Text>(equipObj.transform, "EquipButtonText");
        if (data.equipTypeId != 0 && data.unlockType == 0)
        {
            //未解锁处理
            equipLock.enabled = true;
            equipButtonText.text = CommonData.getText(13)+"("+data.unlockPoint+"PP)";
            equipButton.onClick.AddListener(delegate ()
            {
                unlockEquip(content, data, equipObj);
            });
        }
        else
        {
            //已解锁处理
            equipLock.enabled = false;
            int equipId = 0;
            if (data.equipType == (int)EquipTypeEnum.PuzzlesShape)
            {
                equipId = (int)CommonConfigure.PuzzlesShape;
            }
            else if (data.equipType == (int)EquipTypeEnum.BorderShape)
            {
                equipId = (int)CommonConfigure.BorderShape;
            }
            else if (data.equipType == (int)EquipTypeEnum.BorderColor)
            {
                equipId = (int)CommonConfigure.BorderColor;
            }
            else if (data.equipType == (int)EquipTypeEnum.Background)
            {
                equipId = (int)CommonConfigure.Background;
            }

            if (equipId == data.equipTypeId)
            {
                equipButtonText.text = CommonData.getText(90);
                equipButton.onClick.RemoveAllListeners();
                ColorUtil.setImageColor(backgroundImage, "#CCCCCC");
                ColorUtil.setImageColor(equipButtonImage, "#CCCCCC");
            }
            else
            {
                equipButtonText.text = CommonData.getText(89);
                equipButton.onClick.AddListener(delegate ()
                {
                    useEquip(content, data, equipObj);
                });
            }
        }
    }

    /// <summary>
    /// 初始化颜色选项
    /// </summary>
    /// <param name="content"></param>
    /// <param name="type"></param>
    /// <param name="equipImage"></param>
    private void initColorEquip(Transform content,EquipTypeEnum type, string equipImage)
    {
        int colorNumber = Enum.GetNames(typeof(EquipColorEnum)).Length;
        for (int i = 0; i < colorNumber; i++)
        {
            EquipInfoBean itemEquip = new EquipInfoBean();
            itemEquip.equipType = (int)m_CurrentType;
            itemEquip.equipTypeId = i;
            itemEquip.unlockPoint = 100;
            itemEquip.equipImageUrl = equipImage;

            EquipColorEnum colorEnum= (EquipColorEnum)Enum.ToObject(typeof(EquipColorEnum),i);
            EnumUtil.getEquipColor(itemEquip,colorEnum);
            createEquipItem(content, itemEquip);
        }
    }

    /// <summary>
    /// 使用装备
    /// </summary>
    /// <param name="content"></param>
    /// <param name="data"></param>
    /// <param name="oldObj"></param>
    private void useEquip(Transform content, EquipInfoBean data, GameObject oldObj)
    {
        GameConfigureBean configureBean = DataStorageManage.getGameConfigureDSHandle().getData(0);
        if (data.equipType == (int)EquipTypeEnum.PuzzlesShape)
        {
            configureBean.puzzlesShape = (int)data.equipTypeId;
        }
        else if (data.equipType == (int)EquipTypeEnum.BorderShape)
        {
            configureBean.borderShape = (int)data.equipTypeId;
        }
        else if (data.equipType == (int)EquipTypeEnum.BorderColor)
        {
            configureBean.borderColor = (int)data.equipTypeId;
        }
        else if (data.equipType == (int)EquipTypeEnum.Background)
        {
            configureBean.background = (int)data.equipTypeId;
        }
        DataStorageManage.getGameConfigureDSHandle().saveData(configureBean);
        CommonConfigure.refreshData();

        refreshData(content);
    }

    /// <summary>
    /// 解锁装备
    /// </summary>
    /// <param name="data"></param>
    private void unlockEquip(Transform content, EquipInfoBean data, GameObject oldObj)
    {
        SoundUtil.playSoundClip(AudioButtonOnClickEnum.btn_sound_1);
        long userPoint = DataStorageManage.getUserInfoDSHandle().getData(0).puzzlesPoint;
        if (userPoint < data.unlockPoint)
        {
            //如果没有PP则提示不足
            DialogManager.createToastDialog().setToastText(CommonData.getText(16));
        }
        else
        {
            //如果有PP则解锁
            //保存信息
            ((UserInfoDSHandle)DataStorageManage.getUserInfoDSHandle()).decreaseUserPuzzlesPoint(data.unlockPoint);
            m_EquipUIControl.refreshPuzzlesPoint();

            data.unlockType = 1;
            DataStorageManage.getEquipDSHandle().saveData(data);
            createEquipItem(content, data);
            Destroy(oldObj);
        }

    }


    /// <summary>
    /// 刷新数据
    /// </summary>
    /// <param name="content"></param>
    private void refreshData(Transform content)
    {
        //删除原数据
        for (int i = 0; i < content.childCount; i++)
        {
            Destroy(content.GetChild(i).gameObject);
        }
        if (m_CurrentType == EquipTypeEnum.PuzzlesShape)
        {
            selectPuzzlesShape(content);
        }
        else if (m_CurrentType == EquipTypeEnum.BorderShape)
        {
            selectBorderShape(content);
        }
        else if (m_CurrentType == EquipTypeEnum.BorderColor)
        {
            selectBorderColor(content);
        }
        else if (m_CurrentType == EquipTypeEnum.Background)
        {
            selectBackground(content);
        }
    }

}