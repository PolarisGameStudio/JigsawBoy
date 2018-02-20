using UnityEngine;
using UnityEditor;
using System;

[Serializable]
public class JigsawResInfoBean 
{
    public long id; //id
    public string markFileName;//标记文件名
    public int horizontalNumber;//生成拼图的横向个数
    public int verticalJigsawNumber;//生成拼图的纵向个数
    public JigsawResInfoIntroduceBean details;//详细信息

    //资源路径
    public string resFilePath;
}