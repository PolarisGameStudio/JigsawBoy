using UnityEngine;

public class EnumUtil 
{
    /// <summary>
    /// 获取资源类型名称
    /// </summary>
    /// <param name="resourcesEnum"></param>
    /// <returns></returns>
    public static string getResTypeName(JigsawResourcesEnum resourcesEnum)
    {
        string resName = "";
        switch (resourcesEnum)
        {
            case JigsawResourcesEnum.Painting:
                resName = CommonData.getText(5);
                break;
            case JigsawResourcesEnum.Scenery:
                resName = CommonData.getText(6);
                break;
            case JigsawResourcesEnum.Custom:
                resName = CommonData.getText(7);
                break;
            case JigsawResourcesEnum.Celebrity:
                resName = CommonData.getText(8);
                break;
            case JigsawResourcesEnum.Other:
                resName = CommonData.getText(9);
                break;
            case JigsawResourcesEnum.Animal:
                resName = CommonData.getText(10);
                break;
            case JigsawResourcesEnum.Food:
                resName = CommonData.getText(31);
                break;
            case JigsawResourcesEnum.StarrySky:
                resName = CommonData.getText(12);
                break;
            //    case JigsawResourcesEnum.Movie:
            //       resName = CommonData.getText(11);
            //      break;
            default:
                break;
        }
        return resName;
    }

    /// <summary>
    /// 获取资源类型地址
    /// </summary>
    /// <param name="resourcesEnum"></param>
    /// <returns></returns>
    public static string getResTypeUIPath(JigsawResourcesEnum resourcesEnum)
    {
        string resTypeIconPath = "";
        switch (resourcesEnum)
        {
            case JigsawResourcesEnum.Painting:
                resTypeIconPath = "Texture/UI/tab_painting";
                break;
            case JigsawResourcesEnum.Scenery:
                resTypeIconPath = "Texture/UI/tab_scenery";
                break;
            case JigsawResourcesEnum.Custom:
                resTypeIconPath = "Texture/UI/tab_custom";
                break;
            case JigsawResourcesEnum.Celebrity:
                resTypeIconPath = "Texture/UI/tab_celebrity";
                break;
            case JigsawResourcesEnum.Other:
                resTypeIconPath = "Texture/UI/tab_other";
                break;
            case JigsawResourcesEnum.Animal:
                resTypeIconPath = "Texture/UI/tab_animal";
                break;
            case JigsawResourcesEnum.Food:
                resTypeIconPath = "Texture/UI/tab_food";
                break;
            case JigsawResourcesEnum.StarrySky:
                resTypeIconPath = "Texture/UI/tab_starrysky";
                break;
            //   case JigsawResourcesEnum.Movie:
            //     break;

            default:
                break;
        }
        return resTypeIconPath;
    }
}


