using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class EquipDSHandle : BaseDataStorageHandle<EquipInfoBean>, IBaseDataStorage<EquipInfoBean, EquipInfoBean>
{
    private const string File_Name = "EquipDS";

    private static IBaseDataStorage<EquipInfoBean, EquipInfoBean> handle;

    public static IBaseDataStorage<EquipInfoBean, EquipInfoBean> getInstance()
    {
        if (handle == null)
        {
            handle = new EquipDSHandle();
        }
        return handle;
    }

    public List<EquipInfoBean> getAllData()
    {
        return startLoadDataForList(File_Name);
    }

    public EquipInfoBean getData(EquipInfoBean data)
    {
        List<EquipInfoBean> dataList = getAllData();
        if (dataList == null || dataList.Count == 0)
        {
            return data;
        }
        foreach (EquipInfoBean item in dataList)
        {
            if (item.equipType == data.equipType && item.equipTypeId == data.equipTypeId) {
                data.unlockType = item.unlockType;
                break;
            }
        }
        return data;
    }

    public void saveAllData(List<EquipInfoBean> data)
    {
        if (data == null || data.Count == 0)
        {
            LogUtil.log("保存失败-没有数据");
            return;
        }
        startSaveDataForList(File_Name, data);
    }

    /// <summary>
    /// 保存数据
    /// </summary>
    /// <param name="data"></param>
    public void saveData(EquipInfoBean data)
    {
        if (data == null || data.equipType == 0||data.equipTypeId==0)
        {
            LogUtil.log("保存失败-没有数据或没有装备ID");
            return;
        }
        List<EquipInfoBean> oldAllData = getAllData();
        //如果之前没有数据直接存储
        if (oldAllData == null || oldAllData.Count == 0)
        {
            oldAllData = new List<EquipInfoBean>();
            oldAllData.Add(data);
            startSaveDataForList(File_Name, oldAllData);
            return;
        }
        //如果有数据则遍历之前看是否有相同
        int hasDataBefore = -1;
        for (int i = 0; i < oldAllData.Count; i++)
        {
            if (oldAllData[i].equipType.Equals(data.equipType) && oldAllData[i].equipTypeId.Equals(data.equipTypeId))
            {
                hasDataBefore = i;
                break;
            }
        }
        if (hasDataBefore.Equals(-1))
        {
            oldAllData.Add(data);
            saveAllData(oldAllData);
        }
        else
        {
            oldAllData[hasDataBefore] = data;
            saveAllData(oldAllData);
        }
    }

}