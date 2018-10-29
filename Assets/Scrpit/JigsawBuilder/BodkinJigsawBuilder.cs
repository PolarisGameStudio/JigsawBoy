using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class BodkinJigsawBuilder : BaseJigsawBuilder
{
    public override JigsawBean buildJigsaw(float jigsawWith, float jigsawHigh, JigsawBulgeEnum[] bulgeEnums, Texture2D jigsawPic)
    {
        throw new System.NotImplementedException();
    }

    public override List<JigsawBean> buildJigsawList(int horizontalJigsawNumber, int verticalJigsawNumber, Texture2D jigsawPic)
    {
        return base.baseBuildJigsawList(JigsawStyleEnum.Bodkin, horizontalJigsawNumber, verticalJigsawNumber, jigsawPic);
    }

    public override void setListUVPosition(JigsawBean jigsawItem)
    {
        throw new System.NotImplementedException();
    }

    public override void setListVerticesForItem(JigsawBean jigsawItem)
    {
        List<Vector3> listVertices = new List<Vector3>();
        base.baseSetListVerticesForItem(jigsawItem);

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
        getBulgeVertices(listVertices, leftBulge, Direction2DEnum.Left, withX, highY);

        //添加左上角点
        listVertices.Add(new Vector3(-withX, highY));
        //添加上边
        getBulgeVertices(listVertices, aboveBulge, Direction2DEnum.Above, withX, highY);

        //添加右上角点
        listVertices.Add(new Vector3(withX, highY));
        //添加右边
        getBulgeVertices(listVertices, rightBulge, Direction2DEnum.Right, withX, highY);

        //添加右下角点
        listVertices.Add(new Vector3(withX, -highY));
        //添加下边
        getBulgeVertices(listVertices, belowBulge, Direction2DEnum.Below, withX, highY);

        setListVertices(jigsawItem, listVertices);
    }

    private void getBulgeVertices(List<Vector3> listVertices, JigsawBulgeEnum jigsawBulge, Direction2DEnum direction, float withX, float highY)
    {
        List<Vector3> convex;
        List<Vector3> concave;
        List<Vector3> leftConcavePositionList = new List<Vector3>();

        //TODO 添加传统拼图形状
        leftConcavePositionList.Add(new Vector3(-withX + 0.2f, -0.4f));
        leftConcavePositionList.Add(new Vector3(-withX + 0.18f, -0.35f));
        leftConcavePositionList.Add(new Vector3(-withX + 0.15f, -0.3f));
        leftConcavePositionList.Add(new Vector3(-withX + 0.1f, -0.25f));
        leftConcavePositionList.Add(new Vector3(-withX, -0.22f));
        leftConcavePositionList.Add(new Vector3(-withX - 0.1f, -0.25f));
        leftConcavePositionList.Add(new Vector3(-withX - 0.15f, -0.3f));
        leftConcavePositionList.Add(new Vector3(-withX - 0.18f, -0.35f));
        leftConcavePositionList.Add(new Vector3(-withX - 0.2f, -0.4f));

        leftConcavePositionList.Add(new Vector3(-withX - 0.5f, -0.1f));
        leftConcavePositionList.Add(new Vector3(-withX - 1f, 0));
        leftConcavePositionList.Add(new Vector3(-withX - 0.5f, 0.1f));

        leftConcavePositionList.Add(new Vector3(-withX - 0.20f, 0.4f));
        leftConcavePositionList.Add(new Vector3(-withX - 0.18f, 0.35f));
        leftConcavePositionList.Add(new Vector3(-withX - 0.15f, 0.3f));
        leftConcavePositionList.Add(new Vector3(-withX - 0.1f,0.25f));
        leftConcavePositionList.Add(new Vector3(-withX, 0.22f));
        leftConcavePositionList.Add(new Vector3(-withX + 0.1f, 0.25f));
        leftConcavePositionList.Add(new Vector3(-withX + 0.15f, 0.3f));
        leftConcavePositionList.Add(new Vector3(-withX + 0.18f, 0.35f));
        leftConcavePositionList.Add(new Vector3(-withX + 0.20f, 0.4f));

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