using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class NomralJigsawBuilder : IBaseJigsawBuilder
{
    //拼图缩放大小
    private float mJigsawScale;
    //凸出部分圆润度
    private int mJigsawTriangleNumber;
    //凸出部分圆润度
    private float mJigsawUnitSize;
    //凸出部分直径
    private float mJigsawBulgeR;

    /// <summary>
    /// 构造函数
    /// </summary>
    public NomralJigsawBuilder()
    {
        mJigsawScale = 1f;
        mJigsawUnitSize = 3f * mJigsawScale;
        mJigsawTriangleNumber = 90;
        mJigsawBulgeR = mJigsawUnitSize / 3f;
    }

    /// <summary>
    /// 创建普通样式的拼图碎片集合
    /// </summary>
    /// <param name="horizontalJigsawNumber">横向块数</param>
    /// <param name="verticalJigsawNumber">纵向块数</param>
    /// <param name="jigsawPic">需要分解的图片</param>
    /// <returns></returns>
    public List<JigsawBean> buildJigsawList(int horizontalJigsawNumber, int verticalJigsawNumber, Texture2D jigsawPic)
    {
        //初始化参数
        JigsawBean tempBean = getWithAndHighByTexture2D(horizontalJigsawNumber, verticalJigsawNumber, jigsawPic);
        int jigsawTotalNumber = verticalJigsawNumber * horizontalJigsawNumber;

        List<JigsawBean> listJigsawBean = new List<JigsawBean>();

        for (int horizontal = 0; horizontal < horizontalJigsawNumber; horizontal++)
        {

            for (int vertical = 0; vertical < verticalJigsawNumber; vertical++)
            {
                //生成拼图对象
                JigsawBean jigsawItem = new JigsawBean();
                //确认拼图样式
                setJigsawStyle(jigsawItem);
                //确认当前拼图碎片相对图片的坐标
                setMarkLocation(jigsawItem, horizontal, vertical);
                //设置拼图长宽
                setJigsawHigh(jigsawItem, tempBean.JigsawHigh);
                setJigsawWith(jigsawItem, tempBean.JigsawWith);
                setJigsawUVHigh(jigsawItem, tempBean.JigsawUVHigh);
                setJigsawUVWith(jigsawItem, tempBean.JigsawUVWith);
                setJigsawNumber(jigsawItem, jigsawTotalNumber);
                listJigsawBean.Add(jigsawItem);
            }

        }

        int listJigsawBeanCount = listJigsawBean.Count;
        for (int jigsawposition = 0; jigsawposition < listJigsawBeanCount; jigsawposition++)
        {
            JigsawBean jigsawItem = listJigsawBean[jigsawposition];
            //确认拼图每个边的凹凸情况
            setBulgeEdgeForItem(listJigsawBean, jigsawItem);
            //确认拼图顶点坐标和UVposition
            setListVerticesForItem(jigsawItem);
            setListUVPositionForItem(jigsawItem);

        }

        return listJigsawBean;
    }

    /// <summary>
    /// 随机生成一个拼图数据
    /// </summary>
    /// <returns></returns>
    /// 
    public JigsawBean buildJigsaw(float jigsawWith, float jigsawHigh, JigsawBulgeEnum[] bulgeEdge, Texture2D jigsawPic)
    {
        //生成拼图对象
        JigsawBean jigsawItem = new JigsawBean();
        //确认拼图样式
        setJigsawStyle(jigsawItem);
        //确认当前拼图碎片相对图片的坐标
        setMarkLocation(jigsawItem, 0, 0);
        //设置拼图长宽
        setJigsawHigh(jigsawItem, jigsawWith);
        setJigsawWith(jigsawItem, jigsawHigh);
        setJigsawUVWith(jigsawItem, jigsawPic.width);
        setJigsawUVHigh(jigsawItem, jigsawPic.height);
        //设置拼图块数
        setJigsawNumber(jigsawItem, 1);
        //确认拼图每个边的凹凸情况
        setBulgeEdge(jigsawItem, bulgeEdge);
        //确认拼图顶点坐标和UVposition
        setListVerticesForItem(jigsawItem);
        setListUVPosition(jigsawItem);
        return jigsawItem;
    }
    //-----------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// 初始化参数
    /// </summary>
    /// <param name="horizontalJigsawNumber"></param>
    /// <param name="verticalJigsawNumber"></param>
    /// <param name="jigsawPic"></param>
    private JigsawBean getWithAndHighByTexture2D(int horizontalJigsawNumber, int verticalJigsawNumber, Texture2D jigsawPic)
    {
        JigsawBean tempBean = new JigsawBean();

        float picItemWith = jigsawPic.width / (float)horizontalJigsawNumber;
        float picItemHigh = jigsawPic.height / (float)verticalJigsawNumber;

        tempBean.JigsawUVWith = picItemWith / jigsawPic.width;
        tempBean.JigsawUVHigh = picItemHigh / jigsawPic.height;

        if (picItemWith > picItemHigh)
        {
            //拼图的高
            tempBean.JigsawHigh = mJigsawUnitSize;
            //拼图的宽
            tempBean.JigsawWith = tempBean.JigsawHigh * (picItemWith / picItemHigh);
        }
        else
        {
            //拼图的宽
            tempBean.JigsawWith = mJigsawUnitSize;
            //拼图的高
            tempBean.JigsawHigh = tempBean.JigsawWith * (picItemHigh / picItemWith);
        }
        return tempBean;
    }

    /// <summary>
    /// 设置拼图块所有边的凹凸情况
    /// </summary>
    /// <param name="listJigsaw">所有的拼图块</param>
    /// <param name="jigsawItem">当前拼图块</param>
    private void setBulgeEdgeForItem(List<JigsawBean> listJigsaw, JigsawBean jigsawItem)
    {
        JigsawBulgeEnum leftBulge = JigsawBulgeEnum.Smooth;
        JigsawBulgeEnum rightBulge = JigsawBulgeEnum.Smooth;
        JigsawBulgeEnum belowBulge = JigsawBulgeEnum.Smooth;
        JigsawBulgeEnum aboveBulge = JigsawBulgeEnum.Smooth;

        int listJigsawCount = listJigsaw.Count;
        Vector2 markLocation = jigsawItem.MarkLocation;
        if (markLocation == null)
            return;

        for (int childposition = 0; childposition < listJigsawCount; childposition++)
        {
            JigsawBean childJigsaw = listJigsaw[childposition];
            Vector2 childMarkLocation = childJigsaw.MarkLocation;
            if (markLocation == null)
                continue;
            if (childMarkLocation.y.Equals(markLocation.y))
                if ((childMarkLocation.x - markLocation.x) == 1)
                    rightBulge = compareBulge(childJigsaw, JigsawStyleNormalEdgeEnum.Right);
                else if ((childMarkLocation.x - markLocation.x) == -1)
                    leftBulge = compareBulge(childJigsaw, JigsawStyleNormalEdgeEnum.Left);
            if (childMarkLocation.x.Equals(markLocation.x))
                if ((childMarkLocation.y - markLocation.y) == 1)
                    aboveBulge = compareBulge(childJigsaw, JigsawStyleNormalEdgeEnum.Above);
                else if ((childMarkLocation.y - markLocation.y) == -1)
                    belowBulge = compareBulge(childJigsaw, JigsawStyleNormalEdgeEnum.Below);
        }

        setBulgeEdge(jigsawItem, new JigsawBulgeEnum[4] { leftBulge, aboveBulge, rightBulge, belowBulge });
    }


    /// <summary>
    /// 设置顶点
    /// </summary>
    /// <param name="jigsawItem"></param>
    private void setListVerticesForItem(JigsawBean jigsawItem)
    {
        JigsawBulgeEnum[] listBulge = jigsawItem.ListBulge;

        if (listBulge == null)
            throw new Exception("没有凹凸参数");

        List<Vector3> listVertices = new List<Vector3>();
        //添加中心点坐标
        Vector3 centerVector = new Vector3(0, 0);
        listVertices.Add(centerVector);
        setCenterVector(jigsawItem, centerVector);

        //根据凹凸属性生成坐标点
        JigsawBulgeEnum leftBulge = listBulge[0];
        JigsawBulgeEnum aboveBulge = listBulge[1];
        JigsawBulgeEnum rightBulge = listBulge[2];
        JigsawBulgeEnum belowBulge = listBulge[3];

        float withX = jigsawItem.JigsawWith / 2f;
        float highY = jigsawItem.JigsawHigh / 2f;

        //添加左下角点
        listVertices.Add(new Vector3(-withX, -highY));
        //添加左边凸出部分坐标点
        Vector3 lefgEdgeCenterVector = new Vector3(-withX, 0);
        List<Vector3> leftCircleVertices = getCircleVertices(lefgEdgeCenterVector, mJigsawBulgeR, JigsawStyleNormalEdgeEnum.Left, leftBulge);
        List<Vector3> leftVertices = getCirclePartEdgeVerticesForNormal(leftCircleVertices, lefgEdgeCenterVector, JigsawStyleNormalEdgeEnum.Left, leftBulge);
        listVertices.AddRange(leftVertices);

        //添加左上角点
        listVertices.Add(new Vector3(-withX, highY));
        //添加上边凸出部分坐标点
        Vector3 aboveEdgeCenterVector = new Vector3(0, highY);
        List<Vector3> aboveCircleVertices = getCircleVertices(aboveEdgeCenterVector, mJigsawBulgeR, JigsawStyleNormalEdgeEnum.Above, aboveBulge);
        List<Vector3> aboveVertices = getCirclePartEdgeVerticesForNormal(aboveCircleVertices, aboveEdgeCenterVector, JigsawStyleNormalEdgeEnum.Above, aboveBulge);
        listVertices.AddRange(aboveVertices);

        //添加右上角点
        listVertices.Add(new Vector3(withX, highY));
        //添加右边凸出部分坐标点
        Vector3 rightEdgeCenterVector = new Vector3(withX, 0);
        List<Vector3> rightCircleVertices = getCircleVertices(rightEdgeCenterVector, mJigsawBulgeR, JigsawStyleNormalEdgeEnum.Right, rightBulge);
        List<Vector3> rightVertices = getCirclePartEdgeVerticesForNormal(rightCircleVertices, rightEdgeCenterVector, JigsawStyleNormalEdgeEnum.Right, rightBulge);
        listVertices.AddRange(rightVertices);

        //添加右下角点
        listVertices.Add(new Vector3(withX, -highY));
        //添加下边凸出部分坐标点
        Vector3 belowEdgeCenterVector = new Vector3(0, -highY);
        List<Vector3> belowCircleVertices = getCircleVertices(belowEdgeCenterVector, mJigsawBulgeR, JigsawStyleNormalEdgeEnum.Below, belowBulge);
        List<Vector3> belowVertices = getCirclePartEdgeVerticesForNormal(belowCircleVertices, belowEdgeCenterVector, JigsawStyleNormalEdgeEnum.Below, belowBulge);
        listVertices.AddRange(belowVertices);

        setListVertices(jigsawItem, listVertices);
    }

    /// <summary>
    /// 设置UV点坐标集
    /// </summary>
    /// <param name="jigsawItem"></param>
    private void setListUVPositionForItem(JigsawBean jigsawItem)
    {

        List<Vector3> listVertices = jigsawItem.ListVertices;
        Vector2 markLocation = jigsawItem.MarkLocation;
        if (listVertices == null)
        {
            LogUtil.log("没有顶点坐标");
            return;
        }
        if (markLocation == null)
        {
            LogUtil.log("没有标记坐标");
            return;
        }

        List<Vector2> listUVposition = new List<Vector2>();

        float xRatio = jigsawItem.JigsawWith / jigsawItem.JigsawUVWith;
        float yRatio = jigsawItem.JigsawHigh / jigsawItem.JigsawUVHigh;

        foreach (Vector3 item in listVertices)
        {
            float uvXposition = ((item.x + jigsawItem.JigsawWith / 2f) / xRatio) + (markLocation.x * jigsawItem.JigsawUVWith);
            float uvYposition = ((item.y + jigsawItem.JigsawHigh / 2f) / yRatio) + (markLocation.y * jigsawItem.JigsawUVHigh);
            listUVposition.Add(new Vector2(uvXposition, uvYposition));
        }
        setListUVPosition(jigsawItem, listUVposition);

    }
    /// <summary>
    /// 设置UV坐标集
    /// </summary>
    /// <param name="jigsawItem"></param>
    private void setListUVPosition(JigsawBean jigsawItem)
    {
        List<Vector3> listVertices = jigsawItem.ListVertices;
        if (listVertices == null)
        {
            LogUtil.log("没有顶点坐标");
            return;
        }

        List<Vector2> listUVposition = new List<Vector2>();

        float jigsawWithAndr = jigsawItem.JigsawWith + mJigsawBulgeR ;
        float jigsawHighAndr = jigsawItem.JigsawHigh + mJigsawBulgeR ;

        float picRatio;

        if (jigsawItem.JigsawUVWith > jigsawItem.JigsawUVHigh)
            picRatio = jigsawItem.JigsawUVHigh / jigsawHighAndr;
        else
            picRatio = jigsawItem.JigsawUVWith / jigsawWithAndr;
    

        float xRatio = 1 / jigsawItem.JigsawUVWith;
        float yRatio = 1 / jigsawItem.JigsawUVHigh;


        foreach (Vector3 item in listVertices)
        {
            float uvXposition = (((item.x + jigsawWithAndr / 2f) * picRatio)  ) * xRatio;
            float uvYposition = (((item.y + jigsawHighAndr / 2f)* picRatio) ) * yRatio;
            listUVposition.Add(new Vector2(uvXposition, uvYposition));
        }
        setListUVPosition(jigsawItem, listUVposition);
    }

    /// <summary>
    /// 比较相邻拼图碎片的凹凸情况 获取自身的凹凸情况
    /// </summary>
    /// <param name="jigsawBean"></param>
    /// <returns></returns>
    private JigsawBulgeEnum compareBulge(JigsawBean compareJigsaw, JigsawStyleNormalEdgeEnum edgeEnum)
    {
        if (compareJigsaw == null)
            return JigsawBulgeEnum.Smooth;
        //获取相邻拼图的凹凸情况
        JigsawBulgeEnum[] compareBulgeList = compareJigsaw.ListBulge;
        if (compareBulgeList == null || compareBulgeList.Length != 4)
        {
            int randonBulge = DevUtil.getRandomInt(1, 2);
            if (randonBulge == 1)
                return JigsawBulgeEnum.Bulge;
            else
                return JigsawBulgeEnum.Sunken;
        }

        int bulgeposition;
        if (edgeEnum == JigsawStyleNormalEdgeEnum.Left)
            bulgeposition = 2;
        else if (edgeEnum == JigsawStyleNormalEdgeEnum.Above)
            bulgeposition = 3;
        else if (edgeEnum == JigsawStyleNormalEdgeEnum.Right)
            bulgeposition = 0;
        else if (edgeEnum == JigsawStyleNormalEdgeEnum.Below)
            bulgeposition = 1;
        else
            return JigsawBulgeEnum.Smooth;


        JigsawBulgeEnum compareBulgeEnum = compareBulgeList[bulgeposition];
        if (compareBulgeEnum == JigsawBulgeEnum.Smooth)
            return JigsawBulgeEnum.Smooth;
        else if (compareBulgeEnum == JigsawBulgeEnum.Bulge)
            return JigsawBulgeEnum.Sunken;
        else if (compareBulgeEnum == JigsawBulgeEnum.Sunken)
            return JigsawBulgeEnum.Bulge;
        else
            return JigsawBulgeEnum.Smooth;
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
                return GeometryUtil.getCircleVertices(centerVector, bulgeR, mJigsawTriangleNumber, true, CircleStartVectorEnum.Below);
            }
            else if (bulgeEnum == JigsawBulgeEnum.Sunken)
            {
                return GeometryUtil.getCircleVertices(centerVector, bulgeR, mJigsawTriangleNumber, false, CircleStartVectorEnum.Below);
            }
        }
        else if (edgeEnum == JigsawStyleNormalEdgeEnum.Above)
        {
            if (bulgeEnum == JigsawBulgeEnum.Bulge)
            {
                return GeometryUtil.getCircleVertices(centerVector, bulgeR, mJigsawTriangleNumber, true, CircleStartVectorEnum.Left);
            }
            else if (bulgeEnum == JigsawBulgeEnum.Sunken)
            {
                return GeometryUtil.getCircleVertices(centerVector, bulgeR, mJigsawTriangleNumber, false, CircleStartVectorEnum.Left);
            }
        }
        else if (edgeEnum == JigsawStyleNormalEdgeEnum.Right)
        {
            if (bulgeEnum == JigsawBulgeEnum.Bulge)
            {
                return GeometryUtil.getCircleVertices(centerVector, bulgeR, mJigsawTriangleNumber, true, CircleStartVectorEnum.Above);
            }
            else if (bulgeEnum == JigsawBulgeEnum.Sunken)
            {
                return GeometryUtil.getCircleVertices(centerVector, bulgeR, mJigsawTriangleNumber, false, CircleStartVectorEnum.Above);
            }
        }
        else if (edgeEnum == JigsawStyleNormalEdgeEnum.Below)
        {
            if (bulgeEnum == JigsawBulgeEnum.Bulge)
            {
                return GeometryUtil.getCircleVertices(centerVector, bulgeR, mJigsawTriangleNumber, true, CircleStartVectorEnum.Right);
            }
            else if (bulgeEnum == JigsawBulgeEnum.Sunken)
            {
                return GeometryUtil.getCircleVertices(centerVector, bulgeR, mJigsawTriangleNumber, false, CircleStartVectorEnum.Right);
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

    //-----------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// 设置拼图碎片对象
    /// </summary>
    /// <param name="jigsaw"></param>
    public void setJigsawGameObj(JigsawBean jigsaw, GameObject gameObj)
    {
        if (jigsaw == null) { return; }
        jigsaw.JigsawGameObj = gameObj;
    }

    /// <summary>
    /// 设置当前样式
    /// </summary>
    public void setJigsawStyle(JigsawBean jigsaw)
    {
        if (jigsaw == null) { return; }
        jigsaw.JigsawStyle = JigsawStyleEnum.Normal;
    }

    /// <summary>
    /// 设置当前拼图碎片相对图片的坐标
    /// </summary>
    /// <param name="jigsaw"></param>
    public void setMarkLocation(JigsawBean jigsaw, int markX, int markY)
    {
        if (jigsaw == null) { return; }
        Vector2 markVector = new Vector2(markX, markY);
        jigsaw.MarkLocation = markVector;
    }

    /// <summary>
    /// 设置每个边的凹凸情况
    /// </summary>
    /// <param name="jigsawBulges">凹凸集合</param>
    public void setBulgeEdge(JigsawBean jigsaw, JigsawBulgeEnum[] jigsawBulges)
    {
        if (jigsaw == null) { return; }
        jigsaw.ListBulge = jigsawBulges;
    }

    /// <summary>
    /// 设置顶点坐标
    /// </summary>
    /// <param name="jigsaw"></param>
    public void setListVertices(JigsawBean jigsaw, List<Vector3> listVertices)
    {
        if (jigsaw == null) { return; }
        jigsaw.ListVertices = listVertices;
    }

    /// <summary>
    /// 设置UV坐标
    /// </summary>
    /// <param name="jigsaw"></param>
    public void setListUVPosition(JigsawBean jigsaw, List<Vector2> listUVposition)
    {
        if (jigsaw == null) { return; }
        jigsaw.ListUVposition = listUVposition;
    }


    /// <summary>
    /// 设置中心点坐标
    /// </summary>
    /// <param name="jigsaw"></param>
    /// <param name="centerVector"></param>
    public void setCenterVector(JigsawBean jigsaw, Vector3 centerVector)
    {
        if (jigsaw == null) { return; }
        jigsaw.CenterVector = centerVector;
    }

    /// <summary>
    /// 设置拼图宽
    /// </summary>
    /// <param name="jigsaw"></param>
    /// <param name="jigsawWith"></param>
    public void setJigsawWith(JigsawBean jigsaw, float jigsawWith)
    {
        if (jigsaw == null) { return; }
        jigsaw.JigsawWith = jigsawWith;
    }

    /// <summary>
    /// 设置拼图高
    /// </summary>
    /// <param name="jigsaw"></param>
    /// <param name="jigsawHigh"></param>
    public void setJigsawHigh(JigsawBean jigsaw, float jigsawHigh)
    {
        if (jigsaw == null) { return; }
        jigsaw.JigsawHigh = jigsawHigh;
    }

    /// <summary>
    /// 设置拼图UV宽
    /// </summary>
    /// <param name="jigsaw"></param>
    /// <param name="jigsawWith"></param>
    public void setJigsawUVWith(JigsawBean jigsaw, float jigsawUVWith)
    {
        if (jigsaw == null) { return; }
        jigsaw.JigsawUVWith = jigsawUVWith;
    }

    /// <summary>
    /// 设置拼图UV高
    /// </summary>
    /// <param name="jigsaw"></param>
    /// <param name="jigsawHigh"></param>
    public void setJigsawUVHigh(JigsawBean jigsaw, float jigsawUVHigh)
    {
        if (jigsaw == null) { return; }
        jigsaw.JigsawUVHigh = jigsawUVHigh;
    }

    /// <summary>
    /// 设置拼图块数
    /// </summary>
    /// <param name="jigsaw"></param>
    /// <param name="jigsawNumber"></param>
    public void setJigsawNumber(JigsawBean jigsaw, int jigsawNumber)
    {
        if (jigsaw == null) { return; }
        jigsaw.JigsawNumber = jigsawNumber;
    }
}



