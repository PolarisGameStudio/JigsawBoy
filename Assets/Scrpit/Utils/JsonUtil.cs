using UnityEngine;

public class JsonUtil : ScriptableObject
{
    public static T FromJson<T>(string strData)
    {
        T dataBean = JsonUtility.FromJson<T>(strData);
        return dataBean;
    }

    public static string ToJson<T>(T dataBean)
    {
        string json = JsonUtility.ToJson(dataBean);
        return json;
    }

}