using UnityEngine;
using UnityEditor;

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

}