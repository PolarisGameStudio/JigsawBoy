using UnityEngine;
using System.Collections.Generic;
using System;
using System.Reflection;

public class ReflexUtil : ScriptableObject
{
    /// <summary>
    /// 根据反射获取所有属性名称
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="classType"></param>
    /// <returns></returns>
    public static List<string> getAllName<T>(T classType)
    {
        List<string> listName = new List<string>();
        Type type = classType.GetType();
        PropertyInfo[] propertyInfos = type.GetProperties();

        if (propertyInfos == null)
            return listName;

        int propertyInfoSize = propertyInfos.Length;
        for (int i = 0; i < propertyInfoSize; i++)
        {
            PropertyInfo propertyInfo = propertyInfos[i];
            listName.Add(propertyInfo.Name);
        };
        return listName;
    }

    /// <summary>
    /// 根据反射 设置值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="classType"></param>
    /// <param name="name"></param>
    /// <param name="value"></param>
   public static void setValueByName<T>(T classType,string name,object value)
    {
        Type type = classType.GetType();
        PropertyInfo propertyInfo = type.GetProperty(name);

        if (propertyInfo == null)
            return;
        propertyInfo.SetValue(classType,value,null);
    }
}