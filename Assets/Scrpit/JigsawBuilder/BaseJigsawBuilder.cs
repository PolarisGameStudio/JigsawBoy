using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public abstract class BaseJigsawBuilder : IBaseJigsawBuilder
{
    //拼图缩放大小
    protected float m_JigsawScale;
    //拼图总长度
    protected float m_JigsawUnitSize;

    //初始化参数
    public BaseJigsawBuilder()
    {
        m_JigsawScale = 1f;
        m_JigsawUnitSize = 3f * m_JigsawScale;
    }

    public abstract JigsawBean buildJigsaw(float jigsawWith, float jigsawHigh, JigsawBulgeEnum[] bulgeEnums, Texture2D jigsawPic);
    public abstract List<JigsawBean> buildJigsawList(int horizontalJigsawNumber, int verticalJigsawNumber, Texture2D jigsawPic);

    public abstract void setListVerticesForItem(JigsawBean jigsawItem);
    public abstract void setListUVPositionForItem(JigsawBean jigsawItem);
    public abstract void setListUVPosition(JigsawBean jigsawItem);

    /// <summary>
    /// 基础-创建单个数据
    /// </summary>
    /// <param name="jigsawWith"></param>
    /// <param name="jigsawHigh"></param>
    /// <param name="bulgeEdge"></param>
    /// <param name="jigsawPic"></param>
    /// <returns></returns>
    public JigsawBean baseBuildJigsaw(float jigsawWith, float jigsawHigh, JigsawBulgeEnum[] bulgeEdge, Texture2D jigsawPic)
    {

        //生成拼图对象
        JigsawBean jigsawItem = new JigsawBean();
        //确认拼图样式
        setJigsawStyle(jigsawItem, JigsawStyleEnum.Def);
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

    /// <summary>
    /// 基础-创建数据列表
    /// </summary>
    /// <param name="horizontalJigsawNumber"></param>
    /// <param name="verticalJigsawNumber"></param>
    /// <param name="jigsawPic"></param>
    /// <returns></returns>
    public List<JigsawBean> baseBuildJigsawList(JigsawStyleEnum style, int horizontalJigsawNumber, int verticalJigsawNumber, Texture2D jigsawPic)
    {
        // 根据图片获取item宽高和UV
        JigsawBean tempBean = getWithAndHighByTexture2D(horizontalJigsawNumber, verticalJigsawNumber, jigsawPic);
        // 获取拼图总块数
        int jigsawTotalNumber = verticalJigsawNumber * horizontalJigsawNumber;
        List<JigsawBean> listJigsawBean = new List<JigsawBean>();
        for (int horizontal = 0; horizontal < horizontalJigsawNumber; horizontal++)
        {
            for (int vertical = 0; vertical < verticalJigsawNumber; vertical++)
            {
                //生成拼图对象
                JigsawBean jigsawItem = new JigsawBean();
                //确认拼图样式
                setJigsawStyle(jigsawItem, style);
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
    /// 基础-设置item坐标点
    /// </summary>
    /// <param name="jigsawItem"></param>
    public void baseSetListVerticesForItem(JigsawBean jigsawItem, List<Vector3> listVertices)
    {
        //添加中心点坐标
        Vector3 centerVector = new Vector3(0, 0);
        setCenterVector(jigsawItem, centerVector);
        listVertices.Add(centerVector);
    }


    /// <summary>
    /// 设置UV坐标
    /// </summary>
    /// <param name="jigsawItem"></param>
    public void baseSetListUVPositionForItem(JigsawBean jigsawItem)
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
    /// 设置当前样式
    /// </summary>
    public void setJigsawStyle(JigsawBean jigsaw, JigsawStyleEnum style)
    {
        if (jigsaw == null) { return; }
        jigsaw.JigsawStyle = style;
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
    /// 设置拼图碎片对象
    /// </summary>
    /// <param name="jigsaw"></param>
    public void setJigsawGameObj(JigsawBean jigsaw, GameObject gameObj)
    {
        if (jigsaw == null) { return; }
        jigsaw.JigsawGameObj = gameObj;
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
    //-----------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// 根据图片获取item宽高和UV
    /// </summary>
    /// <param name="horizontalJigsawNumber"></param>
    /// <param name="verticalJigsawNumber"></param>
    /// <param name="jigsawPic"></param>
    protected JigsawBean getWithAndHighByTexture2D(int horizontalJigsawNumber, int verticalJigsawNumber, Texture2D jigsawPic)
    {
        JigsawBean tempBean = new JigsawBean();

        float picItemWith = (float)jigsawPic.width / (float)horizontalJigsawNumber;
        float picItemHigh = (float)jigsawPic.height / (float)verticalJigsawNumber;

        tempBean.JigsawUVWith = picItemWith / (float)jigsawPic.width;
        tempBean.JigsawUVHigh = picItemHigh / (float)jigsawPic.height;

        if (picItemWith > picItemHigh)
        {
            //拼图的高
            tempBean.JigsawHigh = m_JigsawUnitSize;
            //拼图的宽
            tempBean.JigsawWith = tempBean.JigsawHigh * (picItemWith / picItemHigh);
        }
        else
        {
            //拼图的宽
            tempBean.JigsawWith = m_JigsawUnitSize;
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
    protected void setBulgeEdgeForItem(List<JigsawBean> listJigsaw, JigsawBean jigsawItem)
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
    /// 比较相邻拼图碎片的凹凸情况 获取自身的凹凸情况
    /// </summary>
    /// <param name="jigsawBean"></param>
    /// <returns></returns>
    protected JigsawBulgeEnum compareBulge(JigsawBean compareJigsaw, JigsawStyleNormalEdgeEnum edgeEnum)
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
}