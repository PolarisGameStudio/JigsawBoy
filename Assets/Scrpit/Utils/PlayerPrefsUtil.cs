using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class PlayerPrefsUtil
{
    /// <summary>
    /// 保存string类型数据
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void setStringData(string key, string value)
    {
        if (key == null || value == null)
            return;
        PlayerPrefs.SetString(key, value);
    }

    /// <summary>
    /// 批量保存string类型数据 
    /// </summary>
    /// <param name="mapData"></param>
    public static void setListStringData(Dictionary<string, string> mapData)
    {
        if (mapData == null || mapData.Count == 0)
            return;
        foreach (var itemData in mapData)
        {
            if (itemData.Key != null && itemData.Value != null)
                setStringData(itemData.Key, itemData.Value);
        }
    }

    /// <summary>
    /// 获取string数据
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string getStringData(string key)
    {
        if (key == null)
            return null;
        return PlayerPrefs.GetString(key);
    }

    /// <summary>
    /// 批量获取string数据
    /// </summary>
    /// <param name="listKey"></param>
    /// <returns></returns>
    public static Dictionary<string, string> getListStringData(List<string> listKey)
    {
        Dictionary<string, string> mapData = new Dictionary<string, string>();
        if (listKey == null || listKey.Count == 0)
            return mapData;
        foreach (string itemKey in listKey)
        {
            string itemValue = getStringData(itemKey);
            if (itemValue == null)
                continue;
            mapData.Add(itemKey, itemValue);
        }
        return mapData;
    }
    //-----------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// 保存INT类型数据
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void setIntData(string key, int value)
    {
        if (key == null)
            return;
        PlayerPrefs.SetInt(key, value);
    }

    /// <summary>
    /// 批量保存INT类型数据
    /// </summary>
    /// <param name="mapData"></param>
    public static void setListIntData(Dictionary<string, int> mapData)
    {
        if (mapData == null || mapData.Count == 0)
            return;
        foreach (var itemData in mapData)
        {
            if (itemData.Key != null)
                setIntData(itemData.Key, itemData.Value);
        }
    }

    /// <summary>
    /// 获取INT数据
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static int getIntData(string key)
    {
        if (key == null)
            return 0;
        return PlayerPrefs.GetInt(key);
    }

    /// <summary>
    /// 批量获取INT数据
    /// </summary>
    /// <param name="listKey"></param>
    /// <returns></returns>
    public static Dictionary<string, int> getListIntData(List<string> listKey)
    {
        Dictionary<string, int> mapData = new Dictionary<string, int>();
        if (listKey == null || listKey.Count == 0)
            return mapData;
        foreach (string itemKey in listKey)
        {
            int itemValue = getIntData(itemKey);
            mapData.Add(itemKey, itemValue);
        }
        return mapData;
    }
    //-----------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// 保存float类型数据
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void setFloatData(string key, float value)
    {
        if (key == null)
            return;
        PlayerPrefs.SetFloat(key, value);
    }

    /// <summary>
    /// 批量保存float类型数据
    /// </summary>
    /// <param name="mapData"></param>
    public static void setListFloatData(Dictionary<string, float> mapData)
    {
        if (mapData == null || mapData.Count == 0)
            return;
        foreach (var itemData in mapData)
        {
            if (itemData.Key != null)
                setFloatData(itemData.Key, itemData.Value);
        }
    }

    /// <summary>
    /// 获取float数据
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static float getFloatData(string key)
    {
        if (key == null)
            return 0;
        return PlayerPrefs.GetFloat(key);
    }

    /// <summary>
    /// 批量获取float数据
    /// </summary>
    /// <param name="listKey"></param>
    /// <returns></returns>
    public static Dictionary<string, float> getFloatIntData(List<string> listKey)
    {
        Dictionary<string, float> mapData = new Dictionary<string, float>();
        if (listKey == null || listKey.Count == 0)
            return mapData;
        foreach (string itemKey in listKey)
        {
            float itemValue = getFloatData(itemKey);
            mapData.Add(itemKey, itemValue);
        }
        return mapData;
    }
    //-----------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// 删除数据
    /// </summary>
    /// <param name="key"></param>
    public static void deleteData(string key)
    {
        if (key == null)
            return;
        PlayerPrefs.DeleteKey(key);
    }

    /// <summary>
    /// 批量删除数据
    /// </summary>
    /// <param name="key"></param>
    public static void deleteListData(List<string> listKey)
    {
        if (listKey == null || listKey.Count == 0)
            return;
        foreach (string itemKey in listKey)
        {
            deleteData(itemKey);
        }
    }

    /// <summary>
    /// 删除所有数据
    /// </summary>
    public static void deleteAllData()
    {
        PlayerPrefs.DeleteAll();
    }
    //-----------------------------------------------------------------------------------------------------------------------------
}

