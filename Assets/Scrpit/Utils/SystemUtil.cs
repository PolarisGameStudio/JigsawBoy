using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemUtil{

    /// <summary>
    /// 获取唯一ID
    /// </summary>
    /// <returns></returns>
  public static string getUUID()
    {
       return System.Guid.NewGuid().ToString("N");
    }
}
