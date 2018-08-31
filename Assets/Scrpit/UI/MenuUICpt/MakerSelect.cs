using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections.Generic;

public class MakerSelect : BaseMonoBehaviour
{
    private static string MakerItemPath = "Prefab/UI/Menu/MakerItem";
    /// <summary>
    /// 读取制作者数据
    /// </summary>
    public void loadMakerData()
    {
        //删除原数据
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        List<MakerDataBean> listItemData = createData();
        foreach (MakerDataBean itemData in listItemData)
        {
            createMakerItem(itemData);
        }
    }

    /// <summary>
    /// 初始化数据
    /// </summary>
    /// <returns></returns>
    private List<MakerDataBean> createData()
    {
        List<MakerDataBean> listItemData = new List<MakerDataBean>();
        new MakerDataBean("Game Design", "Apple Coffee", listItemData);
        new MakerDataBean("Game Programmer", "Apple Coffee", listItemData);
        new MakerDataBean("Game Tester", "Apple Coffee , JaimeInes , 番茄", listItemData);
        new MakerDataBean("Game Publicizer", "Apple Coffee , 番茄", listItemData);
        new MakerDataBean("UI Design", "Apple Coffee", listItemData);
        return listItemData;
    }

    /// <summary>
    /// 创建ITEM
    /// </summary>
    /// <param name="itemData"></param>
    private GameObject createMakerItem(MakerDataBean itemData)
    {
        GameObject itemObj = Instantiate(ResourcesManager.LoadData<GameObject>(MakerItemPath));

        //设置大小
        RectTransform rect = itemObj.GetComponent<RectTransform>();
        float itemWith = transform.GetComponent<RectTransform>().rect.width;
        float itemHight = transform.GetComponent<RectTransform>().rect.width * 0.1f;
        rect.sizeDelta = new Vector2(itemWith, itemHight);

        itemObj.name = itemData.makerTitle;
        itemObj.transform.SetParent(transform);
        itemObj.transform.localScale = new Vector3(1, 1, 1);

        Text markTitle = CptUtil.getCptFormParentByName<RectTransform, Text>(rect,"MakerTitle");
        Text markName = CptUtil.getCptFormParentByName<RectTransform, Text>(rect, "MakerName");

        markTitle.text = itemData.makerTitle;
        markName.text = itemData.makeName;
        return itemObj;
    }
}