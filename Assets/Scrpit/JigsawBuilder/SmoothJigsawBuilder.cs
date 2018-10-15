using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class SmoothJigsawBuilder : BaseJigsawBuilder
{
    public override JigsawBean buildJigsaw(float jigsawWith, float jigsawHigh, JigsawBulgeEnum[] bulgeEnums, Texture2D jigsawPic)
    {
        throw new System.NotImplementedException();
    }

    public override List<JigsawBean> buildJigsawList(int horizontalJigsawNumber, int verticalJigsawNumber, Texture2D jigsawPic)
    {
      return base.baseBuildJigsawList(JigsawStyleEnum.Smooth, horizontalJigsawNumber, verticalJigsawNumber, jigsawPic);
    }

    public override void setListUVPosition(JigsawBean jigsawItem)
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// 设置顶点坐标
    /// </summary>
    /// <param name="jigsawItem"></param>
    public override void setListVerticesForItem(JigsawBean jigsawItem)
    {
        List<Vector3> listVertices = new List<Vector3>();
        base.baseSetListVerticesForItem(jigsawItem);

        float withX = jigsawItem.JigsawWith / 2f;
        float highY = jigsawItem.JigsawHigh / 2f;

        //添加起始点
        listVertices.Add(jigsawItem.CenterVector);
        //添加左下角点
        listVertices.Add(new Vector3(-withX, -highY));
        //添加左上角点
        listVertices.Add(new Vector3(-withX, highY));
        //添加右上角点
        listVertices.Add(new Vector3(withX, highY));
        //添加右下角点
        listVertices.Add(new Vector3(withX, -highY));

        setListVertices(jigsawItem, listVertices);
    }

}