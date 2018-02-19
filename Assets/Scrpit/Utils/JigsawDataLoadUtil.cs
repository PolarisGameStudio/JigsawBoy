using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JigsawDataLoadUtil
{

    private static string CustomData = "CustomData_";
    private static string PaintingData = "Painting/PaintingData_";

    /// <summary>
    /// 获取拼图图片数据
    /// </summary>
    /// <param name="language"></param>
    /// <param name="resourcesType"></param>
    /// <returns></returns>
    public static JigsawResourcesBean loadAllJigsawDataByType(GameLanguageEnum language, JigsawResourcesEnum resourcesType)
    {
        JigsawResourcesBean resourcesList = new JigsawResourcesBean();
        string fileName = "";
        if (resourcesType.Equals(JigsawResourcesEnum.Custom))
            fileName = CustomData;
        else if (resourcesType.Equals(JigsawResourcesEnum.Painting))
            fileName = PaintingData;

        if (language.Equals(GameLanguageEnum.Chinese))
        {
            resourcesList = startLoad(fileName + "CN");
        }
        else if (language.Equals(GameLanguageEnum.English))
        {
            resourcesList = startLoad(fileName + "EN");
        }
        return resourcesList;
    }


    //开始加载
    private static JigsawResourcesBean startLoad(string fileName)
    {
        TextAsset jsonData = Resources.Load<TextAsset>(fileName);
        JigsawResourcesBean listData= JsonUtility.FromJson<JigsawResourcesBean>(jsonData.text);
        return listData;
    }
}
