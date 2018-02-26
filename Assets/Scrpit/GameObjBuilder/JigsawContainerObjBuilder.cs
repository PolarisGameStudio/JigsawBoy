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
        addContainerCpt(jigsawContainerObj);
        //设置刚体
        //addRigidbody(jigsawContainerObj);
        //增加碰撞
        //addCollider(jigsawContainerObj);
        return jigsawContainerObj;
    }



    /// <summary>
    /// 设置容器组件
    /// </summary>
    /// <param name="jigsawContainerObj"></param>
    private static void addContainerCpt(GameObject jigsawContainerObj)
    {
        jigsawContainerObj.AddComponent<JigsawContainerCpt>();
    }

    /// <summary>
    /// 设置刚体
    /// </summary>
    /// <param name="jigsawContainerObj"></param>
    public static void addRigidbody(GameObject jigsawContainerObj)
    {
        Rigidbody2D jigsawContainerRB = jigsawContainerObj.AddComponent<Rigidbody2D>();
        jigsawContainerRB.gravityScale = 0f;
    }

    /// <summary>
    /// 增加碰撞
    /// </summary>
    /// <param name="jigsawContainerObj"></param>
    public static CompositeCollider2D addCollider(GameObject jigsawContainerObj)
    {
        Transform[] childsTFs= jigsawContainerObj.GetComponentsInChildren<Transform>();
        if (childsTFs != null)
        {
            int childsTFsSize = childsTFs.Length;
            for (int childPosition = 0; childPosition < childsTFsSize; childPosition++)
            {
                Transform childsItemTF = childsTFs[childPosition];
                NormalJigsawCpt jigsawItem= childsItemTF.GetComponent<NormalJigsawCpt>();
                if (jigsawItem == null)
                    continue;
                JigsawBean jigsawData= jigsawItem.getJigsawData();
                if (jigsawData == null)
                    continue;
                JigsawObjBuilder.setCollider2D(childsItemTF.gameObject, jigsawData.CenterVector,jigsawData.JigsawWith,jigsawData.JigsawHigh);
            };
        }

        CompositeCollider2D jigsawContainerCollider = jigsawContainerObj.AddComponent<CompositeCollider2D>();
        jigsawContainerCollider.geometryType = CompositeCollider2D.GeometryType.Polygons;
        jigsawContainerCollider.generationType = CompositeCollider2D.GenerationType.Synchronous;
        return jigsawContainerCollider;
    }


}
