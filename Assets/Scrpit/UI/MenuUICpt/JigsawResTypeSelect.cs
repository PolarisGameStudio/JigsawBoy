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
        string resTypeIconPath;
        string resName;
        EnumUtil.getResTypeInfo(resType, out resName,out resTypeIconPath);
        if (resName == null || resName.Length == 0 || resTypeIconPath == null || resTypeIconPath.Length == 0)
            return;
        GameObject buttonObj = Instantiate(ResourcesManager.LoadData<GameObject>(ResTypeSelectItemPath));

        //设置大小
        RectTransform rect= buttonObj.GetComponent<RectTransform>();
        float itemWith = transform.GetComponent<RectTransform>().rect.width;
        float itemHight = transform.GetComponent<RectTransform>().rect.width * 0.5f;
        rect.sizeDelta=new Vector2(itemWith, itemHight);
        
        TabButton tabButton = buttonObj.GetComponent<TabButton>();
        buttonObj.name = resType.ToString();
        buttonObj.transform.SetParent(transform);
        buttonObj.transform.localScale = new Vector3(1, 1, 1);
        tabButton.setResType(resType);
        //设置按键
        Button selectBT = buttonObj.GetComponent<Button>();
        selectBT.onClick.AddListener(delegate ()
        {
          SoundUtil.playSoundClip(AudioButtonOnClickEnum.btn_sound_1);
          TabButton[] listTab= transform.GetComponentsInChildren<TabButton>();
            foreach(TabButton itemTab in listTab)
            {
                if (tabButton != null && tabButton == itemTab)
                    itemTab.setSelect(true);
                else
                    itemTab.setSelect(false);
            }
            menuSelectUIControl.setJigsawSelectData(resType);
        });

        //设置文本信息
        Text resTypeNameTV = CptUtil.getCptFormParentByName<Transform, Text>(buttonObj.transform, "ResTypeName");
        resTypeNameTV.text = resName;

        //设置图片信息
        Image resTypeIcon= CptUtil.getCptFormParentByName<Transform, Image>(buttonObj.transform, "ResTypeIcon");
        StartCoroutine(ResourcesManager.LoadAsyncDataImage(resTypeIconPath, resTypeIcon));
    }
}

