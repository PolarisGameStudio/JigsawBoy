using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesManager
{
    /// <summary>
    /// 加载资源
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="resPath"></param>
    /// <returns></returns>
    public static T loadData<T>(string resPath) where T : Object
    {
        T resData = Resources.Load(resPath,typeof(T)) as T;
        return resData;
    }

    /// <summary>
    /// 加载本地文件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="resPath"></param>
    /// <returns></returns>
    public static WWW loadLocationData(string resPath)
    {
        string filePath = "file://" + resPath;
        WWW www = new WWW(filePath);
        return www;
    }


    /// <summary>
    /// 异步加载图片
    /// </summary>
    /// <param name="imagePath"></param>
    /// <param name="image"></param>
    /// <returns></returns>
    public static IEnumerator loadLocationImage(string imagePath, Image image)
    {
        string filePath = "file://" + imagePath;
        WWW www = new WWW(filePath);
        yield return www;
        image.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0.5f, 0.5f)); ;
    }
}