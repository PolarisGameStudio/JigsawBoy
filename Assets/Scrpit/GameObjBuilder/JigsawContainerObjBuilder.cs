using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JigsawContainerGameObjBuilder
{


    private static string Container_Name = "JigsawContainer";

    /// <summary>
    /// 创建拼图容器
    /// </summary>
    /// <param name="childJigsawData"></param>
    /// <returns></returns>
    public static GameObject buildJigsawContainerObj()
    {
        string objName = Container_Name + "_" + SystemUtil.getUUID();
        GameObject jigsawContainerObj = new GameObject(objName);

        //设置容器组件
        setContainerCpt(jigsawContainerObj);
        //设置刚体
        setRigidbody(jigsawContainerObj);
        //增加碰撞
        setCollider(jigsawContainerObj);
        return jigsawContainerObj;
    }



    /// <summary>
    /// 设置容器组件
    /// </summary>
    /// <param name="jigsawContainerObj"></param>
    private static void setContainerCpt(GameObject jigsawContainerObj)
    {
        jigsawContainerObj.AddComponent<JigsawContainerCpt>();
    }

    /// <summary>
    /// 设置刚体
    /// </summary>
    /// <param name="jigsawContainerObj"></param>
    private static void setRigidbody(GameObject jigsawContainerObj)
    {
        Rigidbody2D jigsawContainerRB = jigsawContainerObj.AddComponent<Rigidbody2D>();
        jigsawContainerRB.gravityScale = 0f;
    }

    /// <summary>
    /// 增加碰撞
    /// </summary>
    /// <param name="jigsawContainerObj"></param>
    public static void setCollider(GameObject jigsawContainerObj)
    {
        CompositeCollider2D jigsawContainerCollider = jigsawContainerObj.AddComponent<CompositeCollider2D>();
        jigsawContainerCollider.geometryType = CompositeCollider2D.GeometryType.Polygons;
        jigsawContainerCollider.generationType = CompositeCollider2D.GenerationType.Synchronous;
    }


}
