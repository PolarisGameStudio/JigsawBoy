using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class TrapezoidJigsawBuilder : BaseJigsawBuilder
{
    //梯形 上边长度
    private float m_Trapezoid_TopWith;
    //梯形 下边长度
    private float m_Trapezoid_BottomWith;
    //梯形  高
    private float m_Trapezoid_High;

    public TrapezoidJigsawBuilder() : base()
    {
        m_Trapezoid_TopWith = 0.8f;
        m_Trapezoid_BottomWith = 1.5f;
        m_Trapezoid_High = 0.6f;
    }

    public override JigsawBean buildJigsaw(float jigsawWith, float jigsawHigh, JigsawBulgeEnum[] bulgeEnums, Texture2D jigsawPic)
    {
        throw new System.NotImplementedException();
    }

    public override List<JigsawBean> buildJigsawList(int horizontalJigsawNumber, int verticalJigsawNumber, Texture2D jigsawPic)
    {
        return base.baseBuildJigsawList(JigsawStyleEnum.Trapezoid, horizontalJigsawNumber, verticalJigsawNumber, jigsawPic);
    }

    public override void setListUVPosition(JigsawBean jigsawItem)
    {
        throw new System.NotImplementedException();
    }

    public override void setListUVPositionForItem(JigsawBean jigsawItem)
    {
        base.baseSetListUVPositionForItem(jigsawItem);
    }

    public override void setListVerticesForItem(JigsawBean jigsawItem)
    {
        List<Vector3> listVertices = new List<Vector3>();
        base.baseSetListVerticesForItem(jigsawItem, listVertices);

        float withX = jigsawItem.JigsawWith / 2f;
        float highY = jigsawItem.JigsawHigh / 2f;

        //根据凹凸属性生成坐标点
        JigsawBulgeEnum[] listBulge = jigsawItem.ListBulge;
        JigsawBulgeEnum leftBulge = listBulge[0];
        JigsawBulgeEnum aboveBulge = listBulge[1];
        JigsawBulgeEnum rightBulge = listBulge[2];
        JigsawBulgeEnum belowBulge = listBulge[3];

        //添加左下角点
        listVertices.Add(new Vector3(-withX, -highY));
        //添加左边点
        getTrapezoidVertices(listVertices, leftBulge, Direction2DEnum.Left, withX, highY);

        //添加左上角点
        listVertices.Add(new Vector3(-withX, highY));
        //添加上边点
        getTrapezoidVertices(listVertices, aboveBulge, Direction2DEnum.Above, withX, highY);

        //添加右上角点
        listVertices.Add(new Vector3(withX, highY));
        //添加右边
        getTrapezoidVertices(listVertices, rightBulge, Direction2DEnum.Right, withX, highY);

        //添加右下角点
        listVertices.Add(new Vector3(withX, -highY));
        //添加下边
        getTrapezoidVertices(listVertices, belowBulge, Direction2DEnum.Below, withX, highY);

        setListVertices(jigsawItem, listVertices);
    }

    /// <summary>
    /// 获取梯形上坐标
    /// </summary>
    /// <param name="listVertices"></param>
    /// <param name="jigsawBulge"></param>
    /// <param name="direction"></param>
    /// <param name="withX"></param>
    /// <param name="highY"></param>
    private void getTrapezoidVertices(List<Vector3> listVertices, JigsawBulgeEnum jigsawBulge, Direction2DEnum direction, float withX, float highY)
    {

        List<Vector3> convex;
        List<Vector3> concave;
        List<Vector3> leftConcavePositionList = new List<Vector3>();
        leftConcavePositionList.Add(new Vector3(-withX, -m_Trapezoid_BottomWith / 2f));
        leftConcavePositionList.Add(new Vector3(-withX + -m_Trapezoid_High, -m_Trapezoid_TopWith / 2f));
        leftConcavePositionList.Add(new Vector3(-withX + -m_Trapezoid_High, m_Trapezoid_TopWith / 2f));
        leftConcavePositionList.Add(new Vector3(-withX, m_Trapezoid_BottomWith / 2f));

        GameUtil.getJigsawPuzzlescCCPositon(leftConcavePositionList, direction, withX, highY, out convex, out concave);

        if (jigsawBulge.Equals(JigsawBulgeEnum.Bulge))
        {
            listVertices.AddRange(convex);
        }
        else if (jigsawBulge.Equals(JigsawBulgeEnum.Sunken))
        {
            listVertices.AddRange(concave);
        }
    }
}