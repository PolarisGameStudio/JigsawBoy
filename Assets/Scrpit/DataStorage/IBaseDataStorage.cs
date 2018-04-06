using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public interface IBaseDataStorage<T, V>
{
    /// <summary>
    /// 保存所有数据
    /// </summary>
    /// <param name="data"></param>
    void saveAllData(T data);


    /// <summary>
    /// 获取所有数据
    /// </summary>
    /// <param name="markId"></param>
    T getAllData();

}

