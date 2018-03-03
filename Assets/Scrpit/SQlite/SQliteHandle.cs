using UnityEngine;
using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;

public class SQliteHandle : ScriptableObject
{
    private readonly static string DBPath = "data source=" + Application.streamingAssetsPath + "/DataBase/";


    public static SQLiteHelper GetSQLiteHelper(string dbName)
    {
        SQLiteHelper helper = new SQLiteHelper(DBPath + dbName);
        return helper;
    }

    /// <summary>
    /// 创建表
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="dataTypeList"></param>
    public static void CreateTable(string dbName, string tableName, Dictionary<string, string> dataTypeList)
    {
        SQLiteHelper sql = GetSQLiteHelper(dbName);
        if (tableName == null)
        {
            LogUtil.log("创建表失败，没有表名");
            return;
        }
        if (dataTypeList == null || dataTypeList.Count == 0)
        {
            LogUtil.log("创建表失败，没有数据");
            return;
        }
        string[] keyNameList = new string[dataTypeList.Count];
        string[] valueNameList = new string[dataTypeList.Count];
        int position = 0;
        foreach (var item in dataTypeList)
        {
            keyNameList[position] = item.Key;
            valueNameList[position] = item.Value;
            position++;
        }
        try
        {
            sql.CreateTable(tableName, keyNameList, valueNameList);
        }
        catch (Exception e)
        {
            LogUtil.log("创建表失败-" + e.Message);
        }
        finally
        {
            if (sql != null)
                sql.CloseConnection();
        }
    }

    /// <summary>
    /// 读取表数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dbName"></param>
    /// <param name="mainTable"></param>
    /// <param name="leftTableName"></param>
    /// <param name="mainKey"></param>
    /// <param name="leftKey"></param>
    /// <param name="mainColNames"></param>
    /// <param name="mainOperations"></param>
    /// <param name="mainColValues"></param>
    /// <returns></returns>
    public static List<T> LoadTableData<T>(string dbName, string mainTable, string[] leftTableName, string mainKey, string[] leftKey, string[] mainColNames, string[] mainOperations, string[] mainColValues)
    {
        SQLiteHelper sql = GetSQLiteHelper(dbName);
        SqliteDataReader reader = null;
        List<T> listData = new List<T>();
        try
        {
            T tempData = Activator.CreateInstance<T>();
            List<String> dataNameList = ReflexUtil.getAllName(tempData);
            reader = sql.ReadTable(mainTable, leftTableName, mainKey, leftKey, mainColNames, mainOperations, mainColValues);
            while (reader.Read())
            {
                T itemData = Activator.CreateInstance<T>();

                int dataNameSize = dataNameList.Count;
                for (int i = 0; i < dataNameSize; i++)
                {
                    string dataName = dataNameList[i];
                    int ordinal = reader.GetOrdinal(dataName);
                    if (ordinal == -1)
                        continue;

                    string name = reader.GetName(ordinal);
                    object value = reader.GetValue(ordinal);
                    if (value != null && !value.ToString().Equals("")) 
                    ReflexUtil.setValueByName(itemData, dataName, value);
                }
                listData.Add(itemData);
            }
            return listData;
        }
        catch (Exception e)
        {
            LogUtil.log("查询表失败-" + e.Message);
            return null;
        }
        finally
        {
            if (sql != null)
                sql.CloseConnection();
            if (reader != null)
                reader.Close();
        }
    }

    public static List<T> LoadTableData<T>(string dbName, string mainTable)
    {
        return LoadTableData<T>(dbName, mainTable, null, null, null, null, null, null);
    }

    public static List<T> LoadTableData<T>(string dbName, string mainTable, string[] leftTableName, string mainKey, string[] leftKey)
    {
        return LoadTableData<T>(dbName, mainTable, leftTableName, mainKey, leftKey, null, null, null);
    }
}