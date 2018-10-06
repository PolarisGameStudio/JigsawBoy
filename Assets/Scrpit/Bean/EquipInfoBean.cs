using UnityEngine;
using UnityEditor;
using System;


[Serializable]
public class EquipInfoBean 
{   
    public int equipType;
    public long equipTypeId;
    public string equipName;
    public int unlockType=0;//0锁住，1解锁

    public int unlockPoint;//解锁点数
    public string equipImageUrl;//图片地址
}