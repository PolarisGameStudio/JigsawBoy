using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;


public class JigsawResTypeSelect : BaseMonoBehaviour
{

    public MenuSelectUIControl menuSelectUIControl;
    private static string ResTypeSelectItemPath = "Prefab/UI/Menu/ResTypeSelectItem";


    /// <summary>
    /// 加载数据
    /// </summary>
    public void loadResTypeData()
    {
        //删除原数据
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        startLoad();
    }


    private void startLoad()
    {
        foreach (JigsawResourcesEnum item in Enum.GetValues(typeof(JigsawResourcesEnum)))
        {
            createSelectItem(item);
        }
    }

    /// <summary>
    /// 设置UI控制器
    /// </summary>
    /// <param name="menuSelectUIControl"></param>
    public void setMenuSelectUIControl(MenuSelectUIControl menuSelectUIControl)
    {
        this.menuSelectUIControl = menuSelectUIControl;
    }

    /// <summary>
    /// 创建按键
    /// </summary>
    /// <param name="resType"></param>
    private void createSelectItem(JigsawResourcesEnum resType)
    {
        GameObject buttonObj = Instantiate(ResourcesManager.loadData<GameObject>(ResTypeSelectItemPath));
        buttonObj.name = resType.ToString();
        buttonObj.transform.SetParent(transform);
        //设置按键
        Button selectBT = buttonObj.GetComponent<Button>();
        selectBT.onClick.AddListener(delegate ()
        {
            menuSelectUIControl.setJigsawSelectData(resType);
            if (resType.Equals(JigsawResourcesEnum.Custom))
            {
                //展示自定义添加按钮
                menuSelectUIControl.showAddCustomJigsaw(true);
            }
            else
            {
                //不展示自定义添加按钮
                menuSelectUIControl.showAddCustomJigsaw(false);
            }
        });

        //设置文本信息
        Text resTypeNameTV = CptUtil.getCptFormParentByName<Transform, Text>(buttonObj.transform, "ResTypeName");
        resTypeNameTV.text = resType.ToString();

        //设置图片信息
        Image resTypeIcon= CptUtil.getCptFormParentByName<Transform, Image>(buttonObj.transform, "ResTypeIcon");
        String resTypeIconPath="";
        switch (resType) {
            case JigsawResourcesEnum.Painting:
                resTypeIconPath = "Texture/UI/tab_painting";
                break;
            case JigsawResourcesEnum.Scenery:
                resTypeIconPath = "Texture/UI/tab_scenery";
                break;
        }
        Sprite resTypeIconSp = ResourcesManager.loadData<Sprite>(resTypeIconPath);
        resTypeIcon.sprite=resTypeIconSp;
    }
}

