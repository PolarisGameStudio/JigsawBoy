using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class ColorUtil : ScriptableObject
{
    /// <summary>
    /// 16进制转换颜色
    /// </summary>
    /// <param name="colorStr"></param>
    /// <returns></returns>
    public static Color getColor(string colorStr) {
        Color colorData;
        ColorUtility.TryParseHtmlString(colorStr, out colorData);
        return colorData;
    }

    /// <summary>
    /// 设置image颜色
    /// </summary>
    /// <param name="image"></param>
    /// <param name="colorStr"></param>
    public static void setImageColor(Image image, string colorStr)
    {
        image.color = getColor(colorStr);
    }
}