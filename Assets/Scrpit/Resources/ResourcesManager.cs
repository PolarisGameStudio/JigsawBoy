using System.Collections;
using System.Threading.Tasks;
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

        T resData = Resources.Load(resPath, typeof(T)) as T;
        return resData;
    }

    /// <summary>
    /// 异步加载资源
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="resPath"></param>
    /// <returns></returns>
    public static T loadAsyncData<T>(string resPath) where T : Object
    {
        T resData = Resources.LoadAsync(resPath, typeof(T)) as T;
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
    /// 异步加载本地图片
    /// </summary>
    /// <param name="imagePath"></param>
    /// <param name="image"></param>
    /// <returns></returns>
    public static IEnumerator loadAsyncLocationImage(string imagePath, Image image)
    {
        return loadAsyncBaseImage(1, imagePath, image);
    }

    /// <summary>
    /// 异步加载网络图片
    /// </summary>
    /// <param name="imagePath"></param>
    /// <param name="image"></param>
    /// <returns></returns>
    public static IEnumerator loadAsyncHttpImage(string imagePath, Image image)
    {
        return loadAsyncBaseImage(0, imagePath, image);
    }

    /// <summary>
    /// 基础加载
    /// </summary>
    /// <param name="type">1.本地   0.网络</param>
    /// <param name="imagePath"></param>
    /// <param name="image"></param>
    /// <returns></returns>
    public static IEnumerator loadAsyncBaseImage(int type, string imagePath, Image image)
    {
        string filePath = "file://" + imagePath;
        if (type == 1)
        {
            filePath = "file://" + imagePath;
        }
        else
        {
            filePath = imagePath;
        }
        WWW www = new WWW(filePath);
        yield return www;
        image.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0.5f, 0.5f));
    }

    /// <summary>
    /// 异步加载资源图片
    /// </summary>
    /// <param name="imagePath"></param>
    /// <param name="image"></param>
    /// <returns></returns>
    public static IEnumerator loadAsyncDataImage(string imagePath, Image image)
    {
        ResourceRequest res = Resources.LoadAsync<Sprite>(imagePath);
        yield return res;
        Sprite imageSp = res.asset as Sprite;
        image.sprite = imageSp;
       // image.sprite = Sprite.Create(imageTX, new Rect(0, 0, imageTX.width, imageTX.height), new Vector2(0.5f, 0.5f));
    }

    /// <summary>
    /// 异步加载资源图片
    /// </summary>
    /// <param name="imagePath"></param>
    /// <param name="image"></param>
    /// <returns></returns>
    public async static Task loadAsyncDataImageByAwait(string imagePath, Image image)
    {
        var res = await Resources.LoadAsync<Sprite>(imagePath);
        Sprite imageSp= res as Sprite;
        image.sprite = imageSp;

    }
}