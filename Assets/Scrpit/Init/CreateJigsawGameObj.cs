using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CreateJigsawGameObj
{

    /// <summary>
    /// 获取拼图GameObj
    /// </summary>
    /// <param name="JigsawBean">需要生成的拼图块数据</param>
    /// <returns></returns>
    public static GameObject getJigsawGameObj(JigsawBean jigsawData)
    {
        if (jigsawData == null)
            throw new Exception("没有拼图数据");
        List<Vector3> listVertices = jigsawData.ListVertices;
        List<Vector2> listUVPostion = jigsawData.ListUVPostion;
        Texture2D jigsawPic = jigsawData.SourcePic;
        Vector2 markLocation = jigsawData.MarkLocation;
      

        if (listVertices == null)
            throw new Exception("没有顶点坐标");
        if (listVertices.Count < 3)
            throw new Exception("顶点坐标小于3");
        if (listUVPostion == null)
            throw new Exception("没有图片UV坐标");
        if (!listUVPostion.Count.Equals(listVertices.Count))
            throw new Exception("UV坐标与定点坐标数量不对等");
        if (markLocation == null)
            throw new Exception("没有标记坐标");
        if (jigsawPic == null)
            throw new Exception("没有生成拼图所需图片");

        //创建拼图的游戏对象
        String gameObjName = jigsawPic.name + "_X" + markLocation.x + "_Y" + markLocation.y;
        GameObject jigsawGameObj = createGameObjForJigsaw(gameObjName);

        //设置贴图
        setRenderer(jigsawGameObj, jigsawPic);
        //设置网格
        setMeshFilter(jigsawGameObj, listVertices, listUVPostion);
        //设置刚体
        setRigidbody2D(jigsawGameObj);
        //设置2D碰撞器
        //setCollider2D(jigsawGameObj, listVertices);


        return jigsawGameObj;
    }

    /// <summary>
    /// 创建拼图对象
    /// </summary>
    /// <param name="jigsawPic">生成拼图所需图片</param>
    /// <returns></returns>
    private static GameObject createGameObjForJigsaw(String gameObjName)
    {
        GameObject jigsawGameObj = new GameObject(gameObjName);

        jigsawGameObj.AddComponent<MeshRenderer>();
        jigsawGameObj.AddComponent<MeshFilter>();
        jigsawGameObj.AddComponent<Rigidbody2D>();
        jigsawGameObj.AddComponent<BoxCollider2D>();
        //jigsawGameObj.AddComponent<PolygonCollider2D>();

        return jigsawGameObj;
    }

    /// <summary>
    /// 设置拼图的图片纹理
    /// </summary>
    /// <param name="jigsawGameObj"></param>
    /// <param name="jigsawPic"></param>
    private static void setRenderer(GameObject jigsawGameObj, Texture2D jigsawPic)
    {
        //获取拼图的render并设置贴图
        Renderer jigsawRenderer = jigsawGameObj.GetComponent<Renderer>();
        Material jigsawMaterial = jigsawRenderer.material;
        jigsawMaterial.mainTexture = jigsawPic;
    }

    /// <summary>
    /// 设置刚体
    /// </summary>
    /// <param name="jigsawGameObj"></param>
    private static void setRigidbody2D(GameObject jigsawGameObj)
    {
        //获取拼图的刚体并设置
        Rigidbody2D jigsawRB = jigsawGameObj.GetComponent<Rigidbody2D>();
        jigsawRB.gravityScale = 0f;
    }

    /// <summary>
    /// 设置网格
    /// </summary>
    /// <param name="jigsawGameObj"></param>
    /// <param name="listVertices"></param>
    /// <param name="listUVPostion"></param>
    private static void setMeshFilter(GameObject jigsawGameObj, List<Vector3> listVertices, List<Vector2> listUVPostion)
    {
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
    }

    /// <summary>
    /// 设置2D碰撞器
    /// </summary>
    /// <param name="jigsawGameObj"></param>
    /// <param name="listVertices"></param>
    private static void setCollider2D(GameObject jigsawGameObj, List<Vector3> listVertices) {

        PolygonCollider2D jigsawCollider= jigsawGameObj.GetComponent<PolygonCollider2D>();
        int listVerticesCount = listVertices.Count;
        Vector2[] colliderPath = new Vector2[listVerticesCount];
        for(int pathPostion=1; pathPostion< listVerticesCount; pathPostion++)
        {
            
            colliderPath[pathPostion] = new Vector2(listVertices[pathPostion].x, listVertices[pathPostion].y);
        }
        jigsawCollider.SetPath(0, colliderPath);
    }
 //--------------------------------------------------------------------------------------------------------
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
        int trianglesCount = listCount - 1;
        //三角学顶点数
        int[] triangles = new int[3 * trianglesCount];
        //三角形顶点索引,确保按照顺时针方向设置三角形顶点
        for (int i = 0; i < trianglesCount; i++)
        {
            triangles[i * 3] = 0;
            triangles[i * 3 + 1] = i + 1;
            //判断是否为最后一个三角索引  是的话就引用第二个索引
            if ((i + 2) > trianglesCount)
                triangles[i * 3 + 2] = 1;
            else
                triangles[i * 3 + 2] = i + 2;

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



}
