using UnityEngine;
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
    public static TimeSpan getTimeDifference(DateTime startTime, DateTime endTime)
    {
        TimeSpan differenceTime = endTime - startTime;
        return differenceTime;
    }


    /// <summary>
    /// 比较哪个时间更快更短
    /// </summary>
    /// <param name="sourceTime"></param>
    /// <param name="targetTime"></param>
    /// <returns></returns>
    public static bool isFasterTime(TimeBean sourceTime, TimeBean targetTime)
    {
        if (sourceTime == null || targetTime == null)
            return false;

        int sourceTimeCode = sourceTime.days * 1000 + sourceTime.hours * 100 + sourceTime.minutes * 10 + sourceTime.seconds;
        int targetTimeCode = targetTime.days * 1000 + targetTime.hours * 100 + targetTime.minutes * 10 + targetTime.seconds;

        if (sourceTimeCode > targetTimeCode)
            return false;
        else
            return true;
    }
}