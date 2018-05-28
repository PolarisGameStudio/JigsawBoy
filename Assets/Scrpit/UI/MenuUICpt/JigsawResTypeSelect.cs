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
        String resTypeIconPath = "";
        String resName = "";
        bool isDef = false;
        switch (resType)
        {
            case JigsawResourcesEnum.Painting:
                resTypeIconPath = "Texture/UI/tab_painting";
                resName = CommonData.getText(5);
                break;
            case JigsawResourcesEnum.Scenery:
                resTypeIconPath = "Texture/UI/tab_scenery";
                resName = CommonData.getText(6);
                break;
            case JigsawResourcesEnum.Custom:
                resTypeIconPath = "Texture/UI/tab_custom";
                resName = CommonData.getText(7);
                break;
            case JigsawResourcesEnum.Celebrity:
                resTypeIconPath = "Texture/UI/tab_celebrity"; 
                resName = CommonData.getText(8);
                break;
            case JigsawResourcesEnum.Other:
                resTypeIconPath = "Texture/UI/tab_other";
                resName = CommonData.getText(9);
                break;
            case JigsawResourcesEnum.Animal:
                resTypeIconPath = "Texture/UI/tab_animal";
                resName = CommonData.getText(10);
                break;
            //case JigsawResourcesEnum.Movie:
            //    resName = CommonData.getText(11);
            //    break;
            //case JigsawResourcesEnum.StarrySky:
            //    resName = CommonData.getText(12);
            //    break;
            default:
                isDef = true;
                break;
        }
        if (isDef)
            return;
       

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
        resTypeNameTV.text = resName;

        //设置图片信息
        Image resTypeIcon= CptUtil.getCptFormParentByName<Transform, Image>(buttonObj.transform, "ResTypeIcon");
        Sprite resTypeIconSp = ResourcesManager.loadData<Sprite>(resTypeIconPath);
        resTypeIcon.sprite=resTypeIconSp;

    }
}

