using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JigsawSelect : BaseMonoBehaviour
{

    private JigsawResourcesEnum resourcesType;
    private static string JigsawSelectItemPath = "Prefab/UI/JigsawSelectItem";

    // Use this for initialization
    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 设置拼图选择类型
    /// </summary>
    /// <param name="resourcesType"></param>
    public void setResourcesType(JigsawResourcesEnum resourcesType)
    {
        this.resourcesType = resourcesType;
        loadJigsaw(resourcesType);
    }

    /// <summary>
    /// 读取拼图信息
    /// </summary>
    /// <param name="resourcesEnum"></param>
    public void loadJigsaw(JigsawResourcesEnum resourcesEnum)
    {
        resourcesType = resourcesEnum;
        //加载该类型下所有拼图数据
        JigsawResourcesBean resourcesData = JigsawDataLoadUtil.loadAllJigsawDataByType(resourcesEnum);
        if (resourcesData == null || resourcesData.dataList == null)
            return;
        int resourcesListCount = resourcesData.dataList.Count;
        for (int itemPosition = 0; itemPosition < resourcesListCount; itemPosition++)
        {
            JigsawResInfoBean itemInfo = resourcesData.dataList[itemPosition];
            itemInfo.resFilePath = resourcesData.dataFilePath + itemInfo.markFileName;
            createSelectItem(itemInfo);
        }
    }

    /// <summary>
    /// 创建相对应按钮
    /// </summary>
    /// <param name="itemInfo"></param>
    private void createSelectItem(JigsawResInfoBean itemInfo)
    {
        GameObject buttonObj = Instantiate(Resources.Load(JigsawSelectItemPath)) as GameObject;
        buttonObj.name = itemInfo.markFileName;
        buttonObj.transform.parent = transform;

        //设置背景图片
        Image backImage = buttonObj.GetComponent<Image>();
        string filePath = itemInfo.resFilePath;
        Sprite backSp = Resources.Load(filePath, typeof(Sprite)) as Sprite;
        backImage.sprite = backSp;

        //设置按键
        Button itemBT = buttonObj.GetComponent<Button>();
        itemBT.onClick.AddListener(delegate ()
        {
           CommonData.selectJigsawInfo = itemInfo;
           SceneUtil.jumpGameScene();
        });


        //设置文本信息
        Text[] allText = buttonObj.GetComponentsInChildren<Text>();
        if (allText != null)
        {
            int allTextSize = allText.Length;
            for (int textPosition = 0; textPosition < allTextSize; textPosition++)
            {
                Text textItem = allText[textPosition];
                if (textItem.name.Equals("JigsawName"))
                {
                    textItem.text = itemInfo.details.workOfName;
                }
            }
        }


    }


}
