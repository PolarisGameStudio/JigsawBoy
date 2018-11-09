using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class PentagramJigsawBuilder : BaseJigsawBuilder
{
    private float m_PentagramZoom;
    private float m_PentagramWith;
    private float m_PentagramFoot;
    public PentagramJigsawBuilder() : base()
    {
        m_PentagramWith = 1f;
        m_PentagramZoom = 1f;
        m_PentagramFoot = 0.02f;
    }
    public override JigsawBean buildJigsaw(float jigsawWith, float jigsawHigh, JigsawBulgeEnum[] bulgeEnums, Texture2D jigsawPic)
    {
        throw new System.NotImplementedException();
    }

    public override List<JigsawBean> buildJigsawList(int horizontalJigsawNumber, int verticalJigsawNumber, Texture2D jigsawPic)
    {
        return base.baseBuildJigsawList(JigsawStyleEnum.Pentagram, horizontalJigsawNumber, verticalJigsawNumber, jigsawPic);
    }

    public override void setListUVPosition(JigsawBean jigsawItem)
    {
        throw new System.NotImplementedException();
    }

    public override void setListVerticesForItem(JigsawBean jigsawItem)
    {
        base.baseSetListVerticesForItem(jigsawItem);
        List<Vector3> listVertices = new List<Vector3>();

        float withX = jigsawItem.JigsawWith / 2f;
        float highY = jigsawItem.JigsawHigh / 2f;

        JigsawBulgeEnum[] listBulge = jigsawItem.ListBulge;
        JigsawBulgeEnum leftBulge = listBulge[0];
        JigsawBulgeEnum aboveBulge = listBulge[1];
        JigsawBulgeEnum rightBulge = listBulge[2];
        JigsawBulgeEnum belowBulge = listBulge[3];

        //添加左下角点
        listVertices.Add(new Vector3(-withX, -highY));
        //添加左边点
        getPentagramVertices(listVertices, leftBulge, Direction2DEnum.Left, withX, highY);

        //添加左上角点
        listVertices.Add(new Vector3(-withX, highY));
        //添加上边
        getPentagramVertices(listVertices, aboveBulge, Direction2DEnum.Above, withX, highY);

        //添加右上角点
        listVertices.Add(new Vector3(withX, highY));
        //添加右边
        getPentagramVertices(listVertices, rightBulge, Direction2DEnum.Right, withX, highY);

        //添加右下角点
        listVertices.Add(new Vector3(withX, -highY));
        //添加下边
        getPentagramVertices(listVertices, belowBulge, Direction2DEnum.Below, withX, highY);

        setListVertices(jigsawItem, listVertices);
    }

    private void getPentagramVertices(List<Vector3> listVertices, JigsawBulgeEnum jigsawBulge, Direction2DEnum direction, float withX, float highY)
    {
        List<Vector3> convex;
        List<Vector3> concave;
        List<Vector3> leftConcavePositionList = new List<Vector3>();
        leftConcavePositionList.Add(new Vector3(-withX, -m_PentagramFoot));

        leftConcavePositionList.Add(new Vector3(-withX + 0.2f * m_PentagramZoom - m_PentagramWith / 2f, -0.15f * m_PentagramZoom));

        leftConcavePositionList.Add(new Vector3(-withX + 0.15f * m_PentagramZoom - m_PentagramWith / 2f, -0.47f * m_PentagramZoom));

        leftConcavePositionList.Add(new Vector3(-withX + -0.08f * m_PentagramZoom - m_PentagramWith / 2f, -0.24f * m_PentagramZoom));

        leftConcavePositionList.Add(new Vector3(-withX + -0.4f * m_PentagramZoom - m_PentagramWith / 2f, -0.29f * m_PentagramZoom));

        leftConcavePositionList.Add(new Vector3(-withX + -0.25f * m_PentagramZoom - m_PentagramWith / 2f, 0f));

        leftConcavePositionList.Add(new Vector3(-withX + -0.4f * m_PentagramZoom - m_PentagramWith / 2f, 0.29f * m_PentagramZoom));

        leftConcavePositionList.Add(new Vector3(-withX + -0.08f * m_PentagramZoom - m_PentagramWith / 2f, 0.24f * m_PentagramZoom));

        leftConcavePositionList.Add(new Vector3(-withX + 0.15f * m_PentagramZoom - m_PentagramWith / 2f, 0.47f * m_PentagramZoom));

        leftConcavePositionList.Add(new Vector3(-withX + 0.2f * m_PentagramZoom - m_PentagramWith / 2f, 0.15f * m_PentagramZoom));

        leftConcavePositionList.Add(new Vector3(-withX, m_PentagramFoot));
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