using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class DevUtil
{

    /// <summary>
    /// 获取int类型随机数
    /// </summary>
    /// <param name="startNumber">开始</param>
    /// <param name="endNumber">结束</param>
    /// <returns>int类型随机数</returns>
	public static int getRandomInt(int startNumber, int endNumber)
    {
        var seed = Guid.NewGuid().GetHashCode();
        System.Random random = new System.Random(seed);
        int randomNumber = random.Next(startNumber, endNumber + 1);
        return randomNumber;
    }

    /// <summary>
    /// 获取float类型随机数
    /// </summary>
    /// <param name="startNumber">开始</param>
    /// <param name="endNumber">结束</param>
    /// <returns>int类型随机数</returns>
    public static float getRandomFloat(float startNumber, float endNumber)
    {
        int randomInt = getRandomInt((int)(startNumber * 100), (int)(endNumber * 100));
        return randomInt / 100f;
    }

    /// <summary>
    /// Vector3 转化为 Vector2
    /// </summary>
    /// <param name="listVector3"></param>
    /// <returns></returns>
    public static List<Vector2> Vector3ToVector2(List<Vector3> listVector3)
    {
        List<Vector2> listVector2 = new List<Vector2>();
        foreach (Vector3 item in listVector3)
        {
            listVector2.Add(new Vector2(item.x, item.y));
        }
        return listVector2;
    }

    /// <summary>
    /// list转数组
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <returns></returns>
    public static T[] listToArray<T>(List<T> list)
    {
        if (list == null)
            return null;
        int listCount = list.Count;
        T[] tempArray = new T[listCount];
        for (int position = 0; position < listCount; position++)
        {
            tempArray[position] = list[position];
        }
        return tempArray;
    }


    /// <summary>
    /// list转数组
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <returns></returns>
    public static T[] listToArrayFormPosition<T>(List<T> list, int position)
    {
        if (list == null)
            return null;
        int listCount = list.Count;
        T[] tempArray = new T[listCount];
        int f = 0;
        for (int i = 0; i < listCount; i++)
        {
            int startPosition = i + position;
            if (startPosition < listCount)
            {
                tempArray[i] = list[startPosition];
            }
            else
            {
                tempArray[i] = list[f];
                f++;
            }

        }
        return tempArray;
    }

    /// <summary>
    /// 获取屏幕宽
    /// </summary>
    /// <returns></returns>
    public static float GetScreenWith()
    {
        float leftBorder;
        float rightBorder;
        float topBorder;
        float downBorder;
        Vector3 cornerPos = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, Mathf.Abs(Camera.main.transform.position.z)));

        leftBorder = Camera.main.transform.position.x - (cornerPos.x - Camera.main.transform.position.x);
        rightBorder = cornerPos.x;
        topBorder = cornerPos.y;
        downBorder = Camera.main.transform.position.y - (cornerPos.y - Camera.main.transform.position.y);

        return rightBorder - leftBorder;
    }
    /// <summary>
    /// 获取屏幕高
    /// </summary>
    /// <returns></returns>
    public static float GetScreenHeight()
    {
        float leftBorder;
        float rightBorder;
        float topBorder;
        float downBorder;
        Vector3 cornerPos = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, Mathf.Abs(Camera.main.transform.position.z)));

        leftBorder = Camera.main.transform.position.x - (cornerPos.x - Camera.main.transform.position.x);
        rightBorder = cornerPos.x;
        topBorder = cornerPos.y;
        downBorder = Camera.main.transform.position.y - (cornerPos.y - Camera.main.transform.position.y);

        return topBorder - downBorder;
    }
}
