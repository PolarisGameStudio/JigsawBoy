using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class MakerDataBean 
{
    public string makerTitle;
    public string makeName;

    public MakerDataBean(string makerTitle, string makeName,List<MakerDataBean> listData)
    {
        this.makerTitle = makerTitle;
        this.makeName = makeName;
        listData.Add(this);
    }
}