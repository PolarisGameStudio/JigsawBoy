using UnityEngine;

public class EnumUtil 
{

    public static void getResTypeInfoForName(JigsawResourcesEnum resourcesEnum, out string resName)
    {
        string resTypeIconPath;
        getResTypeInfo(resourcesEnum, out resName, out resTypeIconPath);
    }
    public static void getResTypeInfoForPath(JigsawResourcesEnum resourcesEnum, out string resTypeIconPath)
    {
        string resName;
        getResTypeInfo(resourcesEnum, out resName, out resTypeIconPath);
    }
    /// <summary>
    /// 获取资源类型
    /// </summary>
    /// <param name="resourcesEnum"></param>
    /// <returns></returns>
    public static void getResTypeInfo(JigsawResourcesEnum resourcesEnum,out string resName,out string resTypeIconPath)
    {
        switch (resourcesEnum)
        {
            case JigsawResourcesEnum.Painting:
                resName = CommonData.getText(5);
                resTypeIconPath = "Texture/UI/tab_painting";
                break;
            case JigsawResourcesEnum.Scenery:
                resName = CommonData.getText(6);
                resTypeIconPath = "Texture/UI/tab_scenery";
                break;
            case JigsawResourcesEnum.Custom:
                resName = CommonData.getText(7);
                resTypeIconPath = "Texture/UI/tab_custom";
                break;
            case JigsawResourcesEnum.Celebrity:
                resName = CommonData.getText(8);
                resTypeIconPath = "Texture/UI/tab_celebrity";
                break;
            case JigsawResourcesEnum.Other:
                resName = CommonData.getText(9);
                resTypeIconPath = "Texture/UI/tab_other";
                break;
            case JigsawResourcesEnum.Animal:
                resName = CommonData.getText(10);
                resTypeIconPath = "Texture/UI/tab_animal";
                break;
            case JigsawResourcesEnum.Food:
                resName = CommonData.getText(31);
                resTypeIconPath = "Texture/UI/tab_food";
                break;
            case JigsawResourcesEnum.StarrySky:
                resName = CommonData.getText(12);
                resTypeIconPath = "Texture/UI/tab_starrysky";
                break;
            //    case JigsawResourcesEnum.Movie:
            //       resName = CommonData.getText(11);
            //      break;
            default:
                resName = "未知";
                resTypeIconPath = "";
                break;
        }
    }

    public static void getEquipColor(EquipInfoBean data,EquipColorEnum colorEnum)
    {
        if (data == null)
            return;
        string colorStr=null;
        string colorName = null;
        switch (colorEnum)
        {
            case EquipColorEnum.Def:
                colorStr = "#FFFFFF";
                colorName = CommonData.getText(91);
                break;
            case EquipColorEnum.Black:
                colorStr = "#000000";
                colorName = CommonData.getText(96);
                break;
            case EquipColorEnum.Twilight:
                colorStr = "#f7acbc";
                colorName = CommonData.getText(97);
                break;
            case EquipColorEnum.Rose:
                colorStr = "#f05b72";
                colorName = CommonData.getText(98);
                break;
            case EquipColorEnum.Coral:
                colorStr = "#f8aba6";
                colorName = CommonData.getText(99);
                break;
            case EquipColorEnum.Red:
                colorStr = "#d71345";
                colorName = CommonData.getText(100);
                break;
            case EquipColorEnum.Vermilion:
                colorStr = "#f26522";
                colorName = CommonData.getText(101);
                break;
            case EquipColorEnum.Sunflower:
                colorStr = "#ffc20e";
                colorName = CommonData.getText(102);
                break;
            case EquipColorEnum.YellowGreen:
                colorStr = "#b2d235";
                colorName = CommonData.getText(103);
                break;
            case EquipColorEnum.Green:
                colorStr = "#45b97c";
                colorName = CommonData.getText(104);
                break;
            case EquipColorEnum.Blue:
                colorStr = "#009ad6";
                colorName = CommonData.getText(105);
                break;
            case EquipColorEnum.Purple:
                colorStr = "#8552a1";
                colorName = CommonData.getText(106);
                break;
            case EquipColorEnum.Skin:
                colorStr = "#fedcbd";
                colorName = CommonData.getText(111);
                break;
            case EquipColorEnum.Orange:
                colorStr = "#faa755";
                colorName = CommonData.getText(112);
                break;
            case EquipColorEnum.Oil:
                colorStr = "#817936";
                colorName = CommonData.getText(113);
                break;
            case EquipColorEnum.Tea:
                colorStr = "#2e3a1f";
                colorName = CommonData.getText(114);
                break;
            case EquipColorEnum.WhiteGreen:
                colorStr = "#cde6c7";
                colorName = CommonData.getText(115);
                break;
            case EquipColorEnum.Grape:
                colorStr = "#472d56";
                colorName = CommonData.getText(116);
                break;
        }
        data.equipImageColor = colorStr;
        data.equipName = colorName;
    }
}


