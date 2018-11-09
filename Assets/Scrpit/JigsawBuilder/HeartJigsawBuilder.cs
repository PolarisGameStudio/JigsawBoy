using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class HeartJigsawBuilder : BaseJigsawBuilder
{
    private float m_HeartWith;
    private float m_HeartHigh;
    private float m_HeartFoot;

    public HeartJigsawBuilder() : base()
    {
        m_HeartWith = 1f;
        m_HeartHigh = 1f;
        m_HeartFoot = 0.1f;
    }

    public override JigsawBean buildJigsaw(float jigsawWith, float jigsawHigh, JigsawBulgeEnum[] bulgeEnums, Texture2D jigsawPic)
    {
        throw new System.NotImplementedException();
    }

    public override List<JigsawBean> buildJigsawList(int horizontalJigsawNumber, int verticalJigsawNumber, Texture2D jigsawPic)
    {
        return base.baseBuildJigsawList(JigsawStyleEnum.Heart, horizontalJigsawNumber, verticalJigsawNumber, jigsawPic);
    }

    public override void setListUVPosition(JigsawBean jigsawItem)
    {
        base.baseSetListUVPositionForItem(jigsawItem);
    }

    public override void setListVerticesForItem(JigsawBean jigsawItem)
    {
        base.baseSetListVerticesForItem(jigsawItem);
        float withX = jigsawItem.JigsawWith / 2f;
        float highY = jigsawItem.JigsawHigh / 2f;

        //根据凹凸属性生成坐标点
        List<Vector3> listVertices = new List<Vector3>();
        JigsawBulgeEnum[] listBulge = jigsawItem.ListBulge;
        JigsawBulgeEnum leftBulge = listBulge[0];
        JigsawBulgeEnum aboveBulge = listBulge[1];
        JigsawBulgeEnum rightBulge = listBulge[2];
        JigsawBulgeEnum belowBulge = listBulge[3];

        //添加左下角点
        listVertices.Add(new Vector3(-withX, -highY));
        //添加左边点
        getHeartVertices(listVertices, leftBulge, Direction2DEnum.Left, withX, highY);

        //添加左上角点
        listVertices.Add(new Vector3(-withX, highY));
        //添加上边
        getHeartVertices(listVertices, aboveBulge, Direction2DEnum.Above, withX, highY);

        //添加右上角点
        listVertices.Add(new Vector3(withX, highY));
        //添加右边
        getHeartVertices(listVertices, rightBulge, Direction2DEnum.Right, withX, highY);

        //添加右下角点
        listVertices.Add(new Vector3(withX, -highY));
        //添加下边
        getHeartVertices(listVertices, belowBulge, Direction2DEnum.Below, withX, highY);

        setListVertices(jigsawItem, listVertices);
    }

    private void getHeartVertices(List<Vector3> listVertices, JigsawBulgeEnum jigsawBulge, Direction2DEnum direction, float withX, float highY)
    {
        List<Vector3> convex;
        List<Vector3> concave;
        List<Vector3> leftConcavePositionList = new List<Vector3>();

        leftConcavePositionList.Add(new Vector3(-withX, -m_HeartFoot / 2f));
        leftConcavePositionList.Add(new Vector3(-withX + -m_HeartHigh / 2f, -m_HeartWith / 2f));
        leftConcavePositionList.Add(new Vector3(-withX + -m_HeartHigh * 4.5f / 6f, -m_HeartWith * 5.5f / 12f));
        leftConcavePositionList.Add(new Vector3(-withX + -m_HeartHigh * 5.5f / 6f, -m_HeartWith * 4.5f / 12f));

        leftConcavePositionList.Add(new Vector3(-withX + -m_HeartHigh * 5.8f / 6f, -m_HeartWith * 3.2f / 12f));
        leftConcavePositionList.Add(new Vector3(-withX + -m_HeartHigh * 5.8f / 6f, -m_HeartWith * 2.8f / 12f));

        leftConcavePositionList.Add(new Vector3(-withX + -m_HeartHigh * 5.5f / 6f, -m_HeartWith * 1.5f / 12f));
        leftConcavePositionList.Add(new Vector3(-withX + -m_HeartHigh * 5f / 6f, -m_HeartWith * 0.5f / 12f));

        leftConcavePositionList.Add(new Vector3(-withX + -m_HeartHigh*3f / 4f, 0f));

        leftConcavePositionList.Add(new Vector3(-withX + -m_HeartHigh * 5f / 6f, m_HeartWith * 0.5f / 12f));
        leftConcavePositionList.Add(new Vector3(-withX + -m_HeartHigh * 5.5f / 6f, m_HeartWith * 1.5f / 12f));

        leftConcavePositionList.Add(new Vector3(-withX + -m_HeartHigh * 5.8f / 6f, m_HeartWith * 2.8f / 12f));
        leftConcavePositionList.Add(new Vector3(-withX + -m_HeartHigh * 5.8f / 6f, m_HeartWith * 3.2f / 12f));

        leftConcavePositionList.Add(new Vector3(-withX + -m_HeartHigh * 5.5f / 6f, m_HeartWith * 4.5f / 12f));
        leftConcavePositionList.Add(new Vector3(-withX + -m_HeartHigh * 4.5f / 6f, m_HeartWith * 5.5f / 12f));
        leftConcavePositionList.Add(new Vector3(-withX + -m_HeartHigh / 2f, m_HeartWith / 2f));
        leftConcavePositionList.Add(new Vector3(-withX, m_HeartFoot / 2f));

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