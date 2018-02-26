using UnityEngine;
using UnityEditor;
using System;

public class TimeUtil : ScriptableObject
{
    /// <summary>
    /// 获取当前时间
    /// </summary>
    /// <returns></returns>
     public static DateTime getNow()
    {
        return DateTime.Now;
    }

    /// <summary>
    /// 获取当前时间-年
    /// </summary>
    /// <returns></returns>
    public static int getNowYear()
    {
        return DateTime.Now.Year;
    }

    /// <summary>
    /// 获取当前时间-月
    /// </summary>
    /// <returns></returns>
    public static int getNowMonth()
    {
        return DateTime.Now.Month;
    }

    /// <summary>
    /// 获取当前时间-天
    /// </summary>
    /// <returns></returns>
    public static int getNowDay()
    {
        return DateTime.Now.Day;
    }

    /// <summary>
    /// 获取当前时间-小时
    /// </summary>
    /// <returns></returns>
    public static int getNowHour()
    {
        return DateTime.Now.Hour;
    }

    /// <summary>
    /// 获取当前时间-分
    /// </summary>
    /// <returns></returns>
    public static int getNowMinute()
    {
        return DateTime.Now.Minute;
    }

    /// <summary>
    /// 获取当前时间-秒
    /// </summary>
    /// <returns></returns>
    public static int getNowSecond()
    {
        return DateTime.Now.Second;
    }

    /// <summary>
    /// 获取当前时间-毫秒
    /// </summary>
    /// <returns></returns>
    public static int getNowMillisecond()
    {
        return DateTime.Now.Millisecond;
    } 

    /// <summary>
    /// 获取时间差
    /// </summary>
    /// <param name="startTime"></param>
    /// <param name="endTime"></param>
    /// <returns></returns>
    public static TimeSpan getTimeDifference(DateTime startTime,DateTime endTime)
    {
        TimeSpan differenceTime = endTime- startTime ;
        return differenceTime;
    }
}