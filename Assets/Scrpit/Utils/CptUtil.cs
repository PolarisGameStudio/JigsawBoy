using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class CptUtil :BaseMonoBehaviour 
{
    /// <summary>
    /// 从父控件中根据名字获取指定控件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="V"></typeparam>
    /// <param name="parentCpt"></param>
    /// <param name="cptName"></param>
    /// <returns></returns>
    public static V getCptFormParentByName<T, V>(T parentCpt, string cptName) where T : Component where V : Component
    {
        if (parentCpt == null)
            return null;
        if (cptName == null || cptName.Length == 0)
            return null;
        V[] cptList = parentCpt.GetComponentsInChildren<V>();
        if (cptList != null)
        {
            int cptListSize = cptList.Length;
            for (int i = 0; i < cptListSize; i++)
            {
                V cpt = cptList[i];
                if (cpt.name.Equals(cptName))
                    return cpt;
            }
        }
        return null;
    }

    /// <summary>
    /// 从父控件中根据名字获取指定控件
    /// </summary>
    /// <typeparam name="T"></typeparam>

    /// <param name="parentCpt"></param>
    /// <param name="cptName"></param>
    /// <returns></returns>
    public static T getCptFormSceneByName<T>(string cptName) where T : Component 
    {
        if (cptName == null || cptName.Length == 0)
            return null;
        T[] cptList = FindObjectsOfType<T>();
        if (cptList != null)
        {
            int cptListSize = cptList.Length;
            for (int i = 0; i < cptListSize; i++)
            {
                T cpt = cptList[i];
                if (cpt.name.Equals(cptName))
                    return cpt;
            }
        }
        return null;
    }
}

