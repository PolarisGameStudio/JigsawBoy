using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


interface IBaseJigsawBuilder
{
    /// <summary>
    /// 创建拼图碎片集合
    /// </summary>
    /// <param name="horizontalJigsawNumber">横向块数</param>
    /// <param name="verticalJigsawNumber">纵向块数</param>
    /// <param name="jigsawPic">需要分解的图片</param>
    /// <returns></returns>
    List<JigsawBean> buildJigsawList(int horizontalJigsawNumber, int verticalJigsawNumber, Texture2D jigsawPic);

    /// <summary>
    /// 创建一个拼图
    /// </summary>
    /// <param name="jigsawWith"></param>
    /// <param name="jigsawHigh"></param>
    /// <param name="bulgeEnums"></param>
    /// <returns></returns>
    JigsawBean buildJigsaw(float jigsawWith, float jigsawHigh, JigsawBulgeEnum[] bulgeEnums, Texture2D jigsawPic);

    /// <summary>
    /// 设置拼图碎片对象
    /// </summary>
    /// <param name="jigsaw"></param>
    void setJigsawGameObj(JigsawBean jigsaw, GameObject gameObj);

    /// <summary>
    /// 设置当前样式
    /// </summary>
    void setJigsawStyle(JigsawBean jigsaw, JigsawStyleEnum style);

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


    /// <summary>
    /// 设置拼图UV宽
    /// </summary>
    /// <param name="jigsaw"></param>
    /// <param name="jigsawWith"></param>
    void setJigsawUVWith(JigsawBean jigsaw, float jigsawUVWith);

    /// <summary>
    /// 设置拼图UV高
    /// </summary>
    /// <param name="jigsaw"></param>
    /// <param name="jigsawHigh"></param>
    void setJigsawUVHigh(JigsawBean jigsaw, float jigsawUVHigh);

    /// <summary>
    /// 设置拼图块数
    /// </summary>
    /// <param name="jigsaw"></param>
    /// <param name="jigsawHigh"></param>
    void setJigsawNumber(JigsawBean jigsaw, int jigsawNumber);
}

