using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JigsawDataLoadUtil
{
    private static string PaintingDataPath = "PuzzlesPic/PaintingData_";
    private static string MovieDataPath = "PuzzlesPic/MovieData_";
    private static string CelebrityDataPath = "PuzzlesPic/CelebrityData_";

    /// <summary>
    /// 获取拼图图片数据
    /// </summary>
    /// <param name="language"></param>
    /// <param name="resourcesType"></param>
    /// <returns></returns>
    public static JigsawResourcesBean loadAllJigsawDataByType(JigsawResourcesEnum resourcesType)
    {
        JigsawResourcesBean resourcesList = new JigsawResourcesBean();
        GameLanguageEnum language = CommonData.gameLanguage;

        string fileName = "";
        if (resourcesType.Equals(JigsawResourcesEnum.Painting))
            fileName = PaintingDataPath;
        else if (resourcesType.Equals(JigsawResourcesEnum.Movie))
            fileName = MovieDataPath;
        else if (resourcesType.Equals(JigsawResourcesEnum.Celebrity))
            fileName = CelebrityDataPath;
        else
            return null;


        if (language.Equals(GameLanguageEnum.Chinese))
        {
            resourcesList = startLoad(fileName + "CN");
        }
        else if (language.Equals(GameLanguageEnum.English))
        {
            resourcesList = startLoad(fileName + "EN");
        }
        else
        {
            return null;
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
