using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

[Serializable]
public class JigsawResourcesBean 
{
    public string dataType;
    public string dataFilePath;
    public List<JigsawResInfoBean> dataList;
}