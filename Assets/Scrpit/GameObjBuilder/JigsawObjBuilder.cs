using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class JigsawObjBuilder
{
    /// <summary>
    /// 获取拼图GameObj
    /// </summary>
    /// <param name="JigsawBean">需要生成的拼图块数据</param>
    /// <returns></returns>
    public static GameObject buildJigsawGameObj(JigsawBean jigsawData, Texture2D jigsawPic)
    {
        if (jigsawData == null)
            throw new Exception("没有拼图数据");
        List<Vector3> listVertices = jigsawData.ListVertices;
        List<Vector2> listUVposition = jigsawData.ListUVposition;
        Vector2 markLocation = jigsawData.MarkLocation;
        Vector3 centerVector = jigsawData.CenterVector;
        float jigsawWith = jigsawData.JigsawWith;
        float jigsawHigh = jigsawData.JigsawHigh;
        JigsawStyleEnum jigsawStyleEnum = jigsawData.JigsawStyle;

        if (listVertices == null)
        {
            LogUtil.log("生产拼图gameObj失败-没有顶点坐标");
            return null;
        }
        if (listVertices.Count < 3)
        {
            LogUtil.log("生产拼图gameObj失败-顶点坐标小于3");
            return null;
        }
        if (listUVposition == null)
        {
            LogUtil.log("生产拼图gameObj失败-没有图片UV坐标");
            return null;
        }
        if (!listUVposition.Count.Equals(listVertices.Count))
        {
            LogUtil.log("生产拼图gameObj失败-UV坐标与定点坐标数量不对等");
            return null;
        }
        if (jigsawPic == null)
        {
            LogUtil.log("生产拼图gameObj失败-没有生成拼图所需图片");
            return null;
        }
        if (centerVector == null)
        {
            LogUtil.log("生产拼图gameObj失败-没有拼图中心点");
            return null;
        }

        //创建拼图的游戏对象
        String gameObjName = jigsawPic.name + "_X" + markLocation.x + "_Y" + markLocation.y;
        GameObject jigsawGameObj = createGameObjForJigsaw(gameObjName);

        //设置贴图
        setRenderer(jigsawGameObj, jigsawPic);
        //设置网格
        setMeshFilter(jigsawGameObj, listVertices, listUVposition);
        //设置2D碰撞器
        //setCollider2D(jigsawGameObj, centerVector, jigsawWith, jigsawHigh);
        //设置线框
        //setWireFrame(jigsawGameObj);
        //设置拼图component
        setJigsawCpt(jigsawGameObj, jigsawData);

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
        return jigsawGameObj;
    }

    /// <summary>
    /// 设置拼图组件
    /// </summary>
    /// <param name="jigsawGameObj"></param>
    private static void setJigsawCpt(GameObject jigsawGameObj, JigsawBean jigsawData)
    {
        JigsawStyleEnum jigsawStyle = jigsawData.JigsawStyle; 
        if (jigsawStyle == JigsawStyleEnum.Normal) {
            NormalJigsawCpt jigsawCpt= jigsawGameObj.AddComponent<NormalJigsawCpt>();
            jigsawCpt.setJigsawData(jigsawData);
            jigsawCpt.setEdgeMergeStatus(JigsawStyleNormalEdgeEnum.Left,JigsawMergeStatusEnum.Unincorporated);
            jigsawCpt.setEdgeMergeStatus(JigsawStyleNormalEdgeEnum.Above, JigsawMergeStatusEnum.Unincorporated);
            jigsawCpt.setEdgeMergeStatus(JigsawStyleNormalEdgeEnum.Right, JigsawMergeStatusEnum.Unincorporated);
            jigsawCpt.setEdgeMergeStatus(JigsawStyleNormalEdgeEnum.Below, JigsawMergeStatusEnum.Unincorporated);
        }
    }

    /// <summary>
    /// 设置拼图的图片纹理
    /// </summary>
    /// <param name="jigsawGameObj"></param>
    /// <param name="jigsawPic"></param>
    private static void setRenderer(GameObject jigsawGameObj, Texture2D jigsawPic)
    {
        //获取拼图的render并设置贴图
        Renderer jigsawRenderer = jigsawGameObj.AddComponent<MeshRenderer>();
        jigsawRenderer.sortingOrder = -1;

        Material jigsawMaterial = jigsawRenderer.material;
        //设置贴图无光照
        jigsawMaterial.shader = Shader.Find("Unlit/Texture");
        jigsawMaterial.mainTexture = jigsawPic;
    }

    /// <summary>
    /// 设置网格
    /// </summary>
    /// <param name="jigsawGameObj"></param>
    /// <param name="listVertices"></param>
    /// <param name="listUVposition"></param>
    private static void setMeshFilter(GameObject jigsawGameObj, List<Vector3> listVertices, List<Vector2> listUVposition)
    {
        //获取拼图游戏对象的mesh
        Mesh jigsawMesh = jigsawGameObj.AddComponent<MeshFilter>().mesh;
        //创建拼图的坐标点
        Vector3[] jigsawVertices = createJigsawVertices(listVertices);
        //创建拼图的三角形索引
        int[] jigsawTriangles = createJigsawTriangles(listVertices);
        //创建拼图的UV坐标点
        Vector2[] jigsawUVVertices = createJigsawUVposition(listUVposition);

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
    public static void setCollider2D(GameObject jigsawGameObj, Vector3 centerVector, float jigsawWith, float jigsawHigh)
    {
        BoxCollider2D jigsawCollider = jigsawGameObj.AddComponent<BoxCollider2D>();
        jigsawCollider.size = new Vector2(jigsawWith, jigsawHigh);
        jigsawCollider.offset = new Vector2(centerVector.x, centerVector.y);
        jigsawCollider.usedByComposite = true;
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
    /// <param name="listUVposition"></param>
    /// <returns></returns>
    private static Vector2[] createJigsawUVposition(List<Vector2> listUVposition)
    {
        if (listUVposition == null)
            return null;
        //获取UV列表大小
        int listCount = listUVposition.Count;
        //UV数组  
        Vector2[] uvVertices = new Vector2[listCount];
        //将链表中的UV坐标赋值给vertices  
        for (int i = 0; i < listCount; i++)
        {
            uvVertices[i] = listUVposition[i];
        }
        return uvVertices;
    }
}
