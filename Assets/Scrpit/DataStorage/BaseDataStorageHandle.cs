using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public interface BaseDataStorageHandle<T,V>
{
    /// <summary>
    /// 保存数据
    /// </summary>
    /// <param name="data"></param>
    void saveData(T data);

    /// <summary>
    /// 保存数据集
    /// </summary>
    /// <param name="listData"></param>
    void saveListData(List<T> listData);

    /// <summary>
    /// 获取数据
    /// </summary>
    /// <param name="markId"></param>
    T getData(V markData);

    /// <summary>
    /// 获取数据集
    /// </summary>
    /// <param name="listMarkId"></param>
    List<T> getListData(List<V> listMarkData);
}

