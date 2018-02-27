using UnityEngine;
using System;

public class LogUtil
{
    public static void log(String logInfo)
    {
        Debug.Log("输出信息：" + logInfo);
    }
    public static void logError(String logInfo)
    {
        Debug.LogError("错误信息：" + logInfo);
    }
    public static void logWarning(String logInfo)
    {
        Debug.LogWarning("警告信息：" + logInfo);
    }
}