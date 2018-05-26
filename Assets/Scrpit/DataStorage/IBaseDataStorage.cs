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
    void saveAllData(List<T> data);

    /// <summary>
    /// 保存单个数据
    /// </summary>
    /// <param name="data"></param>
    void saveData(T data);

    /// <summary>
    /// 获取所有数据
    /// </summary>
    /// <param name="markId"></param>
    List<T> getAllData();

    /// <summary>
    ///  根据V获取数据
    /// </summary>
    /// <returns></returns>
    T getData(V data);
    
}

