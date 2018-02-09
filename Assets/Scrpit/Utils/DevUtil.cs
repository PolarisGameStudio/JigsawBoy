using System.Collections;
using System.Collections.Generic;
using System;

public class DevUtil {

    /// <summary>
    /// 获取int类型随机数
    /// </summary>
    /// <param name="startNumber">开始</param>
    /// <param name="endNumber">结束</param>
    /// <returns>int类型随机数</returns>
	public static int getRandomInt(int startNumber,int endNumber)
    {
        Random random = new Random();
        int randomNumber= random.Next(startNumber, endNumber+1);
        return randomNumber;
    }
}
