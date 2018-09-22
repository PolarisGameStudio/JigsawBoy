using UnityEngine;
using System.Collections.Generic;

public abstract class BaseDataStorageHandle<T>
{
    private string Data_Path = Application.persistentDataPath;

    public void startSaveData(string fileName, T dataBean)
    {
        if (fileName == null)
        {
            LogUtil.log("保存文件失败-没有文件名称");
            return;
        }
        if (dataBean == null)
        {
            LogUtil.log("保存文件失败-没有数据");
            return;
        }
        string strData = JsonUtil.ToJson(dataBean);
        FileUtil.CreateTextFile(Data_Path, fileName, strData);
    }

    public void startSaveDataForList(string fileName, List<T> dataBeanList)
    {
        if (fileName == null)
        {
            LogUtil.log("保存文件失败-没有文件名称");
            return;
        }
        if (dataBeanList == null)
        {
            LogUtil.log("保存文件失败-没有数据");
            return;
        }
        ListHandleBean<T> handBean = new ListHandleBean<T>();
        handBean.listData = dataBeanList;
        string strData = JsonUtil.ToJson(handBean);
        FileUtil.CreateTextFile(Data_Path, fileName, strData);
    }

    public T startLoadData(string fileName)
    {
        if (fileName == null)
        {
            LogUtil.log("读取文件失败-没有文件名称");
            return default(T);
        }
        string strData = FileUtil.LoadTextFile(Data_Path + "/" + fileName);
        if (strData == null)
            return default(T);
        T data = JsonUtil.FromJson<T>(strData);
        return data;
    }


    public List<T> startLoadDataForList(string fileName)
    {
        if (fileName == null)
        {
            LogUtil.log("读取文件失败-没有文件名称");
            return null;
        }
        string strData = FileUtil.LoadTextFile(Data_Path + "/" + fileName);
        if (strData == null)
            return null;
        ListHandleBean<T> handBean=  JsonUtil.FromJson<ListHandleBean<T>>(strData);
        if (handBean == null)
            return null;
        return handBean.listData;
    }
}