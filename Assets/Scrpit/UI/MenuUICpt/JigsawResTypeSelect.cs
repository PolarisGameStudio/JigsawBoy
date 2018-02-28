using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        GameObject buttonObj = Instantiate(Resources.Load(ResTypeSelectItemPath)) as GameObject;
        buttonObj.name = resType.ToString();
        buttonObj.transform.parent = transform;

        //设置按键
        Button selectBT = buttonObj.GetComponent<Button>();
        selectBT.onClick.AddListener(delegate ()
        {
            menuSelectUIControl.setJigsawSelectData(resType);
        });

        //设置文本信息
        Text[] allText = buttonObj.GetComponentsInChildren<Text>();
        if (allText != null)
        {
            int allTextSize = allText.Length;
            for (int textPosition = 0; textPosition < allTextSize; textPosition++)
            {
                Text textItem = allText[textPosition];
                if (textItem.name.Equals("ResTypeName"))
                {
                    textItem.text = resType.ToString();
                }
            }
        }
    }
}

