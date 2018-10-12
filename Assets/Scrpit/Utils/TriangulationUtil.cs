using UnityEngine;
using UnityEditor;
using System.Collections.Generic;


/// <summary>
/// 对多边形处理
/// </summary>
public class TriangulationUtil
{

    public class VertexNode
    {
        public VertexNode(Vector3 vector, int position)
        {
            this.vector = vector;
            this.position = position;
        }
        public Vector3 vector;
        public int position;
    }

    /// <summary>
    /// 获取多边形三角序列 （注：顺时针顶点）
    /// </summary>
    /// <param name="polygonVertexs"></param>
    /// <returns></returns>
    public static int[] GetTriangles(List<Vector3> polygonVertexs)
    {
        //获取多边形三角形数量
        int traianglesNumber = polygonVertexs.Count - 2;
        //根据多边形数量实例化三角序列数组
        int[] triangles = new int[traianglesNumber * 3];

        //遍历每个顶点判断是否是耳朵 并且是凸顶点
        int vertexsNumber = polygonVertexs.Count;
        int trianglePosition = 0;

        List<VertexNode> listNode = new List<VertexNode>();
        for (int i = 0; i < vertexsNumber; i++)
        {
            VertexNode itemNode = new VertexNode(polygonVertexs[i], i);
            listNode.Add(itemNode);
        }

        while (listNode.Count>=3)
        {
            for (int i = 0; i < listNode.Count && listNode.Count >= 3; i++)
            {
                VertexNode leftPosition = (i == 0 ? listNode[listNode.Count - 1] : listNode[i - 1]);
                VertexNode rightPosition = (i == listNode.Count - 1 ? listNode[0] : listNode[i + 1]);
                VertexNode currentPosition = listNode[i];

                //判断是否为凸点
                bool isConvex = IsConvex(leftPosition.vector, currentPosition.vector, rightPosition.vector);
                if (isConvex)
                {
                    //如果是凸点再判断是否是耳朵
                    bool isEar = true;
                    foreach (VertexNode itemVertex in listNode)
                    {
                        if(itemVertex == currentPosition|| itemVertex == leftPosition || itemVertex == rightPosition)
                        {
                            continue;
                        }
                        bool isInTriangle = GeometryUtil.VertexIsInTriangle(itemVertex.vector, currentPosition.vector, leftPosition.vector, rightPosition.vector);
                        if (isInTriangle)
                        {
                            isEar = false;
                            break;
                        }
                    }
                    if (isEar)
                    {
                        triangles[trianglePosition] = leftPosition.position;
                        triangles[trianglePosition + 1] = currentPosition.position;
                        triangles[trianglePosition + 2] = rightPosition.position;
                        trianglePosition += 3;
                        listNode.Remove(listNode[i]);
                        i--;
                    }
                }
            }
        }
        return triangles;
    }

    /// <summary>
    /// 判断是否是凸点
    /// </summary>
    /// <param name="left"></param>
    /// <param name="center"></param>
    /// <param name="right"></param>
    public static bool IsConvex(Vector3 left, Vector3 center, Vector3 right)
    {
        Vector3 cross = Vector3.Cross((center - left).normalized, (center - right).normalized);
        bool isConvex = false;
        if (cross.z > 0)
        {
            isConvex = true;
        }
        else
        {
            isConvex = false;
        }
        return isConvex;
    }
}
