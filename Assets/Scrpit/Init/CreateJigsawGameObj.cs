using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CreateJigsawGameObj : MonoBehaviour
{

    /// <summary>
    /// 创建拼图对象
    /// </summary>
    /// <param name="jigsawPic">生成拼图所需图片</param>
    /// <returns></returns>
    private static GameObject createGameObjForJigsaw(Texture2D jigsawPic)
    {
        GameObject jigsawGameObj = new GameObject();

        jigsawGameObj.AddComponent<MeshRenderer>();
        jigsawGameObj.AddComponent<MeshFilter>();

        //获取拼图的render并设置贴图
        Renderer jigsawRenderer = jigsawGameObj.GetComponent<Renderer>();
        Material jigsawMaterial = jigsawRenderer.material;
        jigsawMaterial.mainTexture = jigsawPic;

        return jigsawGameObj;
    }

    /// <summary>
    /// 创建拼图的坐标点
    /// </summary>
    /// <param name="listVector3">拼图的顶点坐标</param>
    /// <returns>拼图顶点坐标</returns>
    private static Vector3[] createJigsawVertices(List<Vector3> listVertices)
    {
        if (listVertices == null)
            return null;
        //获取顶点列表大小
        int listCount = listVertices.Count;
        //创建顶点Vector3数组
        Vector3[] vertices = new Vector3[listCount];
        //将链表中的顶点坐标赋值给vertices  
        for (int i = 0; i < listCount; i++)
        {
            vertices[i] = listVertices[i];
        }
        return vertices;
    }

    /// <summary>
    /// 创建拼图三角形索引
    /// </summary>
    /// <param name="listVertices">拼图的顶点坐标</param>
    /// <returns>拼图三角形索引数组</returns>
    private static int[] createJigsawTriangles(List<Vector3> listVertices)
    {
        if (listVertices == null)
            return null;
        //获取顶点列表大小
        int listCount = listVertices.Count;
        //三角形个数  
        int trianglesCount = listCount - 2;
        //三角学顶点数
        int[] triangles = new int[3*trianglesCount];
        //根据三角形的个数，来计算绘制三角形的顶点顺序（索引）  
        //注意顺序，否则可能看不到，（0,1,2）（0，2,3）
        for (int i = 0; i < trianglesCount; i++)
        {
            triangles[3 * i] = 0;
            triangles[3 * i + 1] = i + 1;
            triangles[3 * i + 2] = i + 2;
        }
        return triangles;
    }

    /// <summary>
    /// 创建拼图UV顶点
    /// </summary>
    /// <param name="listUVPostion"></param>
    /// <returns></returns>
    private static Vector2[] createJigsawUVPostion(List<Vector2> listUVPostion)
    {
        if (listUVPostion == null)
            return null;
        //获取UV列表大小
        int listCount = listUVPostion.Count;
        //UV数组  
        Vector2[] uvVertices = new Vector2[listCount];
        //将链表中的UV坐标赋值给vertices  
        for (int i = 0; i < listCount; i++)
        {
            uvVertices[i] = listUVPostion[i];
        }
        return uvVertices;
    }

    /// <summary>
    /// 获取拼图GameObj
    /// </summary>
    /// <param name="listVertices">需要生成的拼图块顶点坐标</param>
    /// <param name="listUVPostion">需要生成的拼图块图片UV坐标</param>
    /// <param name="jigsawPic">生成所需图片</param>
    /// <returns></returns>
    public static GameObject getJigsawGameObj(List<Vector3> listVertices, List<Vector2> listUVPostion, Texture2D jigsawPic)
    {
        if (listVertices == null)
            throw new Exception("没有顶点坐标");
        if (listVertices.Count < 3)
            throw new Exception("顶点坐标小于3");
        if (listUVPostion == null)
            throw new Exception("没有图片UV坐标");
        if (!listUVPostion.Count.Equals(listVertices.Count))
            throw new Exception("UV坐标与定点坐标数量不对等");
        if (jigsawPic == null)
            throw new Exception("没有生成拼图所需图片");


        //创建拼图的游戏对象
        GameObject jigsawGameObj = createGameObjForJigsaw(jigsawPic);
        //获取拼图游戏对象的mesh
        Mesh jigsawMesh = jigsawGameObj.GetComponent<MeshFilter>().mesh;
        //创建拼图的坐标点
        Vector3[] jigsawVertices = createJigsawVertices(listVertices);
        //创建拼图的三角形索引
        int[] jigsawTriangles = createJigsawTriangles(listVertices);
        //创建拼图的UV坐标点
        Vector2[] jigsawUVVertices = createJigsawUVPostion(listUVPostion);

        //将拼图坐标点给拼图mesh赋值
        if (jigsawVertices != null)
            jigsawMesh.vertices = jigsawVertices;
        //将拼图三角形索引给拼图mesh赋值
        if (jigsawTriangles != null)
            jigsawMesh.triangles = jigsawTriangles;
        //将拼图UV坐标点给拼图mesh赋值
        if (jigsawUVVertices != null)
            jigsawMesh.uv = jigsawUVVertices;

        return jigsawGameObj;
    }

}
