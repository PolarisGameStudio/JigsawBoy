using System.Collections;
using System.Collections.Generic;
using System;
using  UnityEngine;

public class DevUtil {

    /// <summary>
    /// 获取int类型随机数
    /// </summary>
    /// <param name="startNumber">开始</param>
    /// <param name="endNumber">结束</param>
    /// <returns>int类型随机数</returns>
	public static int getRandomInt(int startNumber,int endNumber)
    {
        var seed = Guid.NewGuid().GetHashCode();
        System.Random random = new System.Random(seed);
        int randomNumber= random.Next(startNumber, endNumber+1);
        return randomNumber;
    }

    /// <summary>
    /// Vector3 转化为 Vector2
    /// </summary>
    /// <param name="listVector3"></param>
    /// <returns></returns>
    public static List<Vector2> Vector3ToVector2(List<Vector3> listVector3) 
    {
        List<Vector2> listVector2 = new List<Vector2>();
        foreach(Vector3 item in listVector3)
        {
            listVector2.Add(new Vector2(item.x,item.y));
        }
        return listVector2;
    }

    /// <summary>
    /// list转数组
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <returns></returns>
    public static  T[] listToArray<T>(List<T> list)
    {
        if (list == null)
            return null;
        int listCount = list.Count;
        T[] tempArray = new T[listCount];
        for (int position=0;position< listCount;position++)
        {
            tempArray[position] = list[position];
        }
        return tempArray;
    }
}
