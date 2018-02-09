using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class NomralJigsawBuilder : IBaseJigsawBuilder
{
    //拼图缩放大小
    private float mJigsawScale;
    //拼图的宽
    private float mJigsawWith;
    //拼图的高
    private float mJigsawHigh;


    /// <summary>
    /// 构造函数
    /// </summary>
    public NomralJigsawBuilder()
    {
        mJigsawScale = 1f;
        mJigsawWith = 3f;
        mJigsawHigh = 3f;
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
        List<JigsawBean> listJigsawBean = new List<JigsawBean>();

        for (int horizontal = 0; horizontal < horizontalJigsawNumber; horizontal++)
        {

            for (int vertical = 0; vertical < verticalJigsawNumber; vertical++)
            {
                //生成拼图对象
                JigsawBean jigsawItem = new JigsawBean();
                //确认拼图原图片
                setSourcePic(jigsawItem, jigsawPic);
                //确认拼图样式
                setJigsawStyle(jigsawItem);
                //确认当前拼图碎片相对图片的坐标
                setMarkLocation(jigsawItem, horizontal, vertical);

                listJigsawBean.Add(jigsawItem);
            }

        }


        int listJigsawBeanCount = listJigsawBean.Count;
        for (int jigsawPostion = 0; jigsawPostion < listJigsawBeanCount; jigsawPostion++)
        {
            JigsawBean jigsawItem = listJigsawBean[jigsawPostion];
            //确认拼图每个边的凹凸情况
            setBulgeEdgeForItem(listJigsawBean, jigsawItem, horizontalJigsawNumber, verticalJigsawNumber);
            //确认拼图顶点坐标和UVpostion
            setListVerticesAndListUVPositionForItem( jigsawItem,  horizontalJigsawNumber,  verticalJigsawNumber);
            // 确认拼图的每个游戏对象
            GameObject itemObj = CreateJigsawGameObj.getJigsawGameObj(jigsawItem);
            setJigsawGameObj(jigsawItem, itemObj);
        }

        return listJigsawBean;
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
    public void setListUVPosition(JigsawBean jigsaw, List<Vector2> listUVPostion)
    {
        if (jigsaw == null) { return; }
        jigsaw.ListUVPostion = listUVPostion;
    }

    /// <summary>
    /// 设置原图片
    /// </summary>
    /// <param name="jigsaw"></param>
    /// <param name="sourcePic"></param>
    public void setSourcePic(JigsawBean jigsaw, Texture2D sourcePic)
    {
        if (jigsaw == null) { return; }
        jigsaw.SourcePic = sourcePic;
    }
    //-----------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// 设置拼图块所有边的凹凸情况
    /// </summary>
    /// <param name="listJigsaw">所有的拼图块</param>
    /// <param name="jigsawItem">当前拼图块</param>
    /// <param name="horizontalJigsawNumber">横向块数</param>
    /// <param name="verticalJigsawNumber">纵向块数</param>
    private void setBulgeEdgeForItem(List<JigsawBean> listJigsaw, JigsawBean jigsawItem, int horizontalJigsawNumber, int verticalJigsawNumber)
    {
        int listJigsawCount = listJigsaw.Count;

        JigsawBulgeEnum leftBulge = JigsawBulgeEnum.Smooth;
        JigsawBulgeEnum rightBulge = JigsawBulgeEnum.Smooth;
        JigsawBulgeEnum belowBulge = JigsawBulgeEnum.Smooth;
        JigsawBulgeEnum aboveBulge = JigsawBulgeEnum.Smooth;

        Vector2 markLocation = jigsawItem.MarkLocation;
        if (markLocation == null)
            return;

        for (int childPostion = 0; childPostion < listJigsawCount; childPostion++)
        {
            JigsawBean childJigsaw = listJigsaw[childPostion];
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
    /// 设置顶点及UVPostion
    /// </summary>
    /// <param name="jigsawItem"></param>
    /// <param name="horizontalJigsawNumber"></param>
    /// <param name="verticalJigsawNumber"></param>
    private void setListVerticesAndListUVPositionForItem(JigsawBean jigsawItem, int horizontalJigsawNumber, int verticalJigsawNumber)
    {
        JigsawBulgeEnum[] listBulge = jigsawItem.ListBulge;
        Vector2 markLocation = jigsawItem.MarkLocation;
        Texture2D sourcePic = jigsawItem.SourcePic;

        if (listBulge == null)
            throw new Exception("没有凹凸参数");
        if (markLocation == null)
            throw new Exception("没有标记坐标");
        if (sourcePic == null)
            throw new Exception("没有源图片");

        List<Vector3> listVertices = new List<Vector3>();
        List<Vector2> listUVPostion = new List<Vector2>();
        //添加中心点坐标
        Vector3 centerVector = new Vector3(mJigsawWith / 2f, mJigsawHigh / 2f);
        listVertices.Add(centerVector);
        //添加中心点UV
        float sourcePicW = sourcePic.width;
        float sourcePicH = sourcePic.height;

        float itemPicW = sourcePicW / horizontalJigsawNumber;
        float itemPicH = sourcePicH / verticalJigsawNumber;

        float percentageItemW = itemPicW / sourcePicW;
        float percentageItemH = itemPicH / sourcePicH;

        float itemEachW = markLocation.x * percentageItemW;
        float itemEachH = markLocation.y * percentageItemH;

        Vector2 centerUVPostion = new Vector2
            (itemEachW + (percentageItemW / 2f), itemEachH + (percentageItemH / 2f));
        listUVPostion.Add(centerUVPostion);

        //
        listVertices.Add(new Vector3(0, 0, 0));
        listVertices.Add(new Vector3(0, mJigsawHigh, 0));
        listVertices.Add(new Vector3(mJigsawWith, mJigsawHigh, 0));
        listVertices.Add(new Vector3(mJigsawWith, 0, 0));

        listUVPostion.Add(new Vector2(itemEachW, itemEachH));
        listUVPostion.Add(new Vector2(itemEachW, itemEachH + percentageItemH));
        listUVPostion.Add(new Vector2(itemEachW + percentageItemW, itemEachH + percentageItemH));
        listUVPostion.Add(new Vector2(itemEachW + percentageItemW, itemEachH));

        setListVertices(jigsawItem, listVertices);
        setListUVPosition(jigsawItem, listUVPostion);
    }

    /// <summary>
    /// 比较相邻拼图碎片的凹凸情况 获取自身的凹凸情况
    /// </summary>
    /// <param name="jigsawBean"></param>
    /// <returns></returns>
    private JigsawBulgeEnum compareBulge(JigsawBean compareJigsaw, JigsawStyleNormalEdgeEnum edgeEnum)
    {
        if (compareJigsaw == null || edgeEnum == null)
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

        int bulgePostion;
        if (edgeEnum == JigsawStyleNormalEdgeEnum.Left)
            bulgePostion = 2;
        else if (edgeEnum == JigsawStyleNormalEdgeEnum.Above)
            bulgePostion = 3;
        else if (edgeEnum == JigsawStyleNormalEdgeEnum.Right)
            bulgePostion = 0;
        else if (edgeEnum == JigsawStyleNormalEdgeEnum.Below)
            bulgePostion = 1;
        else
            return JigsawBulgeEnum.Smooth;


        JigsawBulgeEnum compareBulgeEnum = compareBulgeList[bulgePostion];
        if (compareBulgeEnum == JigsawBulgeEnum.Smooth)
            return JigsawBulgeEnum.Smooth;
        else if (compareBulgeEnum == JigsawBulgeEnum.Bulge)
            return JigsawBulgeEnum.Sunken;
        else if (compareBulgeEnum == JigsawBulgeEnum.Sunken)
            return JigsawBulgeEnum.Bulge;
        else
            return JigsawBulgeEnum.Smooth;
    }


}



