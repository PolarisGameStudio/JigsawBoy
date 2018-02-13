using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


interface IBaseJigsawBuilder
{
    /// <summary>
    /// 创建普通样式的拼图碎片集合
    /// </summary>
    /// <param name="horizontalJigsawNumber">横向块数</param>
    /// <param name="verticalJigsawNumber">纵向块数</param>
    /// <param name="jigsawPic">需要分解的图片</param>
    /// <returns></returns>
    List<JigsawBean> buildJigsawList(int horizontalJigsawNumber, int verticalJigsawNumber, Texture2D jigsawPic);

    /// <summary>
    /// 设置拼图碎片对象
    /// </summary>
    /// <param name="jigsaw"></param>
    void setJigsawGameObj(JigsawBean jigsaw, GameObject gameObj);

    /// <summary>
    /// 设置当前样式
    /// </summary>
    void setJigsawStyle(JigsawBean jigsaw);

    /// <summary>
    /// 设置当前拼图碎片相对图片的坐标
    /// </summary>
    /// <param name="markX">X坐标</param>
    /// <param name="markY">Y坐标</param>
    void setMarkLocation(JigsawBean jigsaw,int markX, int markY);

    /// <summary>
    /// 设置每个边的凹凸情况
    /// </summary>
    /// <param name="jigsawBulges">凹凸集合</param>
    void setBulgeEdge(JigsawBean jigsaw,JigsawBulgeEnum[] jigsawBulges);

    /// <summary>
    /// 设置顶点坐标
    /// </summary>
    /// <param name="jigsaw"></param>
    void setListVertices(JigsawBean jigsaw, List<Vector3> listVertices);

    /// <summary>
    /// 设置UV坐标
    /// </summary>
    /// <param name="jigsaw"></param>
    void setListUVPosition(JigsawBean jigsaw, List<Vector2> listUVposition);

    /// <summary>
    /// 设置原图片
    /// </summary>
    /// <param name="jigsaw"></param>
    /// <param name="sourcePic"></param>
    void setSourcePic(JigsawBean jigsaw,Texture2D sourcePic);

    /// <summary>
    /// 设置中心点坐标
    /// </summary>
    /// <param name="jigsaw"></param>
    /// <param name="centerVector"></param>
    void setCenterVector(JigsawBean jigsaw,Vector3 centerVector);

    /// <summary>
    /// 设置拼图宽
    /// </summary>
    /// <param name="jigsaw"></param>
    /// <param name="jigsawWith"></param>
    void setJigsawWith(JigsawBean jigsaw,float jigsawWith);

    /// <summary>
    /// 设置拼图高
    /// </summary>
    /// <param name="jigsaw"></param>
    /// <param name="jigsawHigh"></param>
    void setJigsawHigh(JigsawBean jigsaw,float jigsawHigh);
}

