using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class NomralJigsawBuilder : BaseJigsawBuilder
{
    //凸出部分圆润度
    private int m_JigsawTriangleNumber;
    //凸出部分直径
    private float m_JigsawBulgeR;

    /// <summary>
    /// 构造函数
    /// </summary>
    public NomralJigsawBuilder():base()
    {
        m_JigsawTriangleNumber = 360;
        m_JigsawBulgeR = m_JigsawUnitSize / 3f;
    }

    /// <summary>
    /// 创建普通样式的拼图碎片集合
    /// </summary>
    /// <param name="horizontalJigsawNumber">横向块数</param>
    /// <param name="verticalJigsawNumber">纵向块数</param>
    /// <param name="jigsawPic">需要分解的图片</param>
    /// <returns></returns>
    public override List<JigsawBean> buildJigsawList(int horizontalJigsawNumber, int verticalJigsawNumber, Texture2D jigsawPic)
    {
        return base.baseBuildJigsawList(JigsawStyleEnum.Def, horizontalJigsawNumber, verticalJigsawNumber, jigsawPic);
    }

    /// <summary>
    /// 随机生成一个拼图数据
    /// </summary>
    /// <returns></returns>
    /// 
    public override JigsawBean buildJigsaw(float jigsawWith, float jigsawHigh, JigsawBulgeEnum[] bulgeEdge, Texture2D jigsawPic)
    {
        if (jigsawHigh > jigsawWith)
            m_JigsawBulgeR = jigsawWith / 3f;
        else
            m_JigsawBulgeR = jigsawHigh / 3f;
        return base.baseBuildJigsaw(jigsawWith, jigsawHigh, bulgeEdge, jigsawPic);
    }


    /// <summary>
    /// 设置顶点
    /// </summary>
    /// <param name="jigsawItem"></param>
    public override void setListVerticesForItem(JigsawBean jigsawItem)
    {
        JigsawBulgeEnum[] listBulge = jigsawItem.ListBulge;

        if (listBulge == null)
            throw new Exception("没有凹凸参数");

        base.baseSetListVerticesForItem(jigsawItem);

        //根据凹凸属性生成坐标点
        List<Vector3> listVertices = new List<Vector3>();

        JigsawBulgeEnum leftBulge = listBulge[0];
        JigsawBulgeEnum aboveBulge = listBulge[1];
        JigsawBulgeEnum rightBulge = listBulge[2];
        JigsawBulgeEnum belowBulge = listBulge[3];

        float withX = jigsawItem.JigsawWith / 2f;
        float highY = jigsawItem.JigsawHigh / 2f;

        //添加起始点
        listVertices.Add(jigsawItem.CenterVector);
        //添加左下角点
        listVertices.Add(new Vector3(-withX, -highY));
        //添加左边凸出部分坐标点
        Vector3 lefgEdgeCenterVector = new Vector3(-withX, 0);
        List<Vector3> leftCircleVertices = getCircleVertices(lefgEdgeCenterVector, m_JigsawBulgeR, JigsawStyleNormalEdgeEnum.Left, leftBulge);
        List<Vector3> leftVertices = getCirclePartEdgeVerticesForNormal(leftCircleVertices, lefgEdgeCenterVector, JigsawStyleNormalEdgeEnum.Left, leftBulge);
        listVertices.AddRange(leftVertices);

        //添加左上角点
        listVertices.Add(new Vector3(-withX, highY));
        //添加上边凸出部分坐标点
        Vector3 aboveEdgeCenterVector = new Vector3(0, highY);
        List<Vector3> aboveCircleVertices = getCircleVertices(aboveEdgeCenterVector, m_JigsawBulgeR, JigsawStyleNormalEdgeEnum.Above, aboveBulge);
        List<Vector3> aboveVertices = getCirclePartEdgeVerticesForNormal(aboveCircleVertices, aboveEdgeCenterVector, JigsawStyleNormalEdgeEnum.Above, aboveBulge);
        listVertices.AddRange(aboveVertices);

        //添加右上角点
        listVertices.Add(new Vector3(withX, highY));
        //添加右边凸出部分坐标点
        Vector3 rightEdgeCenterVector = new Vector3(withX, 0);
        List<Vector3> rightCircleVertices = getCircleVertices(rightEdgeCenterVector, m_JigsawBulgeR, JigsawStyleNormalEdgeEnum.Right, rightBulge);
        List<Vector3> rightVertices = getCirclePartEdgeVerticesForNormal(rightCircleVertices, rightEdgeCenterVector, JigsawStyleNormalEdgeEnum.Right, rightBulge);
        listVertices.AddRange(rightVertices);

        //添加右下角点
        listVertices.Add(new Vector3(withX, -highY));
        //添加下边凸出部分坐标点
        Vector3 belowEdgeCenterVector = new Vector3(0, -highY);
        List<Vector3> belowCircleVertices = getCircleVertices(belowEdgeCenterVector, m_JigsawBulgeR, JigsawStyleNormalEdgeEnum.Below, belowBulge);
        List<Vector3> belowVertices = getCirclePartEdgeVerticesForNormal(belowCircleVertices, belowEdgeCenterVector, JigsawStyleNormalEdgeEnum.Below, belowBulge);
        listVertices.AddRange(belowVertices);

        setListVertices(jigsawItem, listVertices);
    }

    /// <summary>
    /// 设置UV坐标集
    /// </summary>
    /// <param name="jigsawItem"></param>
    public override void setListUVPosition(JigsawBean jigsawItem)
    {
        List<Vector3> listVertices = jigsawItem.ListVertices;
        if (listVertices == null)
        {
            LogUtil.log("没有顶点坐标");
            return;
        }

        List<Vector2> listUVposition = new List<Vector2>();

        float jigsawWithAndr = jigsawItem.JigsawWith + m_JigsawBulgeR;
        float jigsawHighAndr = jigsawItem.JigsawHigh + m_JigsawBulgeR;

        float picRatio;

        if (jigsawItem.JigsawUVWith > jigsawItem.JigsawUVHigh)
            picRatio = jigsawItem.JigsawUVHigh / jigsawHighAndr;
        else
            picRatio = jigsawItem.JigsawUVWith / jigsawWithAndr;


        float xRatio = 1 / jigsawItem.JigsawUVWith;
        float yRatio = 1 / jigsawItem.JigsawUVHigh;


        foreach (Vector3 item in listVertices)
        {
            float uvXposition = (((item.x + jigsawWithAndr / 2f) * picRatio)) * xRatio;
            float uvYposition = (((item.y + jigsawHighAndr / 2f) * picRatio)) * yRatio;
            listUVposition.Add(new Vector2(uvXposition, uvYposition));
        }
        setListUVPosition(jigsawItem, listUVposition);
    }

    /// <summary>
    /// 获取凸出部分的圆所有坐标
    /// </summary>
    /// <param name="CenterVector"></param>
    /// <param name="edgeEnum"></param>
    ///  <param name="bulgeEnum"></param>
    /// <returns></returns>
    private List<Vector3> getCircleVertices(Vector3 centerVector, float bulgeR, JigsawStyleNormalEdgeEnum edgeEnum, JigsawBulgeEnum bulgeEnum)
    {
        if (edgeEnum == JigsawStyleNormalEdgeEnum.Left)
        {
            if (bulgeEnum == JigsawBulgeEnum.Bulge)
            {
                return GeometryUtil.getCircleVertices(centerVector, bulgeR, m_JigsawTriangleNumber, true, CircleStartVectorEnum.Below);
            }
            else if (bulgeEnum == JigsawBulgeEnum.Sunken)
            {
                return GeometryUtil.getCircleVertices(centerVector, bulgeR, m_JigsawTriangleNumber, false, CircleStartVectorEnum.Below);
            }
        }
        else if (edgeEnum == JigsawStyleNormalEdgeEnum.Above)
        {
            if (bulgeEnum == JigsawBulgeEnum.Bulge)
            {
                return GeometryUtil.getCircleVertices(centerVector, bulgeR, m_JigsawTriangleNumber, true, CircleStartVectorEnum.Left);
            }
            else if (bulgeEnum == JigsawBulgeEnum.Sunken)
            {
                return GeometryUtil.getCircleVertices(centerVector, bulgeR, m_JigsawTriangleNumber, false, CircleStartVectorEnum.Left);
            }
        }
        else if (edgeEnum == JigsawStyleNormalEdgeEnum.Right)
        {
            if (bulgeEnum == JigsawBulgeEnum.Bulge)
            {
                return GeometryUtil.getCircleVertices(centerVector, bulgeR, m_JigsawTriangleNumber, true, CircleStartVectorEnum.Above);
            }
            else if (bulgeEnum == JigsawBulgeEnum.Sunken)
            {
                return GeometryUtil.getCircleVertices(centerVector, bulgeR, m_JigsawTriangleNumber, false, CircleStartVectorEnum.Above);
            }
        }
        else if (edgeEnum == JigsawStyleNormalEdgeEnum.Below)
        {
            if (bulgeEnum == JigsawBulgeEnum.Bulge)
            {
                return GeometryUtil.getCircleVertices(centerVector, bulgeR, m_JigsawTriangleNumber, true, CircleStartVectorEnum.Right);
            }
            else if (bulgeEnum == JigsawBulgeEnum.Sunken)
            {
                return GeometryUtil.getCircleVertices(centerVector, bulgeR, m_JigsawTriangleNumber, false, CircleStartVectorEnum.Right);
            }
        }

        return new List<Vector3>();
    }

    /// <summary>
    /// 获取普通拼图凸出部分点坐标
    /// </summary>
    /// <param name="circleVertices"></param>
    /// <param name="edgeEnum"></param>
    /// <returns></returns>
    private List<Vector3> getCirclePartEdgeVerticesForNormal(List<Vector3> circleVertices, Vector3 centerVecotr, JigsawStyleNormalEdgeEnum edgeEnum, JigsawBulgeEnum bulgeEnum)
    {
        List<Vector3> listVertices = new List<Vector3>();
        foreach (Vector3 itemVector in circleVertices)
        {
            if (edgeEnum == JigsawStyleNormalEdgeEnum.Left)
            {
                if (bulgeEnum == JigsawBulgeEnum.Bulge)
                {
                    if (itemVector.x <= centerVecotr.x)
                        listVertices.Add(itemVector);
                }
                else if (bulgeEnum == JigsawBulgeEnum.Sunken)
                {
                    if (itemVector.x >= centerVecotr.x)
                        listVertices.Add(itemVector);
                }
            }
            else if (edgeEnum == JigsawStyleNormalEdgeEnum.Above)
            {
                if (bulgeEnum == JigsawBulgeEnum.Bulge)
                {
                    if (itemVector.y >= centerVecotr.y)
                        listVertices.Add(itemVector);
                }
                else if (bulgeEnum == JigsawBulgeEnum.Sunken)
                {
                    if (itemVector.y <= centerVecotr.y)
                        listVertices.Add(itemVector);
                }
            }
            else if (edgeEnum == JigsawStyleNormalEdgeEnum.Right)
            {
                if (bulgeEnum == JigsawBulgeEnum.Bulge)
                {
                    if (itemVector.x >= centerVecotr.x)
                        listVertices.Add(itemVector);
                }
                else if (bulgeEnum == JigsawBulgeEnum.Sunken)
                {
                    if (itemVector.x <= centerVecotr.x)
                        listVertices.Add(itemVector);
                }
            }
            else if (edgeEnum == JigsawStyleNormalEdgeEnum.Below)
            {
                if (bulgeEnum == JigsawBulgeEnum.Bulge)
                {
                    if (itemVector.y <= centerVecotr.y)
                        listVertices.Add(itemVector);
                }
                else if (bulgeEnum == JigsawBulgeEnum.Sunken)
                {
                    if (itemVector.y >= centerVecotr.y)
                        listVertices.Add(itemVector);
                }
            }
        }
        return listVertices;
    }


}



