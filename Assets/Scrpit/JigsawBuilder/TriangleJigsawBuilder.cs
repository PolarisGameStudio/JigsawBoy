using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class TriangleJigsawBuilder : BaseJigsawBuilder
{
    private float m_TriangleHigh;
    private float m_TriangleWith;
    public TriangleJigsawBuilder() : base()
    {
        m_TriangleWith = 1f;
        //m_TriangleHigh = Mathf.Sqrt(Mathf.Pow(m_TriangleWith,2)- Mathf.Pow(m_TriangleWith/2f, 2));
        //m_TriangleHigh = Mathf.Sin(Mathf.PI * 60 / 180) * m_TriangleWith;
        m_TriangleHigh = 0.6f;
    }

    public override JigsawBean buildJigsaw(float jigsawWith, float jigsawHigh, JigsawBulgeEnum[] bulgeEnums, Texture2D jigsawPic)
    {
        throw new System.NotImplementedException();
    }

    public override List<JigsawBean> buildJigsawList(int horizontalJigsawNumber, int verticalJigsawNumber, Texture2D jigsawPic)
    {
        return base.baseBuildJigsawList(JigsawStyleEnum.Triangle, horizontalJigsawNumber, verticalJigsawNumber, jigsawPic);
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
        //添加左边三角点
        getTriangleVertices(listVertices, leftBulge, Direction2DEnum.Left, withX, highY);

        //添加左上角点
        listVertices.Add(new Vector3(-withX, highY));
        //添加上边三角形
        getTriangleVertices(listVertices, aboveBulge, Direction2DEnum.Above, withX, highY);

        //添加右上角点
        listVertices.Add(new Vector3(withX, highY));
        //添加右边三角形
        getTriangleVertices(listVertices, rightBulge, Direction2DEnum.Right, withX, highY);

        //添加右下角点
        listVertices.Add(new Vector3(withX, -highY));
        //添加下边三角形
        getTriangleVertices(listVertices, belowBulge, Direction2DEnum.Below, withX, highY);

        setListVertices(jigsawItem, listVertices);
    }

    private void getTriangleVertices(List<Vector3> listVertices, JigsawBulgeEnum jigsawBulge,Direction2DEnum direction ,float withX, float highY)
    {
        if (jigsawBulge.Equals(JigsawBulgeEnum.Smooth))
        {
            return;
        }
        //添加第一个点
        if (direction.Equals(Direction2DEnum.Left)) {
            listVertices.Add(new Vector3(-withX,-m_TriangleWith/2f));
        }
        else if (direction.Equals(Direction2DEnum.Above))
        {
            listVertices.Add(new Vector3(-m_TriangleWith / 2f, highY));
        }
        else if (direction.Equals(Direction2DEnum.Right))
        {
            listVertices.Add(new Vector3(withX, m_TriangleWith / 2f));
        }
        else if (direction.Equals(Direction2DEnum.Below))
        {
            listVertices.Add(new Vector3(m_TriangleWith / 2f, -highY));
        }
        //添加第二个点
        if (direction.Equals(Direction2DEnum.Left))
        {
            if (jigsawBulge.Equals(JigsawBulgeEnum.Bulge))
            {
                listVertices.Add(new Vector3(-withX+-m_TriangleHigh, 0));
            }
            else if (jigsawBulge.Equals(JigsawBulgeEnum.Sunken))
            {
                listVertices.Add(new Vector3(-withX+ m_TriangleHigh, 0));
            }
        }
        else if (direction.Equals(Direction2DEnum.Above))
        {
            if (jigsawBulge.Equals(JigsawBulgeEnum.Bulge))
            {
                listVertices.Add(new Vector3(0, highY + m_TriangleHigh));
            }
            else if (jigsawBulge.Equals(JigsawBulgeEnum.Sunken))
            {
                listVertices.Add(new Vector3(0, highY - m_TriangleHigh));
            }
        }
        else if (direction.Equals(Direction2DEnum.Right))
        {
            if (jigsawBulge.Equals(JigsawBulgeEnum.Bulge))
            {
                listVertices.Add(new Vector3(withX + m_TriangleHigh, 0));
            }
            else if (jigsawBulge.Equals(JigsawBulgeEnum.Sunken))
            {
                listVertices.Add(new Vector3(withX - m_TriangleHigh, 0));
            }
        }
        else if (direction.Equals(Direction2DEnum.Below))
        {
            if (jigsawBulge.Equals(JigsawBulgeEnum.Bulge))
            {
                listVertices.Add(new Vector3(0, -highY + -m_TriangleHigh));
            }
            else if (jigsawBulge.Equals(JigsawBulgeEnum.Sunken))
            {
                listVertices.Add(new Vector3(0, -highY +m_TriangleHigh));
            }
        }
        //添加第三个点
        if (direction.Equals(Direction2DEnum.Left))
        {
            listVertices.Add(new Vector3(-withX, m_TriangleWith / 2f));
        }
        else if (direction.Equals(Direction2DEnum.Above))
        {
            listVertices.Add(new Vector3(m_TriangleWith / 2f, highY));
        }
        else if (direction.Equals(Direction2DEnum.Right))
        {
            listVertices.Add(new Vector3(withX, -m_TriangleWith / 2f));
        }
        else if (direction.Equals(Direction2DEnum.Below))
        {
            listVertices.Add(new Vector3(-m_TriangleWith / 2f, -highY));
        }
    }
}